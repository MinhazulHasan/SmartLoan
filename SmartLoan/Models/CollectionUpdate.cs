namespace SmartLoan.Models
{
    public class CollectionUpdate
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public int Route { get; set; }
        public byte PaymentId { get; set; }
    }
}
