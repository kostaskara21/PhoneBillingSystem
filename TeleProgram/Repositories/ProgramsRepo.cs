using Microsoft.EntityFrameworkCore;
using System;
using TeleProgram;
using TeleProgram.Interfaces;
using TeleProgram.Models;

namespace TeleProgram.Repositories
{
    public class ProgramsRepo : IPrograms
    {
        private readonly ApplicationDbContext _context;

        

        public ProgramsRepo(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public async Task<Boolean> Create(Programs programs)
        {
            _context.Add(programs);
            var status=await _context.SaveChangesAsync();
            if (status > 0)
            {
                return true;
            }
            else { return false; }

        }

        public async Task<Programs?> Delete(string id)
        {
            if (id == null)
            {
                return null;
            }

            var programs = await _context.Programs
                .FirstOrDefaultAsync(m => m.ProgrameName == id);

            if (programs == null)
            {
                return null;
            }

            _context.Programs.Remove(programs);

            await _context.SaveChangesAsync();
            return programs;
        }



        public async Task<Programs?> Details(string id)
        {
            if (id == null)
            {
                return null;
            }

            var programs = await _context.Programs
                .FirstOrDefaultAsync(m => m.ProgrameName == id);
            if (programs == null)
            {
                return null;
            }
            return programs;
        }

        public async Task<Programs?> Edit(string id, string desc, decimal charg) { 
            if (id == null)
            {
                return null;
            }

            var Programs = await _context.Programs.FindAsync(id);
            if (Programs == null)
            {
                return null;
            }
            Programs.Description = desc;
            Programs.Charge = charg;    
             _context.Update(Programs);
            await _context.SaveChangesAsync();
            
            return Programs;
        }


        public async Task<List<Programs>> Index()
        {
            
            
            return await _context.Programs.ToListAsync();
        }
    }


}
