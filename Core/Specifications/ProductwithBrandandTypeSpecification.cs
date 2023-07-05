using Core.Entities;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ProductwithBrandandTypeSpecification
{
    public class ProductwithBrandandTypeSpecification: Specification<Product>
    {
        public ProductwithBrandandTypeSpecification(ProductSpecParams productSpec)
            :base(x => 
            (!productSpec.BrandId.HasValue || x.ProductBrandId == productSpec.BrandId) &&
            (!productSpec.TypeId.HasValue || x.ProductTypeId == productSpec.TypeId)
            )
            
        {
            AddIncludes(p=>p.ProductType);
            AddIncludes(p => p.ProductBrand);
            AddOrderBy(p => p.Name);

            ApplayPaging(productSpec.PageSize * productSpec.PageIndex - 1, productSpec.PageSize);
            if (!string.IsNullOrEmpty(productSpec.Sort))
            {
                switch (productSpec.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }
        public ProductwithBrandandTypeSpecification(int id):base(x=>x.Id == id)
        {
            AddIncludes(p => p.ProductType);
            AddIncludes(p => p.ProductBrand);
        }
       
    }
}
