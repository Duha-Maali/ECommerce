
namespace Ecommerce.Business.ViewModels
{
    public class ShoppingCartViewModel
    {
        public int CartID { get; set; }
        public int CustomerID { get; set; }
        public List<ProductViewModel> Products { get; set; }
        public ShoppingCartViewModel()
        {
            Products = new List<ProductViewModel>();
        }
    }
}
