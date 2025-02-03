using System.ComponentModel.DataAnnotations;

namespace muzikaletleristok.Models
{
    public class Logincs
    {
        [Key]
        public string Email { get; set; }
        public string PassWord { get; set; }
        public bool LoggedStatus
        {
            get; set;

        }
    }
}
