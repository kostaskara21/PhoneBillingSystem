using System.ComponentModel.DataAnnotations;

namespace TeleProgram.Models
{
    public class Calls
    {
        public int Id { get; set; }

        [Required]
        public string CalledPhoneNumber { get; set; }

        [Required]
        public double Duration { get; set; }

        //Foreign key for the Bills 
        public int BillsId { get; set; }
        public Bills Bills { get; set; }
    }
}
