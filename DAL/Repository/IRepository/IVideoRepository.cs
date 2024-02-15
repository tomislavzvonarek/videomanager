using VideoManager.Models;

namespace VideoManager.DAL.Repository.IRepository;

public interface IVideoRepository : IRepository<Video>
{
    void Update(Video obj);
}