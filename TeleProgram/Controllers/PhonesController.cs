using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using TeleProgram.Models;

namespace TeleProgram.Controllers
{
    [Authorize(Roles ="Seller")]
    public class PhonesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PhonesController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> ChangeClientProgram()
        {
            return View();   
        }


        [HttpPost]
        public async Task<IActionResult> ChangeClientProgram(string UserId)
        {
            if (string.IsNullOrEmpty(UserId))
            {

                TempData["NotFound"] = "Email Is Required";
                return View(); 
            }

            // Find user by email or username 
            var user = await _userManager.FindByNameAsync(UserId) ?? await _userManager.FindByEmailAsync(UserId);

            if (user == null)
            {
                // case where the user is not found
                TempData["NotFound"] = "There is Not such a User";
                return View(); 
            }

            
            var phone = await _context.Phones.FirstOrDefaultAsync(o => o.UserId == user.Id);
            
            if (phone == null)
            {

                TempData["NotFound"] = "Not User Found ";
                return View(); // Return with error
            }
            

            // Redirect to the Edit page with the phone ID (or any other unique property from phone)
            return RedirectToAction("Edit", new { id = phone.PhoneNumber });
        }




        // GET: Phones/Create
        public IActionResult Create()
        {
            
            ViewData["ProgrameName"] = new SelectList(_context.Programs, "ProgrameName", "ProgrameName");
            return View();
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhoneNumber,ProgrameName,UserId")] Phones phones)
        {
            var phone = await _context.Phones.FindAsync(phones.PhoneNumber);
            if (phone != null) {
                TempData["error1"] = "Phone Already Exists";
                ViewData["ProgrameName"] = new SelectList(_context.Programs, "ProgrameName", "ProgrameName", phones.ProgrameName);
                return View(phones);
            }
            
            var user = await _userManager.FindByNameAsync(phones.UserId);
           
            if (user != null)
            {
                var uid = await _userManager.GetUserIdAsync(user);
                phones.UserId = uid;
                _context.Add(phones);
                await _context.SaveChangesAsync();
                TempData["NotError"] = "Phone Registed";
            }
            else
            {
                TempData["error"] = "There is no such a user";
                ViewData["ProgrameName"] = new SelectList(_context.Programs, "ProgrameName", "ProgrameName", phones.ProgrameName);
                return View(phones);
            }
            ViewData["ProgrameName"] = new SelectList(_context.Programs, "ProgrameName", "ProgrameName", phones.ProgrameName);
            return View(phones);
        }



        // GET: Phones/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                TempData["NotFound"] = "There is No Such an Email";
            }

            var phones = await _context.Phones.FindAsync(id);
            if (phones == null)
            {
                TempData["NotFound"] = "There is No Such an Email";
                return View("ChangeClientProgram");
            }
           
            ViewData["ProgrameName"] = new SelectList(_context.Programs, "ProgrameName", "ProgrameName", phones.ProgrameName);
            return View(phones);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PhoneNumber,ProgrameName,UserId")] Phones phones)
        {
            if (id != phones.PhoneNumber)
            {
                TempData["NotFound"] = "There is No Such an Email";
               
            }
            //Here we are finding the Phone Number of the User that we want 
            var phone = await _context.Phones.FindAsync(id);
            //From the Phones table we find the programe name of the phone(so the prgrame name of the user we gave)
            //We are assigning the new Programe Name to the users Progame name
            phone.ProgrameName = phones.ProgrameName;
           
            _context.Phones.Update(phone);
            await _context.SaveChangesAsync();
            TempData["Complete"] = "Changes Completed Sucessfully";

            return RedirectToAction(nameof(ChangeClientProgram));
           
        }

        

        private bool PhonesExists(string id)
        {
            return _context.Phones.Any(e => e.PhoneNumber == id);
        }
    }
}
