using Ecommerce.Business.ViewModels;
using Ecommerce.Data.Entities;
using Ecommerce.Data.Repositories.Implementations;
using Ecommerce.Data.Repositories.Interfaces;

namespace Ecommerce.Business.Services.Implementations
{
    public class EmployeeService : Interfaces.IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void AddEmployee(EmployeeViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel), "EmployeeViewModel cannot be null");
            }
            var employee = new Employee
            {
                EmployeeID = viewModel.EmployeeID,
                Name = viewModel.Name
            };
            _employeeRepository.AddEmployee(employee);
        }

        public void DeleteEmployee(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid employee ID.", nameof(id));
            }
            _employeeRepository.DeleteEmployee(id);
        }

        public List<EmployeeViewModel> GetAllEmployees()
        {
            var employees = _employeeRepository.GetAllEmployees();
            var employeeViewModels = new List<EmployeeViewModel>();
            foreach (var employee in employees)
            {
                employeeViewModels.Add(new EmployeeViewModel
                {
                    EmployeeID = employee.EmployeeID,
                    Name = employee.Name
                });
            }
            return employeeViewModels;
        }

        public EmployeeViewModel GetEmployeeById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid employee ID.", nameof(id));
            }
            try
            {
                var employee = _employeeRepository.GetEmployeeById(id);
                if (employee == null)
                {
                    return null;
                }
                return new EmployeeViewModel
                {
                    EmployeeID = employee.EmployeeID,
                    Name = employee.Name
                };
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error retrieving employee: {ex.Message}");
                throw;
            }
        }

        public void UpdateEmployee(EmployeeViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel), "EmployeeViewModel cannot be null");
            }
            var employee = new Employee
            {
                EmployeeID = viewModel.EmployeeID,
                Name = viewModel.Name
            };
            _employeeRepository.UpdateEmployee(employee);
        }
    }
}
