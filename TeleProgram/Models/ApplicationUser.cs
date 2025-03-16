
using System.ComponentModel.DataAnnotations;

namespace TeleProgram.Models
{
    public class ApplicationUser 
    {

        [Required]
        public string FirstName { get; set; }
            
        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string AFM { get; set; }

        
        
    }
}
