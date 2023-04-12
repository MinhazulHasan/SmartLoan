using System.ComponentModel.DataAnnotations;

namespace SmartLoan.Models
{
    public class UsersLoginSession
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public bool Active { get; set; } = false;
    }
}
