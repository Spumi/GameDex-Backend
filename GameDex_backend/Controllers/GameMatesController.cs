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
    public class GameMatesController : ControllerBase
    {
        private readonly UserContext _context;

        public GameMatesController(UserContext context)
        {
            _context = context;
        }

        // GET: api/GameMates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameMate>>> GetGameMate()
        {
            return await _context.GameMate.ToListAsync();
        }

        // GET: api/GameMates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameMate>> GetGameMate(int id)
        {
            var gameMate = await _context.GameMate.FindAsync(id);

            if (gameMate == null)
            {
                return NotFound();
            }

            return gameMate;
        }

        // PUT: api/GameMates/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameMate(int id, GameMate gameMate)
        {
            if (id != gameMate.Id)
            {
                return BadRequest();
            }

            _context.Entry(gameMate).State = EntityState.Modified;

            try
            {
                gameMate.IsAccepted = true;
                User user = await _context.Users.Include(m => m.GameMates).Where(m => m.Id == id).FirstOrDefaultAsync();
                User friend = await _context.Users.Include(m => m.GameMates).Where(m => m.Id == gameMate.UserId).FirstOrDefaultAsync();
                var friendRequest = user.GameMates.Find(m => m.UserId == gameMate.UserId);
                friendRequest.IsAccepted = true;
                friend.GameMates.Add(new GameMate() { UserId = user.Id, IsAccepted = true});

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameMateExists(id))
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

        // POST: api/GameMates
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<GameMate>> PostGameMate(GameMate gameMate)
        {
            gameMate.IsAccepted = false;
            _context.GameMate.Add(gameMate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGameMate", new { id = gameMate.Id }, gameMate);
        }

        // DELETE: api/GameMates/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GameMate>> DeleteGameMate(int id)
        {
            var gameMate = await _context.GameMate.FindAsync(id);
            if (gameMate == null)
            {
                return NotFound();
            }

            _context.GameMate.Remove(gameMate);
            await _context.SaveChangesAsync();

            return gameMate;
        }

        private bool GameMateExists(int id)
        {
            return _context.GameMate.Any(e => e.Id == id);
        }
    }
}
