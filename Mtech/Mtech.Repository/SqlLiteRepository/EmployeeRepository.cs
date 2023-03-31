using Microsoft.EntityFrameworkCore;
using Mtech.Shared;

namespace Mtech.Repository.SqlLiteRepository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MtechDbContext _context;

        public EmployeeRepository(MtechDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.ID == id);
        }

        public async Task<Employee> GetByRfcAsync(string rfc)
        {
            var employee = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.RFC == rfc);
            return employee;
        }

        public async Task<List<Employee>> GetSortedByBornDateAsync(string filter = null)
        {
            IQueryable<Employee> query = _context.Employees.AsNoTracking()
                .Where(e => string.IsNullOrEmpty(filter) || e.Name.Contains(filter) || e.LastName.Contains(filter))
                .OrderBy(e => e.BornDate);

            return await query.ToListAsync();
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await GetByIdAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }
    }
}
