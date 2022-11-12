using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Enums
{

    /// <summary>
    /// Enum sử dụng mô tả các lỗi xảy ra khi gọi API
    /// </summary>
    internal enum ErrorCode
    {

        /// <summary>
        /// Lỗi gặp Exception
        /// </summary>
        Exception = 1,

        /// <summary>
        /// Lỗi trùng mã
        /// </summary>
        DuplicateCode = 2,

        /// <summary>
        /// Lỗi đầu vào không hợp lệ
        /// </summary>
        InvalidData = 3
    }
}
