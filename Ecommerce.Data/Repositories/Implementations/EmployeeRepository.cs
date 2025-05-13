using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Ecommerce.Data.Entities;
using Ecommerce.Data.Repositories.Interfaces;
using Ecommerce.Data.Utilities;

namespace Ecommerce.Data.Repositories.Implementations
{
    public class EmployeeRepository : Interfaces.IEmployeeRepository
    {
        private const string FileName = "employees.json";
        private readonly List<Employee> _employees;
        public EmployeeRepository()
        {
            try
            {
                var employeeList = FileHandler.ReadJson<List<Employee>>(FileName);
                if (employeeList == null)
                {
                    _employees = new List<Employee>();
                }
                else
                {
                    _employees = employeeList;
                }
            }
            catch (IOException ex)
            {
                throw new IOException($"Failed to read file {FileName}: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception($"Failed to deserialize JSON from {FileName}: {ex.Message}", ex);
            }
        }

        public void AddEmployee(Employee employee)
        {
            if (_employees.Any(e => e.EmployeeID == employee.EmployeeID))
            {
                throw new Exception($"Employee with this ID {employee.EmployeeID} already exists.");
            }
            _employees.Add(employee);
            SaveChanges();
        }

        public void DeleteEmployee(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.EmployeeID == id);
            if (employee == null)
            {
                throw new Exception("Employee not found");
            }
            _employees.Remove(employee);
            SaveChanges();
        }

        public List<Employee> GetAllEmployees()
        {
            return _employees;
        }

        public Employee GetEmployeeById(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.EmployeeID == id);
            return employee;
        }

        public void UpdateEmployee(Employee updatedEmployee)
        {
            var existingEmployee = _employees.FirstOrDefault(e => e.EmployeeID == updatedEmployee.EmployeeID);
            if (existingEmployee == null)
            {
                throw new Exception("Employee not found.");
            }
            existingEmployee.Name = updatedEmployee.Name;
            SaveChanges();
        }
        private void SaveChanges()
        {
            try
            {
                FileHandler.WriteJson(FileName, _employees);
            }
            catch (IOException ex)
            {
                throw new IOException($"Failed to write to file {FileName}: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception($"Failed to serialize JSON to {FileName}: {ex.Message}", ex);
            }
        }
    }
}
