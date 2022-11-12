using Dapper;
using MISA.AMIS.Common.DTO;
using MISA.AMIS.Common.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL
{
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {
        public PagingResult<Employee> GetByFilter(PagingRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
