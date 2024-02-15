using Microsoft.Extensions.DependencyInjection;
using VideoManager.Data;
using VideoManager.Models;

namespace VideoManager.DAL.Repository.IRepository;

public class VideoRepository : Repository<Video>, IVideoRepository
{
    private ApplicationDbContext _db;
    public VideoRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Video obj)
    {
        var videoFromDb = _db.Videos.FirstOrDefault(x => x.Id == obj.Id);
        if (videoFromDb is not null)
        {
            videoFromDb.Name = obj.Name;
            videoFromDb.Description = obj.Description;
            videoFromDb.TotalTime = obj.TotalTime;
            videoFromDb.TagId = obj.TagId;
            videoFromDb.GenreId = obj.GenreId;
            if (obj.ImageUrl is not null)
            {
                videoFromDb.ImageUrl = obj.ImageUrl;
            }
        }
    }
}