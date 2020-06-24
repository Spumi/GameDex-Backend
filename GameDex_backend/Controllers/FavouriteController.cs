using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameDex_backend;
using GameDex_backend.Models;
using System.Diagnostics;

namespace GameDex_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteController : ControllerBase
    {
        private readonly UserContext _context;

        public FavouriteController(UserContext context)
        {
            _context = context;
        }

        // GET: api/Favourite
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Favourite>>> GetFavourite()
        {
            return await _context.Favourite.ToListAsync();
        }

        // GET: api/Favourite/5
        [HttpGet("{userid}")]
        public async Task<ActionResult<Favourite>> GetFavourite(int userid)
        {
            var user = await _context.Users.Include(u => u.FavGames).Where(x => x.Id == userid).SingleOrDefaultAsync();
            

            if (user == null)
            {
                return NotFound();
            }
            
            return Ok(user.FavGames);
        }

        // PUT: api/Favourite/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavourite(int id, Favourite favourite)
        {
            if (id != favourite.Id)
            {
                return BadRequest();
            }

            _context.Entry(favourite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavouriteExists(id))
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

        // POST: api/Favourite
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Favourite>> PostFavourite(Favourite favourite)
        {
            _context.Attach(favourite);
            //User user = _context.Users.Include(p => p.FavGames).FirstOrDefault();
            User user = _context.Users.Where(u => u.Id == favourite.UserId).Include(p => p.FavGames).FirstOrDefault();
            _context.Attach(user);
            if (user.Auth_token == favourite.Auth_token)
            {               
            }
            
            _context.Entry(user).Collection(e => e.FavGames).IsModified = true;
            
            _context.Entry(user).State = EntityState.Modified;
            _context.Users.Update(user).Entity.FavGames.Add(favourite);
            
            await _context.SaveChangesAsync();

            return new JsonResult(user) ;
        }

        // DELETE: api/Favourite/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Favourite>> DeleteFavourite(int id)
        {
            var favourite = await _context.Favourite.FindAsync(id);
            if (favourite == null)
            {
                return NotFound();
            }

            _context.Favourite.Remove(favourite);
            await _context.SaveChangesAsync();

            return favourite;
        }

        private bool FavouriteExists(int id)
        {
            return _context.Favourite.Any(e => e.Id == id);
        }
    }
}
