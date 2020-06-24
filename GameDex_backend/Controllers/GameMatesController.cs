using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameDex_backend;
using GameDex_backend.Models;

namespace GameDex_backend.Controllers
{
    public class GameMatesController : Controller
    {
        private readonly UserContext _context;

        public GameMatesController(UserContext context)
        {
            _context = context;
        }

        // GET: GameMates
        public async Task<IActionResult> Index()
        {
            return View(await _context.GameMate.ToListAsync());
        }

        // GET: GameMates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameMate = await _context.GameMate
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameMate == null)
            {
                return NotFound();
            }

            return View(gameMate);
        }

        // GET: GameMates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GameMates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,IsAccepted")] GameMate gameMate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameMate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gameMate);
        }

        // GET: GameMates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameMate = await _context.GameMate.FindAsync(id);
            if (gameMate == null)
            {
                return NotFound();
            }
            return View(gameMate);
        }

        // POST: GameMates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,IsAccepted")] GameMate gameMate)
        {
            if (id != gameMate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameMate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameMateExists(gameMate.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(gameMate);
        }

        // GET: GameMates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameMate = await _context.GameMate
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameMate == null)
            {
                return NotFound();
            }

            return View(gameMate);
        }

        // POST: GameMates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameMate = await _context.GameMate.FindAsync(id);
            _context.GameMate.Remove(gameMate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameMateExists(int id)
        {
            return _context.GameMate.Any(e => e.Id == id);
        }
    }
}
