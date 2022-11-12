using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.DTO
{
    public class PagingRequest
    {
        public int PageSize { get; set; }
        
        public int PageNumber { get; set; }

        public string EmployeeFilter { get; set; }

        public Guid DepartmentID { get; set; }

        public Guid JobPositionID { get; set; }
    }
}
