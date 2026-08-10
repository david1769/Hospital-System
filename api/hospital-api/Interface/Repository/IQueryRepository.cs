using hospital_api.Models;
using System.Linq.Expressions;

namespace hospital_api.Interface.Repository
{
    public interface IQueryRepository<Ent> where Ent : Entity
    {
        Task<List<Ent>> Filter(Expression<Func<Ent, bool>> predicate, int pageNumber = 1, int pageSize = 10);
        IQueryable<Ent> Filter(Expression<Func<Ent, bool>> predicate);
        Task<Ent> GetById(int id);
        Task<List<Ent>> GetAll();
        Task<Ent> GetByPlainId(int id);
        Task<int> CountAsync(Expression<Func<Ent, bool>>? predicate = null);
    }
}
