using hospital_api.Data;
using hospital_api.Interface.Repository;
using hospital_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace hospital_api.Repositories
{
    public class DoctorRepository : QueryRepository<Doctor>, IQueryRepository<Doctor>
    {

        public DoctorRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public override async Task<List<Doctor>> GetAll()
        {
            var items = await context.Set<Doctor>()
                .Include(a => a.Specialty)
                .Include(a => a.Department)
                .Include(a => a.Schedule)
                .Include(a => a.Qualification)
                .Where(i => i.IsActive == true)
                .ToListAsync();

            return items;
        }


        public override async Task<Doctor> GetById(int id)
        {
            var item = await context.Set<Doctor>().Where(i => i.IsActive == true)
                .Include(a => a.Specialty)
                .Include(a => a.Department)
                .Include(a => a.Schedule)
                .Include(a => a.Qualification)
                .FirstOrDefaultAsync(a => a.Id == id);

            return item;
        }

        public override IQueryable<Doctor> Filter(Expression<Func<Doctor, bool>> predicate)
        {
            var items = context.Set<Doctor>().Where(i => i.IsActive == true)
                .Include(a => a.Specialty)
                .Include(a => a.Department)
                .Include(a => a.Schedule)
                .Include(a => a.Qualification);


            return items.Where(predicate);
        }



        public async Task<int> CountAsync(Expression<Func<Doctor, bool>>? predicate = null)
        {
            var query = context.Set<Doctor>().Where(i => i.IsActive == true);

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.CountAsync(); // from EF Core
        }


    }

}
