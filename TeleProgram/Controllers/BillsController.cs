using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeleProgram.Models;

namespace TeleProgram.Controllers
{
    public class BillsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BillsController(ApplicationDbContext context)
        {
            _context = context;
        }



        // GET: BillsController/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: BillsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(String phonenumber)
        {
            var programName = await _context.Phones.FirstOrDefaultAsync(o => o.PhoneNumber == phonenumber);
           
            if (programName == null)
            {
                TempData["NotFoundProgramName"] = "There is Not Such A Number Try again";
                return View();
            }
            var proprogramName = programName.ProgrameName;
            var cost = await _context.Programs.FirstOrDefaultAsync(p => p.ProgrameName == proprogramName);
            var charge = cost.Charge;

            Bills bills = new Bills();



            bills.PhoneNumber = phonenumber;
            bills.Cost = charge;



            _context.Add(bills);
            _context.SaveChanges();
            return View();


        }


    }
}
