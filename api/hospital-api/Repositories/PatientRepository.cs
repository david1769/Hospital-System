using hospital_api.Data;
using hospital_api.Interface.Repository;
using hospital_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;


namespace hospital_api.Repositories
{
    public class PatientRepository : QueryRepository<Patient>, IQueryRepository<Patient>
    {

        public PatientRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public override async Task<List<Patient>> GetAll()
        {
            var items = await context.Set<Patient>()
                .Where(i => i.IsActive == true)
                .Include(a => a.InsuranceProvider)
                .ToListAsync();

            return items;
        }





        public override async Task<Patient> GetById(int id)
        {
            var item = await context.Set<Patient>().Where(i => i.IsActive == true)
                   .Include(a => a.InsuranceProvider)

                .FirstOrDefaultAsync(a => a.Id == id);

            return item;
        }

        public override IQueryable<Patient> Filter(Expression<Func<Patient, bool>> predicate)
        {
            var items = context.Set<Patient>().Where(i => i.IsActive == true)
                
                .Include(a => a.InsuranceProvider);


            return items.Where(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<Patient, bool>>? predicate = null)
        {
            var query = context.Set<Patient>().Where(i => i.IsActive == true);

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.CountAsync(); 
        }


        
    }





}

