using VideoManager.Models;

namespace VideoManager.DAL.Repository.IRepository;

public interface IGenreRepository : IRepository<Genre>
{
    void Update(Genre entity);
}