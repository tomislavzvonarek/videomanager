using System;
using VideoManager.Models;

namespace VideoManager.DAL.Repository.IRepository
{
	public interface ITagRepository : IRepository<Tag>
	{
		void Update(Tag entity);
	}
}

