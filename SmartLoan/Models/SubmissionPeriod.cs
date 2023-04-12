using System.ComponentModel.DataAnnotations;

namespace SmartLoan.Models
{
    public class SubmissionPeriod
    {
        [Key]
        public int id { get; set; }

        public string Title { get; set; }

        public int TimeInDays { get; set;}

    }
}
