using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TeleProgram.Models
{
    public class ApplicationUser :IdentityUser
    {

        //This for all the users
        [Required]
        public string FirstName { get; set; }
            
        
        [Required]
        public string LastName { get; set; }
        

        //This is for the Customer
        [Required]
        public string AFM { get; set; }


        public String? PhoneNumber {  get; set; }
        public Phones? Phones { get; set; }  
        
    }
}
