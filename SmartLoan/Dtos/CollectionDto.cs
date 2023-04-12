using System.ComponentModel.DataAnnotations;

namespace SmartLoan.Dtos
{
    public class CollectionDto
    {
        public DateTime? CollectionDate { get; set; }
        [Range(1,int.MaxValue,ErrorMessage = "Id can't be less than 1")]
        public int GroupNameId { get; set; }
        public string CollectorId { get; set; }
        public string CollectionId { get; set; }
    }
}
