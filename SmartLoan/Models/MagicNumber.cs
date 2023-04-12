namespace SmartLoan.Models
{
    public class MagicNumber
    {
        public static readonly byte Single = 1;
        public static readonly byte Married = 2;
        public static readonly byte Devorced = 3;

        public static readonly byte Due = 1;
        public static readonly byte PartiallyCollected = 2;
        public static readonly byte Collected = 3;

        public static readonly byte CashPayment = 1;
        public static readonly byte DigitalPayment = 2;
    }
}
