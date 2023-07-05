using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Entities;
using Core.Intrefaces;
using Core.ProductwithBrandandTypeSpecification;
using API.DTOs;
using AutoMapper;
using Core.Specifications;
using API.Helpers;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseAPIController
    {
        private readonly IGenericRepo<ProductBrand> _brandrepo;
        private readonly IGenericRepo<ProductType> _typerepo;
        private readonly IGenericRepo<Product> _productrepo;
        private readonly IMapper _mapper;
        public ProductController(IGenericRepo<Product> productRepo, IGenericRepo<ProductBrand> brandrepo, IGenericRepo<ProductType> typerepo, IMapper mapper = null)
        {
            _productrepo = productRepo;
            _brandrepo = brandrepo;
            _typerepo = typerepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDTO>>> GetProudects([FromQuery]ProductSpecParams specParams)
        {
            ProductwithBrandandTypeSpecification? spec = new ProductwithBrandandTypeSpecification(specParams);
            var countSpec = new ProductWithFiltersForCountSpec(specParams);
            var totalItems = await _productrepo.CountAsync(countSpec); 
            var products = await _productrepo.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDTO>>(products);
            return Ok(new Pagination<ProductDTO>(specParams.PageIndex,specParams.PageSize,totalItems,data));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProudect(int id)
        {
            ProductwithBrandandTypeSpecification? spec = new ProductwithBrandandTypeSpecification(id);
            var product = await _productrepo.GetEntityWithSpec(spec);
            return _mapper.Map<Product, ProductDTO>(product);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var productType = await _typerepo.ListAllAsync();
            return Ok(productType);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var productBrand = await _brandrepo.ListAllAsync();
            return Ok(productBrand);
        }
    }
}
