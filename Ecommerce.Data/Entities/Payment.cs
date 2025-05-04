using Ecommerce.Data.Enums;

namespace Ecommerce.Data.Entities
{
    public class Payment
    {
        public PaymentType Type { get; set; }
        public decimal Amount { get; set; }
        public string TransactionID { get; set; }
    }
}
