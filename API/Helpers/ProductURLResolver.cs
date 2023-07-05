using API.DTOs;
using AutoMapper;
using AutoMapper.Execution;
using Core.Entities;

namespace API.Helpers
{
    public class ProductURLResolver : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration _configuration;

        public ProductURLResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureURL))
            {
                return _configuration["APIUrl"] + source.PictureURL;
            }
            return null;
        }
    }
}
