namespace SmartLoan.Dtos
{
    public class OfficeCollectionDto
    {
        public int GroupInfoId { get; set; }
        public int CollectionId { get; set; }
        public string SubmitterId { get; set; }
        public double Amount { get; set; }
        public string CollectorId { get; set; }
        public int PaymentMethod { get; set; }
    }
}
