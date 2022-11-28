using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Constants
{
    /// <summary>
    /// Lớp lưu store procedure
    /// </summary>
    /// Created by: MDLONG(30/10/2022)
    public class Procedure
    {

        #region Field
        /// <summary>
        /// Format tên của procedure lấy tất cả bản ghi
        /// </summary>
        public static string GET_ALL = "Proc_{0}_SelectAll";

        /// <summary>
        /// Format tên của procedure lấy bản ghi theo id
        /// </summary>
        public static string GET_BY_ID = "Proc_{0}_SelectByID";

        /// <summary>
        /// Format tên của procedure lấy bản ghi theo điều kiện
        /// </summary>
        public static string GET_BY_FILTER = "Proc_{0}_SelectFilter";

        /// <summary>
        /// Kiểm tra mã trùng
        /// </summary>
        public static string CHECK_DUPPLICATED_CODE = "Proc_{0}_CheckDupplicatedCode";

        /// <summary>
        /// Format tên của procedure thêm bản ghi
        /// </summary>
        public static string INSERT_ONE = "Proc_{0}_Insert";

        /// <summary>
        /// Format tên của procedure cập nhật bản ghi
        /// </summary>
        public static string UPDATE_BY_ID = "Proc_{0}_Update";

        /// <summary>
        /// Format tên của procedure xóa 1 bản ghi theo id
        /// </summary>
        public static string DELETE_BY_ID = "Proc_{0}_DeleteByID";

        /// <summary>
        /// Format tên của procedure xóa NHIỀU bản ghi theo id
        /// </summary>
        public static string DELETE_BY_IDS = "Proc_{0}_DeleteByIds";
        #endregion
    }
}
