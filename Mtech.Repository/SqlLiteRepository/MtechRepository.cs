
namespace Mtech.Repository.SqlLiteRepository
{
    public class MtechRepository : IMtechRepository
    {
        public MtechRepository(MtechDbContext dbContext)
        {
            Employees = new EmployeeRepository(dbContext);
        }

        public IEmployeeRepository Employees { get; set; }
    }
}
