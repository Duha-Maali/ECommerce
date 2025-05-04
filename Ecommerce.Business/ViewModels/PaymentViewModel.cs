using Ecommerce.Data.Enums;

namespace Ecommerce.Business.ViewModels
{
    public class PaymentViewModel
    {
        public PaymentType Type { get; set; }
        public decimal Amount { get; set; }
        public string TransactionID { get; set; }
    }
}
