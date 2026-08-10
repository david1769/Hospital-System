using hospital_api.Interface;
using hospital_api.Interface.Repository;
using hospital_api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using hospital_api.Models;
using hospital_api.Extension;

namespace hospital_api.Repositories
{
    public class CommandRepository<T> : ICommandRepository<T> where T : Entity
    {
       private readonly ApplicationDbContext context;
        IHttpContextAccessor httpContextAccessor;

        public CommandRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async System.Threading.Tasks.Task Create(T ent)
        {
            ent.CreatedAt = DateTime.Now;
            ent.CreatedBy = httpContextAccessor.GetUser();
            ent.IsActive = true;

            await context.Set<T>().AddAsync(ent);
            await context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task  Delete(T ent)
        {
            _ = new Entity();
            Entity deletedItem = ent;
            deletedItem.IsActive = false;
            deletedItem.UpdatedAt = DateTime.Now;
            deletedItem.UpdatedBy = httpContextAccessor.GetUser();
            context.Entry(ent).CurrentValues.SetValues(deletedItem);
            await context.SaveChangesAsync();

        }

        public Task Exist(T ent)
        {
            throw new NotImplementedException();
        }

        public async Task Update(T old, T ent)
        {
            ent.UpdatedAt = DateTime.Now;
            ent.UpdatedBy = httpContextAccessor.GetUser();

            context.Entry(old).CurrentValues.SetValues(ent);

            await context.SaveChangesAsync();
        }
    }
}
