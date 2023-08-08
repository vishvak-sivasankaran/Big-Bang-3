using Kanini_Tourism.Models;
using Kanini_Tourism.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kanini_Tourism.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedback _user;
        public FeedbackController(IFeedback user)
        {
            _user = user;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Feedback>> Get()
        {
            var feedbacks = _user.GetAllFeedback();
            return Ok(feedbacks);
        }
        [HttpGet("{id}")]
        public Feedback GetById(int id)
        {
            return _user.GetFeedbackById(id);
        }

        [HttpPost]
        public async Task<ActionResult<List<Feedback>>> Add(Feedback user)
        {
            var users = await _user.AddFeedback(user);
            return Ok(users);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Feedback updatedFeedback)
        {
            var existingFeedback = await _user.UpdateFeedback(id, updatedFeedback);
            if (existingFeedback == null)
            {
                return NotFound();
            }
            return Ok(existingFeedback);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Feedback>>> DeleteById(int id)
        {
            var users = await _user.DeleteFeedbackById(id);
            if (users is null)
            {
                return NotFound("userid not matching");
            }
            return Ok(users);
        }
    }
}
