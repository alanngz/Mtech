using Mtech.Repository;
using Mtech.Repository.SqlLiteRepository;
using Mtech.Shared;
using System.Text.RegularExpressions;

namespace Mtech.Application
{
    public class EmployeeService
    {
        private readonly IMtechRepository mtechRepository;

        public EmployeeService(IMtechRepository mtechRepository)
        {
            this.mtechRepository = mtechRepository;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            // Validate uniqueness of employee based on RFC
            var existingEmployee = await mtechRepository.Employees.GetByRfcAsync(employee.RFC);
            if (existingEmployee != null)
            {
                throw new ArgumentException("An employee with the same RFC already exists.");
            }

            // Validate RFC format and length
            if (!IsValidRFC(employee.RFC))
            {
                throw new ArgumentException("Invalid RFC format or length.");
            }

            // Persist employee to data store
            return await mtechRepository.Employees.AddAsync(employee);
        }

        public async Task UpdateEmployeeAsync(int id, Employee employee)
        {
            var existingEmployee = await GetAsync(id);

            if (existingEmployee == null)
            {
                throw new ArgumentException("Invalid employee id");
            }

            // Validate RFC format and length
            if (!IsValidRFC(employee.RFC))
            {
                throw new ArgumentException("Invalid RFC format or length.");
            }

            // Validate uniqueness of employee based on RFC
            var existingRfcEmployee = await mtechRepository.Employees.GetByRfcAsync(employee.RFC);
            if (existingRfcEmployee != null && existingEmployee.ID != existingRfcEmployee.ID)
            {
                throw new ArgumentException("An employee with the same RFC already exists.");
            }

            await mtechRepository.Employees.UpdateAsync(employee);
        }

        public async Task<List<Employee>> GetSortedByBornDateAsync(string? filter = null)
        {
            return await mtechRepository.Employees.GetSortedByBornDateAsync(filter);
        }

        public async Task<Employee> GetAsync(int id)
        {
            return await mtechRepository.Employees.GetByIdAsync(id);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await mtechRepository.Employees.GetByIdAsync(id);
            if (employee == null)
            {
                throw new ArgumentException($"Employee with ID {id} not found");
            }

            await mtechRepository.Employees.DeleteAsync(id);
        }


        private static bool IsValidRFC(string rfc)
        {
            // RFC validation logic
            string pattern = @"^[A-ZÑ&]{3,4}\d{6}[A-V1-9][A-Z\d]{2}\b";

            if (string.IsNullOrEmpty(rfc))
                return false;

            return Regex.IsMatch(rfc, pattern);
        }
    }

}