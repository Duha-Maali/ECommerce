
namespace Ecommerce.Data.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        void AddEmployee(Entities.Employee employee);
        Entities.Employee GetEmployeeById(int id);
        List<Entities.Employee> GetAllEmployees();
        void UpdateEmployee(Entities.Employee employee);
        void DeleteEmployee(int id);
    }
}
