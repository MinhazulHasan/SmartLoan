using System.ComponentModel.DataAnnotations;

namespace SmartLoan.Models
{
    public class GroupName
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
