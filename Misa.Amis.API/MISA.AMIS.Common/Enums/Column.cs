using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Enums
{
    /// <summary>
    /// Cột bảng excel
    /// </summary>
    public class Column
    {
        /// <summary>
        /// Tên cột
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Thứ tự
        /// </summary>
        public int Order { get; set; }
    }
}
