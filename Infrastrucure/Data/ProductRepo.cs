
using Core.Entities;
using Core.Intrefaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace Infrastrucure.Data
{
    public class ProductRepo : IProductRepo
    {
        private readonly StoreContext _context;
        public ProductRepo(StoreContext context)
        {
            _context = context;
        }

        
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Proucts
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType).FirstOrDefaultAsync(p=>p.Id == id);   
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Proucts
                .Include(p=> p.ProductBrand)
                .Include(p=>p.ProductType).ToListAsync();
        }
        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }
    }
}
