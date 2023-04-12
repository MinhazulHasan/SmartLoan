namespace SmartLoan.Models
{
    public class CollectionReport
    {
        public List<MemberCollection> MemberCollection { get; set; }
        public string GroupName { get; set; }
        public double TotalDeposit { get; set; }
        public double TotalOnlinePay { get; set; }
        public double TotalPayInCash { get; set; }
        public double TotalDue { get; set; }
    }
}
