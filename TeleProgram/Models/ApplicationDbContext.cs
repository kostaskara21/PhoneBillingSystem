using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace TeleProgram.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {


        }

        DbSet<Programs> Programs { get; set; }
        DbSet<Phones> Phones { get; set; }

        DbSet<Bills> Bills { get; set; }
        DbSet<Calls> Calls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //One Phone can have be in only one program but one program can have multiple phones
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Phones>()
                .HasOne(p => p.Programs)
                .WithMany(t => t.Phones)
                .HasForeignKey(z => z.ProgrameName);


            //One Phone can also have many bills but one bill can be only to one phone 
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Bills>()
                .HasOne(p => p.Phones)
                .WithMany(t => t.Bills)
                .HasForeignKey(z => z.PhoneNumber);


           
            //One Bill can have many calls but one call can only be in one bill since after the call is finished can not be added to another bill 
            modelBuilder.Entity<Calls>()
                .HasOne(c => c.Bills)  
                .WithMany(b => b.Calls)  
                .HasForeignKey(c => c.BillsId);


            //One PhoneNumber Belongs to one user and one user can have only one phoneNumber so one to one relationship 
            modelBuilder.Entity<Calls>()
                .HasOne(c => c.Bills)
                .WithMany(b => b.Calls)
                .HasForeignKey(c => c.BillsId);



            //Configure the one-to-one relationship between ApplicationUser and Phone one user can have one phone and only one phone can belong to one user
            modelBuilder.Entity<Phones>()
                .HasOne(p => p.ApplicationUser) 
                .WithOne(u => u.Phones) 
                .HasForeignKey<Phones>(p => p.UserId) 
                .OnDelete(DeleteBehavior.SetNull); 

        }



    }
}
