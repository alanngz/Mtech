using Microsoft.EntityFrameworkCore;
using Mtech.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtech.Repository.SqlLiteRepository
{
    public class MtechDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public MtechDbContext(DbContextOptions<MtechDbContext> options) : base(options)
        {
            
        }

    }
}
