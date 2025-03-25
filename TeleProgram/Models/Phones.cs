using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TeleProgram.Models
{
    public class Phones
    {
        [DisplayName("Phone Number")]
        [Key]
        public string PhoneNumber { get; set; }

        [DisplayName("Program Name")]
        //Foreign key pointing to the Programs table 
        public string ProgrameName { get; set; }

        public Programs Programs { get; set; }


        public List<Bills> Bills { get; set; }


        [DisplayName("Email")]
        public string? UserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
     
    }
}
