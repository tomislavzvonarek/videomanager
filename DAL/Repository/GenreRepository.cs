using VideoManager.DAL.Repository.IRepository;
using VideoManager.Data;
using VideoManager.Models;

namespace VideoManager.DAL.Repository;

public class GenreRepository : Repository<Genre>, IGenreRepository
{
    private ApplicationDbContext _db;

    public GenreRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Genre obj)
    {
        _db.Genres.Update(obj);
    }
}