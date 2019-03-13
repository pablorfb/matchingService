
using System.Collections.Generic;
using MatchingService.Feature;
using MatchingService.Models;
using Microsoft.AspNetCore.Mvc;

namespace MatchingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private static Matcher Matcher = MatcherFactory.GetInstance();

        // GET api/match
        [HttpGet]
        public ActionResult<GameMatch> Get()
        {
            return Ok(Matcher.TryMatch());
        }

        // POST api/match
        [HttpPost]
        public ActionResult Post([FromBody] GameRequest value)
        {
            if (Matcher.TryAdd(value))
            {
                return Accepted();
            }

            return BadRequest("The user id could not be added to the queue");

        }
    }
}
