using VideoManager.DAL.Repository.IRepository;
using VideoManager.Data;
using VideoManager.Models;

namespace VideoManager.DAL.Repository;

public class CountryRepository : Repository<Country>, ICountryRepository
{
    private ApplicationDbContext _db;

    public CountryRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Country obj)
    {
        _db.Countries.Update(obj);
    }
}