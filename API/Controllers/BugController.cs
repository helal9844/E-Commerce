using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugController : BaseAPIController
    {
        private readonly StoreContext _context;

        public BugController(StoreContext context)
        {
            _context = context;
        }
        [HttpGet("NotFound")]
        public IActionResult GetNotFoundReqest()
        {
            var thing = _context.Proucts.Find(50);
            if (thing == null) 
                return NotFound(new APIResponse(404));
            return Ok(thing);
        }
        [HttpGet("ServerError")]
        public IActionResult GetServerErorr()
        {
            var thing = _context.Proucts.Find(50);
            var thingReturn = thing.ToString();
            return Ok();
        }
        [HttpGet("BadRequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new APIResponse(400));
        }
        [HttpGet("BadRequest/{id}")]
        public IActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }
    }
}
