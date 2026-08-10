using hospital_api.Data;
using hospital_api.Interface.Repository;
using hospital_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace hospital_api.Repositories
{
    public class AppointmentRepository : QueryRepository<Appointment>,IQueryRepository<Appointment>
    {

        public AppointmentRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public override async Task<List<Appointment>> GetAll()
        {
            var items = await context.Set<Appointment>().
                Where(i => i.IsActive == true)
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Department)
                .Include(a => a.Status)
                .ToListAsync();

            return items; 
        }


        public override async Task<Appointment> GetById(int id)
        {
            var item = await context.Set<Appointment>().Where(i => i.IsActive == true)
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Department)
                .Include(a => a.Status)

                .FirstOrDefaultAsync(a => a.Id == id);

            return item!;
        }

        public override IQueryable<Appointment> Filter(Expression<Func<Appointment, bool>> predicate)
        {
            var items = context.Set<Appointment>().Where(i => i.IsActive == true).Include(a => a.Patient)
                .Include(a => a.Doctor).Include(a => a.Department).Include(a => a.Status)
;


            return items.Where(predicate);
        }




    }
}
