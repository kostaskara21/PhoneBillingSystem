using System.ComponentModel.DataAnnotations;

namespace TeleProgram.Models
{
    public class Phones
    {
        [Key]
        public string PhoneNumber { get; set; }


        //Foreign key pointing to the Programs table 
        public string ProgrameName { get; set; }

        public Programs Programs { get; set; }


        public List<Bills> Bills { get; set; }

        
        public string? UserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
     
    }
}
