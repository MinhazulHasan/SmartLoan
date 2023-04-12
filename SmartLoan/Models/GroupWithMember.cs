using System.ComponentModel.DataAnnotations;

namespace SmartLoan.Models
{
    public class GroupWithMember
    {
        [Key]
        public int Id { get; set; }
        public GroupInfo? GroupInfo { get; set; }
        public int? GroupInfoId { get; set; }
        public int MemberId { get; set; }
    }
}
