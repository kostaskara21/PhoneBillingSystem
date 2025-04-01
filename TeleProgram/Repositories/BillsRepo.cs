using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeleProgram.Interfaces;
using TeleProgram.Models;

namespace TeleProgram.Repositories
{
    public class BillsRepo : IBills
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;



        public BillsRepo(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        public async Task<Boolean> Create(string PhoneNumber)
        {
            //we are giving the phone number of the client 
            //from the Phone we can identify the ProgramName(UserId is Fk in the table of Phones since each user has a specific Phone)
            var programName = await _context.Phones.FirstOrDefaultAsync(o => o.PhoneNumber == PhoneNumber);

            if (programName == null)
            {
                
                return false;
            }
            var proprogramName = programName.ProgrameName;
            var cost = await _context.Programs.FirstOrDefaultAsync(p => p.ProgrameName == proprogramName);
            var charge = cost.Charge;

            Bills bills = new Bills();


            bills.PhoneNumber = PhoneNumber;
            bills.Cost = charge;



            _context.Add(bills);
            var status = _context.SaveChanges();
            if (status > 0)
            {
                return true;
                
            }
            return true;
        }

        public async Task<Boolean> Delete(string phonenumber)
        {
            var bill = await _context.Bills.FirstOrDefaultAsync(x => x.PhoneNumber == phonenumber);
            if (bill == null)
            {
                return false;
            }
            bill.Cost = 0;
            _context.Remove(bill);
            var status = await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Bills>> Index(string  id)
        {
           
            var phone = await _context.Phones.FirstOrDefaultAsync(x => x.UserId.Equals(id));

            if (phone.PhoneNumber == null)
            {
                return null;
            }
            var phoneonly = phone.PhoneNumber;
            var calls = await _context.Bills
                   .Where(x => x.PhoneNumber.Equals(phoneonly))  
                   .ToListAsync();  

            return calls;
            
        }
    }
}
