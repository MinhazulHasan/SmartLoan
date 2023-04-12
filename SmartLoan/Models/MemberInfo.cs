using System.ComponentModel.DataAnnotations;

namespace SmartLoan.Models
{
    public class MemberInfo
    {
        public int Id { get; set; }

        public int? GroupId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "Father's Name")]
        [StringLength(100)]
        public string? FatherName { get; set; }
        
        [Display(Name = "Mother's Name")]
        [StringLength(100)]
        public string? MotherName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "National ID Card Number")]
        public string Nid { get; set;}

        [StringLength(50)]
        [Display(Name = "Passport Number")]
        public string? PassportNum { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime? BirthDate { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

        [Display(Name = "Mobile Number")]
        public string? MobileNumber { get; set; }

        public string? Occupation { get; set; }

        [Display(Name = "Last Educational Degree")]
        public string? HeighestEducationalLevel { get; set;}


        [Display(Name = "Name of the Institution")]
        public string? EducationalInstituteName { get; set;}

        public MarritialStatus MarritialStatus { get; set; }

        [Required]
        [Display(Name = "Maritial Status")]
        public byte MarritialStatusId { get; set; }

        [Display(Name = "Spouse Name")]
        public string? SpouseName { get; set; }

        [Display(Name = "Spouse Phone Number")]
        public string? SpousePhone { get; set; }

        [StringLength(50)]
        [Display(Name = "Spouse National ID Card Number")]
        public string? SpouseNid { get; set; }

        [Display(Name = "Spouse Occupation")]
        public string? SpouseOccupation { get; set; }

        [Display(Name = "Number of Family Member")]
        [Range(1, 50)]
        public byte? FamilyMember { get; set; }

        [Display(Name = "Earning Member in Family")]
        [Range(1, 50)]
        public byte EarningMember { get; set; }

        [Display(Name = "Total Earning of Family in a Month")]
        [Range(1, 10000000, ErrorMessage = "Family earning per month should be betweeen 1 to 1,00,000")]
        public double FamilyTotalEarningPerMonth { get; set; }
    }
}
