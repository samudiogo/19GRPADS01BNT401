using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using _19GRPADS01BNT401_TP3Api.Models;
using _19GRPADS01BNT401_TP3Api.Services;

namespace _19GRPADS01BNT401_TP3Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FriendsController : ControllerBase
    {

        private readonly IFriendService _service;

        public FriendsController(IFriendService service) => _service = service;


        // GET: api/Friends
        [HttpGet]
        public async Task<IEnumerable<FriendModel>> GetFriends() => await _service.GetAllAsync();

        // GET: api/Friends/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FriendModel>> GetFriend(Guid id)
        {
            var friend = await _service.GetByIdAsync(id);

            if (friend == null)
            {
                return NotFound();
            }

            return friend;
        }

        // PUT: api/Friends/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFriend(Guid id, FriendModel friend)
        {
            if (id != friend.Id)
            {
                return BadRequest();
            }

            try
            {
                await _service.UpdateAsync(id, friend);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        // POST: api/Friends
        [HttpPost]
        public async Task<ActionResult<FriendModel>> PostFriend(FriendModel friend)
        {
            friend.Id = Guid.NewGuid();

            await _service.CreateAsync(friend);

            return CreatedAtAction("GetFriend", new { id = friend.Id }, friend);
        }

        // DELETE: api/Friends/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FriendModel>> DeleteFriend(Guid id)
        {
            var friend = await _service.GetByIdAsync(id);

            if (friend == null)
            {
                return NotFound();
            }
            
            await _service.DeleteAsync(friend.Id);

            return friend;
        }
    }
}
