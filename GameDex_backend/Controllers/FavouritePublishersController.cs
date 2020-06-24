using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameDex_backend;
using GameDex_backend.Models;

namespace GameDex_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouritePublishersController : ControllerBase
    {
        private readonly UserContext _context;

        public FavouritePublishersController(UserContext context)
        {
            _context = context;
        }

        // GET: api/FavouritePublishers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavouritePublisher>>> GetFavouritePublisher()
        {
            return await _context.FavouritePublisher.ToListAsync();
        }

        // GET: api/FavouritePublishers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FavouritePublisher>> GetFavouritePublisher(int id)
        {
            var user = await _context.Users.Include(u => u.FavPublishers).Where(x => x.Id == id).SingleOrDefaultAsync();


            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.FavPublishers);
        }

        // PUT: api/FavouritePublishers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavouritePublisher(int id, FavouritePublisher favouritePublisher)
        {
            if (id != favouritePublisher.Id)
            {
                return BadRequest();
            }

            _context.Entry(favouritePublisher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavouritePublisherExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FavouritePublishers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<FavouritePublisher>> PostFavouritePublisher(FavouritePublisher favouritePublisher)
        {
            User user = _context.Users.Where(u => u.Id == favouritePublisher.UserId).Include(p => p.FavPublishers).FirstOrDefault();
            _context.Attach(user);
            _context.FavouritePublisher.Add(favouritePublisher);
            _context.Users.Update(user).Entity.FavPublishers.Add(favouritePublisher);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFavouritePublisher", new { id = favouritePublisher.Id }, favouritePublisher);
        }

        // DELETE: api/FavouritePublishers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FavouritePublisher>> DeleteFavouritePublisher(int id)
        {
            var favouritePublisher = await _context.FavouritePublisher.FindAsync(id);
            if (favouritePublisher == null)
            {
                return NotFound();
            }

            _context.FavouritePublisher.Remove(favouritePublisher);
            await _context.SaveChangesAsync();

            return favouritePublisher;
        }

        private bool FavouritePublisherExists(int id)
        {
            return _context.FavouritePublisher.Any(e => e.Id == id);
        }
    }
}
