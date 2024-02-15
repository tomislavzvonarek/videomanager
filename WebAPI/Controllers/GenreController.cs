using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoManager.DAL.Repository.IRepository;
using VideoManager.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/Genre
        [HttpGet]
        public ActionResult<IEnumerable<Genre>> Get()
        {
            try
            {
                return _unitOfWork.GenreRepository.GetAll().ToList();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: api/Genre/5
        //[HttpGet("{id}", Name = "Get")]
        [HttpGet, Route("[controller]/{id}")]
        public ActionResult<Genre> Get(int id)
        {
            try
            {
                return _unitOfWork.GenreRepository.Get(x => x.Id == id);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("name/{name}")]
        public ActionResult<Genre> GetGenreByName(string name)
        {
            try
            {
                return _unitOfWork.GenreRepository.Get(x => x.Name != null && x.Name.ToLower().Contains(name));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpGet("description/{description}")]
        public ActionResult<Genre> GetGenreByDescription(string description)
        {
            try
            {
                return _unitOfWork.GenreRepository.Get(x => x.Description != null && x.Description.ToLower().Contains(description));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: api/Genre
        [HttpPost]
        public ActionResult<Genre> Post([FromBody] Genre obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Genre newGenre = new()
            {
                Name = obj.Name,
                Description = obj.Description
            };
            try
            {
                _unitOfWork.GenreRepository.Add(newGenre);
                _unitOfWork.Save();

                return Ok(newGenre);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT: api/Genre/5
        [HttpPut("{id}")]
        public ActionResult<Genre> Put(int id, [FromBody] Genre obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var target = _unitOfWork.GenreRepository.Get(x => x.Id == id);
                if (target is null)
                {
                    return NotFound();
                }

                target.Name = obj.Name;
                target.Description = obj.Description;
                _unitOfWork.GenreRepository.Update(target);
                _unitOfWork.Save();

                return Ok(target);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE: api/Genre/5
        [HttpDelete("{id}")]
        public ActionResult<Genre> Delete(int id)
        {
            try
            {
                var target = _unitOfWork.GenreRepository.Get(x => x.Id == id);
                if (target is null)
                {
                    return NotFound();
                }
                _unitOfWork.GenreRepository.Remove(target);
                _unitOfWork.Save();

                return Ok(target);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
