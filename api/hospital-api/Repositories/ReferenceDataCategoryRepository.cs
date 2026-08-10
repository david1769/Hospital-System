using hospital_api.Data;
using hospital_api.Interface.Repository;
using hospital_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace hospital_api.Repositories
{
    public class ReferenceDataCategoryRepository : QueryRepository<ReferenceDataCategory>, IQueryRepository<ReferenceDataCategory>
    {

        public ReferenceDataCategoryRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public override async Task<List<ReferenceDataCategory>> GetAll()
        {
            var items = await context.Set<ReferenceDataCategory>()
                .Where(i => i.IsActive == true)
                .ToListAsync();

            return items;
        }


        public override async Task<ReferenceDataCategory> GetById(int id)
        {
            var item = await context.Set<ReferenceDataCategory>().Where(i => i.IsActive == true)
                .FirstOrDefaultAsync(a => a.Id == id);

            return item;
        }

        public override IQueryable<ReferenceDataCategory> Filter(Expression<Func<ReferenceDataCategory, bool>> predicate)
        {
            var items = context.Set<ReferenceDataCategory>().Where(i => i.IsActive == true);



            return items.Where(predicate);
        }


    }
}
