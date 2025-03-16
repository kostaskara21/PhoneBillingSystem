using System.ComponentModel.DataAnnotations;

namespace TeleProgram.Models
{
    public class Bills
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Coast { get; set; }



        //Foreign Key pointing to  the Phones table
        public  string PhoneNumber { get; set; }

        public Phones Phones { get; set; }

        
        public List<Calls> Calls { get; set; }

    }
}
