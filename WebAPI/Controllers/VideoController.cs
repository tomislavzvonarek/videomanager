using Microsoft.AspNetCore.Mvc;
using VideoManager.DAL.Repository.IRepository;
using VideoManager.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public VideoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        
        // GET: api/Video
        [HttpGet]
        public ActionResult<IEnumerable<Video>> Get()
        {
            try
            {
                return _unitOfWork.VideoRepository.GetAll().ToList();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("name/{name}")]
        public ActionResult<Video> GetVideoByName(string name)
        {
            try
            {
                return _unitOfWork.VideoRepository.Get(x => x.Name.ToLower().Contains(name));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("orderByName")]
        public IOrderedEnumerable<Video> GetVideosOrderBy()
        {
                return _unitOfWork.VideoRepository.GetAll().OrderBy(x => x.Name);
        }

        [HttpGet("orderById")]
        public IOrderedEnumerable<Video> GetVideosOrderById()
        {
            return _unitOfWork.VideoRepository.GetAll().OrderBy(x => x.Id);
        }

        [HttpGet("paging")]
        public ActionResult<IEnumerable<Video>> GetVideosByPaging(int page = 1, int pageSize = 10)
        {
            var target = _unitOfWork.VideoRepository.GetAll();
            var query = target.AsQueryable();
            query = query.Skip((page) * pageSize).Take(pageSize);
            target = query.ToList();

            return Ok(target);
        }

        // GET: api/Video/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Video> Get(int id)
        {
            try
            {
                var target = _unitOfWork.VideoRepository.Get(x => x.Id == id);
                if (target is null)
                {
                    return NotFound();
                }

                return Ok(target);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        // POST: api/Video
        [HttpPost]
        public ActionResult<Video> Post([FromBody] Video obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Video newVideo = new()
                {
                    Name = obj.Name,
                    Description = obj.Description,
                    TotalTime = obj.TotalTime,
                    StreamingUrl = obj.StreamingUrl,
                    ImageUrl = obj.ImageUrl,
                    TagId = obj.TagId,
                    GenreId = obj.GenreId
                };
                _unitOfWork.VideoRepository.Add(newVideo);
                _unitOfWork.Save();

                return Ok(newVideo);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        } 
        

        // PUT: api/Video/5
        [HttpPut("{id}")]
        public ActionResult<Video> Put(int id, [FromBody] Video obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var target = _unitOfWork.VideoRepository.Get(x => x.Id == id);
                if (target is null)
                {
                    return NotFound();
                }

                target.Name = obj.Name;
                target.Description = obj.Description;
                target.TotalTime = obj.TotalTime;
                target.StreamingUrl = obj.StreamingUrl;
                target.ImageUrl = obj.ImageUrl;
                target.TagId = obj.TagId;
                target.GenreId = obj.GenreId;
                _unitOfWork.VideoRepository.Update(target);
                _unitOfWork.Save();

                return Ok(target);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE: api/Video/5
        [HttpDelete("{id}")]
        public ActionResult<Video> Delete(int id)
        {
            try
            {
                var target = _unitOfWork.VideoRepository.Get(x => x.Id == id);
                if (target is null)
                {
                    return NotFound();
                }
                _unitOfWork.VideoRepository.Remove(target);
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
