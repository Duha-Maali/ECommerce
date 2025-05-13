
namespace Ecommerce.Data.Entities
{
    public class Customer : Person
    {
        //CustomerID is Alias for the inherited ID property, giving more specific name for customer context 
        public int CustomerID
        {
            get { return ID; }
            set { ID = value; }
        }
    }
}
