using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mtech.Repository
{
    public interface IMtechRepository
    {
        public IEmployeeRepository Employees { get; set; }
    }
}
