namespace SmartLoan.Models
{
    public class MemberCollection
    {
        public string CollectorName { get; set; }
        public DateTime? CollectionDate { get; set; }
        public double CollectionAmount { get; set; }
        public double CashPayment { get; set; }
        public double OnlinePayment { get; set; }
        public double DuePayment { get; set; }
    }
}
