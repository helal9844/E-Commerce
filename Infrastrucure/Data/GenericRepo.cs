using Core.Entities;
using Core.Intrefaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastrucure.Data
{
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
    {
        private readonly StoreContext _storecontext;

        public GenericRepo(StoreContext storecontext)
        {
            _storecontext = storecontext;
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _storecontext.Set<T>().FindAsync(id);
        }
        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _storecontext.Set<T>().ToListAsync();
        }
        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplaySpecification(spec).FirstOrDefaultAsync();
        }
        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplaySpecification(spec).ToListAsync();
        }
        private IQueryable<T> ApplaySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_storecontext.Set<T>().AsQueryable(), spec);
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplaySpecification(spec).CountAsync();
        }
    }
}
