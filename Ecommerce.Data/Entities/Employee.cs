
namespace Ecommerce.Data.Entities
{
    public class Employee : Person
    {
        //EmployeeID is Alias for the inherited ID property, giving more specific name for employee context 
        public int EmployeeID
        {
            get { return ID; }
            set { ID = value; }
        }
    }
}
