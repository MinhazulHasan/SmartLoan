using System.ComponentModel.DataAnnotations;

namespace SmartLoan.Models
{
    public class PaymentMethod
    {
        [Key]
        public byte Id { get; set; }
        public string MethodName { get; set; }
    }
}
