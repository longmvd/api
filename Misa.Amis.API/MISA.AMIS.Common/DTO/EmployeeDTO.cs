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
    /// <summary>
    /// Lớp dto trả dữ liệu nhân viên cho client
    /// </summary>
    /// Created by: MDLONG(30/10/2022)
    public class EmployeeDTO : Employee
    {
        #region Properties
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public String DepartmentName { get; set; }
        #endregion
    }
}
