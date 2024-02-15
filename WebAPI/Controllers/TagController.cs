using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoManager.DAL.Repository.IRepository;
using VideoManager.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/Tag
        [HttpGet]
        public ActionResult<IEnumerable<Tag>> Get()
        {
            try
            {
                return _unitOfWork.TagRepository.GetAll().ToList();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: api/Tag/5
        //[HttpGet("{id}", Name = "Get")]
        [HttpGet, Route("[controller]/{id}")]
        public ActionResult<Tag> Get(int id)
        {
            try
            {
                var target = _unitOfWork.TagRepository.Get(x => x.Id == id);
                
                return Ok(target);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("name/{name}")]
        public ActionResult<Tag> GetResultByName(string name)
        {
            try
            {
                return _unitOfWork.TagRepository.Get(x => x.Name != null && x.Name.ToLower().Contains(name));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: api/Tag
        [HttpPost]
        public ActionResult<Tag> Post([FromBody] Tag obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            //there is no need to check for maxId in collection of tags because it's set up in database
            Tag newTag = new()
            {
                Name = obj.Name
            };
            try
            {
                _unitOfWork.TagRepository.Add(newTag);
                _unitOfWork.Save();
            
                return Ok(newTag);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT: api/Tag/5
        [HttpPut("{id}")]
        public ActionResult<Tag> Put(int id, [FromBody] Tag obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {

                var target = _unitOfWork.TagRepository.Get(x => x.Id == id);
                 if (target is null)
                {
                    return NotFound();
                }

                target.Name = obj.Name;
            
                _unitOfWork.TagRepository.Update(target);
                _unitOfWork.Save();
            
                return Ok(target);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE: api/Tag/5
        [HttpDelete("{id}")]
        public ActionResult<Tag> Delete(int id)
        {
            try
            {
                var target = _unitOfWork.TagRepository.Get(x => x.Id == id);
                if (target is null) 
                {
                     return NotFound(); 
                }

            
                _unitOfWork.TagRepository.Remove(target);
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
