using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeleProgram.Models;

namespace TeleProgram.Controllers
{
    public class BillsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BillsController(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }


        //Only the client Can Pay his bills
        [Authorize(Roles = "Client")]

        [HttpPost]
        public async Task<IActionResult> Edit(string phonenumber)
        {
            var bill = await _context.Bills.FirstOrDefaultAsync(x=>x.PhoneNumber==phonenumber);
            if (bill == null)
            {
                return NotFound();
            }
            bill.Cost = 0;
            _context.Remove(bill);
            var status=await _context.SaveChangesAsync();
            if (status >0)
            {
                TempData["Complete"] = "Bill Payied";
            }
            return RedirectToAction("Index");
        }


        //Only the client can see his bills
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Index()
        {

            var userid = await  _userManager.GetUserAsync(User);
            if (userid == null)
            {
                return NotFound();
            }
            
            var id = userid.Id;
            var phone = await _context.Phones.FirstOrDefaultAsync(x => x.UserId.Equals(id));
            var phoneonly = phone.PhoneNumber;

            var calls = await _context.Bills
                   .Where(x => x.PhoneNumber.Equals(phoneonly))  // Filters records with the matching phone number
                   .ToListAsync();  // Converts the result into a list

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
            //we are giving the phone number of the client 
            //from the Phone we can identify the ProgramName(UserId is Fk in the table of Phones since each user has a specific Phone)
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
            var status=_context.SaveChanges();
            if (status > 0)
            {
                TempData["Found"] = "Bill Created";
            }
            return View();


        }


    }
}
