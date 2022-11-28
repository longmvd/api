using MISA.AMIS.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.DTO
{
    /// <summary>
    /// Lớp mã lỗi
    /// </summary>
    /// Created by: MDLONG(29/10/2022)
    public class ErrorResult
    {
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public ErrorCode ErrorCode { get; set; }

        /// <summary>
        /// Thông tin lỗi cho dev đọc
        /// </summary>
        public string DevMsg { get; set; }

        /// <summary>
        /// Thông tin lỗi cho người dùng đọc
        /// </summary>
        public string UserMsg { get; set; }

        /// <summary>
        /// Thông tin thêm
        /// </summary>
        public string MoreInfo { get; set; }

        /// <summary>
        /// id truy vết lỗi
        /// </summary>
        public string TraceId { get; set; } 


    }
}
