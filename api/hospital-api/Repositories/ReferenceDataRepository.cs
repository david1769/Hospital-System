using hospital_api.Data;
using hospital_api.Interface.Repository;
using hospital_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace hospital_api.Repositories
{
    public class ReferenceDataRepository : QueryRepository<ReferenceData>, IQueryRepository<ReferenceData>
    {

        public ReferenceDataRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public override async Task<List<ReferenceData>> GetAll()
        {
            var items = await context.Set<ReferenceData>()
                .Where(i => i.IsActive == true)
                .Include(i => i.ReferenceDataCategory)
                .ToListAsync();

            return items;
        }


        public override async Task<ReferenceData> GetById(int id)
        {
            var item = await context.Set<ReferenceData>().Where(i => i.IsActive == true)
                .Include(i => i.ReferenceDataCategory)
                .FirstOrDefaultAsync(a => a.Id == id);

            return item!;
        }

        public override IQueryable<ReferenceData> Filter(Expression<Func<ReferenceData, bool>> predicate)
        {
            var items = context.Set<ReferenceData>().Where(i => i.IsActive == true).Include(i => i.ReferenceDataCategory);



            return items.Where(predicate);
        }


    }
}
