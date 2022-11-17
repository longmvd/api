using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.DTO
{
    public class EmployeeDTO : Employee
    {
        #region Properties
        public String DepartmentName { get; set; }

        public String JobPositionName { get; set; }
        #endregion
    }
}
