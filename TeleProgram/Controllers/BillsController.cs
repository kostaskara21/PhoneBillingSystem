using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeleProgram.Interfaces;
using TeleProgram.Models;

namespace TeleProgram.Controllers
{
    public class BillsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBills _bills;

        public BillsController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IBills bills)
        {
            _userManager = userManager;
            _context = context;
            _bills = bills;
        }


        //Only the client Can Pay his bills
        [Authorize(Roles = "Client")]

        [HttpPost]
        public async Task<IActionResult> Edit(string phonenumber)
        {

            var bill = await _bills.Delete(phonenumber);
            if (!bill)
            {
                return NotFound();
            }
            if (bill)
            {
                TempData["Complete"] = "Bill Payied";
            }
            return RedirectToAction("Index");
        }



        //Only the client can see his bills
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Index()
        {

            var userid = await _userManager.GetUserAsync(User);
            if (userid == null)
            {
                return NotFound();
            }

            var id = userid.Id;


            var calls = await _bills.Index(id);
            if (calls == null)
            {
                return NotFound();
            }
            
            return View(calls);
            

        }




        //Only a seller can CREATE a bill for a client 
        [Authorize(Roles = "Seller")]
        // GET: BillsController/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: BillsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> Create(String phonenumber)
        {
            var b= await _bills.Create(phonenumber);

            //we are giving the phone number of the client 
            //from the Phone we can identify the ProgramName(UserId is Fk in the table of Phones since each user has a specific Phone)
            var programName = await _context.Phones.FirstOrDefaultAsync(o => o.PhoneNumber == phonenumber);
           
            if (!b)
            {
                TempData["NotFoundProgramName"] = "There is Not Such A Number Try again";
                return View();
            }
            else {
                TempData["Found"] = "Bill Created";
            }
            return View();


        }


    }
}
