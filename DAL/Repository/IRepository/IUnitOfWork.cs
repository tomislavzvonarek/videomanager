using System;
namespace VideoManager.DAL.Repository.IRepository
{
	public interface IUnitOfWork
	{
		ITagRepository TagRepository{ get; }
		IVideoRepository VideoRepository { get;  }
		IGenreRepository GenreRepository { get; }
		ICountryRepository CountryRepository { get; }
		void Save();
	}
}

