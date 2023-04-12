using System.ComponentModel.DataAnnotations;

namespace SmartLoan.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Title { get; set; }
    }
}
