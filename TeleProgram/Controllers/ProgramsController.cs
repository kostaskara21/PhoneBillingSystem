using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeleProgram.Interfaces;
using TeleProgram.Models;

namespace TeleProgram.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ProgramsController : Controller
    {
        private readonly ApplicationDbContext _context;

        //This is for refactoring the code using Repository Pattern 
        private readonly IPrograms _programs;

        public ProgramsController(ApplicationDbContext context, IPrograms programs)
        {
            _context = context;
            _programs = programs;
        }

        // GET: Programs
        public async Task<IActionResult> Index()
        {
            var programs = await _programs.Index();
            return View(programs);
        }


        // GET: Programs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programs = await _context.Programs
                .FirstOrDefaultAsync(m => m.ProgrameName == id);
            if (programs == null)
            {
                return NotFound();
            }

            return View(programs);
        }

        // GET: Programs/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProgrameName,Description,Charge")] Programs programs)
        {
           
            _context.Add(programs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }


        // GET: Programs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programs = await _context.Programs.FindAsync(id);
            if (programs == null)
            {
                return NotFound();
            }
            return View(programs);
        }


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ProgrameName,Description,Charge")] Programs programs)
        {
            if (id != programs.ProgrameName)
            {
                return NotFound();
            }

            
            try
            {
                _context.Update(programs);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgramsExists(programs.ProgrameName))
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

        // GET: Programs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programs = await _context.Programs
                .FirstOrDefaultAsync(m => m.ProgrameName == id);
            if (programs == null)
            {
                return NotFound();
            }

            return View(programs);
        }

        // POST: Programs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var programs = await _context.Programs.FindAsync(id);
            if (programs != null)
            {
                _context.Programs.Remove(programs);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgramsExists(string id)
        {
            return _context.Programs.Any(e => e.ProgrameName == id);
        }
    }
}
