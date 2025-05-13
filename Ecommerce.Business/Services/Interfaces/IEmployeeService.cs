using Ecommerce.Business.ViewModels;

namespace Ecommerce.Business.Services.Interfaces
{
    public interface IEmployeeService
    {
        void AddEmployee(EmployeeViewModel viewModel);
        EmployeeViewModel GetEmployeeById(int id);
        List<EmployeeViewModel> GetAllEmployees();
        void UpdateEmployee(EmployeeViewModel viewModel);
        void DeleteEmployee(int id);
    }
}
