using hospital_api.Models;

namespace hospital_api.Interface.Repository
{
    public interface ICommandRepository<Ent> where Ent : Entity
    {
        Task Create(Ent ent);
        Task Update(Ent old, Ent ent);
        Task Delete(Ent ent);
        Task Exist(Ent ent);
    }
}
