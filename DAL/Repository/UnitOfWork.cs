using System;
using VideoManager.DAL.Repository.IRepository;
using VideoManager.Data;

namespace VideoManager.DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ITagRepository TagRepository { get; private set; }
        public IVideoRepository VideoRepository { get; private set; }
        public IGenreRepository GenreRepository { get; private set; }
        public ICountryRepository CountryRepository { get; private set; }
        private ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            TagRepository = new TagRepository(_db);
            VideoRepository = new VideoRepository(_db);
            GenreRepository = new GenreRepository(_db);
            CountryRepository = new CountryRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}

