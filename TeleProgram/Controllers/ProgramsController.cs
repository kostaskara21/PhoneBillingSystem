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
      
        //This is for refactoring the code using Repository Pattern 
        private readonly IPrograms _programs;

        public ProgramsController(ApplicationDbContext context, IPrograms programs)
        {
           
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
            var programs = await _programs.Details(id);
            if(programs==null)
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

            if (await _programs.Create(programs))
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
           
        }



        // GET: Programs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ProgrameName,Description,Charge")] Programs programs)
        {
            programs = await _programs.Edit(id,programs.Description,programs.Charge);
            if (programs == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index),programs);
                      
        }



        // GET: Programs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            return View();
        }

        // POST: Programs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _programs.Delete(id);
            return RedirectToAction(nameof(Index));
        }

       
    }
}
