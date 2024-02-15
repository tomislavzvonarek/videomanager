using System;
using System.Linq.Expressions;
using VideoManager.DAL.Repository.IRepository;
using VideoManager.Data;
using VideoManager.Models;

namespace VideoManager.DAL.Repository
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        private ApplicationDbContext _db;
        public TagRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Tag obj)
        {
            _db.Tags.Update(obj);
        }
    }
}

