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

        public Task<Programs> Create(Programs programs)
        {
            throw new NotImplementedException();
        }

        public Task<Programs?> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Programs?> Details(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Programs?> Edit(string id, Programs programs)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Programs>> Index()
        {
            
            
            return await _context.Programs.ToListAsync();
        }
    }


}
