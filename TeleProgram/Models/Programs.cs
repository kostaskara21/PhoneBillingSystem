using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TeleProgram.Models
{
    public class Programs
    {
        [Key]
        [DisplayName("Progran Name")]
        public string ProgrameName { get; set; }

        [Required]
        public string Description { get;set; }

        [Required]
        public decimal Charge { get; set; }


        public List<Phones> Phones { get; set; }
       

    }
}
