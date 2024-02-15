using VideoManager.Models;

namespace VideoManager.DAL.Repository.IRepository;

public interface ICountryRepository : IRepository<Country>
{
    void Update(Country entity);
}