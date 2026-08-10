using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using hospital_api.Interface.Repository;
using hospital_api.Models;
using Microsoft.EntityFrameworkCore;
using hospital_api.Data;
namespace hospital_api.Repositories

{
    public class QueryRepository<T> : IQueryRepository<T> where T : Entity
    {
       protected ApplicationDbContext context;
        public QueryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

   
        public virtual IQueryable<T> Filter(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where(predicate);
        }

        public virtual async Task<List<T>> Filter(Expression<Func<T, bool>> predicate, int pageNumber = 1, int pageSize = 10)
        {
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            var totalCount = context.Set<T>().Where(predicate).Count();
            var startRow = (pageNumber - 1) * pageSize;
            var items = await context.Set<T>().Where(predicate).Skip(startRow)
                      .Take(pageSize).ToListAsync();
            return items;
        }

        public virtual async Task<List<T>> GetAll()
        {
            var items = await context.Set<T>().Where(i => i.IsActive == true).ToListAsync();
            return items;
        }


        public virtual async  Task<T> GetById(int id)
        {
            var item = await context.Set<T>().FindAsync(id);
            return item;   
        }

        public virtual async Task<T> GetByPlainId(int id)
        {
            var item = await context.Set<T>().FindAsync(id);
            return item;
        }


        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            var query = context.Set<T>().AsQueryable();

            if (predicate != null)
                query = query.Where(predicate);

            return await query.CountAsync();
        }


    }
}
