using GameDex_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameDex_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteDevelopersController : ControllerBase
    {
        private readonly UserContext _context;

        public FavouriteDevelopersController(UserContext context)
        {
            _context = context;
        }

        // GET: api/FavouriteDevelopers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavouriteDeveloper>>> GetFavouriteDeveloper()
        {
            return await _context.FavouriteDeveloper.ToListAsync();
        }

        // GET: api/FavouriteDevelopers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FavouriteDeveloper>> GetFavouriteDeveloper(int id)
        {
            var user = await _context.Users.Include(u => u.FavDeveloper).Where(x => x.Id == id).SingleOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.FavPublishers);
        }

        // PUT: api/FavouriteDevelopers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavouriteDeveloper(int id, FavouriteDeveloper favouriteDeveloper)
        {
            if (id != favouriteDeveloper.Id)
            {
                return BadRequest();
            }

            _context.Entry(favouriteDeveloper).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavouriteDeveloperExists(id))
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

        // POST: api/FavouriteDevelopers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<FavouriteDeveloper>> PostFavouriteDeveloper(FavouriteDeveloper favouriteDeveloper)
        {
            User user = _context.Users.Where(u => u.Id == favouriteDeveloper.UserId).Include(p => p.FavPublishers).FirstOrDefault();
            _context.Attach(user);
            _context.FavouriteDeveloper.Add(favouriteDeveloper);
            _context.Users.Update(user).Entity.FavDeveloper.Add(favouriteDeveloper);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFavouriteDeveloper", new { id = favouriteDeveloper.Id }, favouriteDeveloper);
        }

        // DELETE: api/FavouriteDevelopers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FavouriteDeveloper>> DeleteFavouriteDeveloper(int id)
        {
            var favouriteDeveloper = await _context.FavouriteDeveloper.FindAsync(id);
            if (favouriteDeveloper == null)
            {
                return NotFound();
            }

            _context.FavouriteDeveloper.Remove(favouriteDeveloper);
            await _context.SaveChangesAsync();

            return favouriteDeveloper;
        }

        private bool FavouriteDeveloperExists(int id)
        {
            return _context.FavouriteDeveloper.Any(e => e.Id == id);
        }
    }
}