using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.DTO
{
    public class PagingRequest
    {
        /// <summary>
        /// Kích thước trang
        /// </summary>
        public int? PageSize { get; set; }

        /// <summary>
        /// Trang số bao nhiêu
        /// </summary>
        public int? PageNumber { get; set; }

        /// <summary>
        /// Điều kiện lọc
        /// </summary>
        public string? EmployeeFilter { get; set; }
    }
}
