using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Enums
{
    /// <summary>
    /// Tên các cột
    /// </summary>
    /// Create by: MDLONG(20/11/2022)
    public class EmployeeField
    {
        /// <summary>
        /// Thứ tự cột
        /// </summary>
        public Column ColumnOrder { get; } = new Column { Name = "STT", Order = 0};

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public Column EmployeeCode { get; } = new Column { Name = "Mã nhân viên", Order = 1};

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public Column EmployeeName { get; } = new Column { Name = "Tên nhân viên", Order = 2};

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public Column DateOfBirth { get; } = new Column {Name = "Ngày sinh", Order = 3};

        /// <summary>
        /// Giới tính nhân viên
        /// </summary>
        public Column Gender { get; } = new Column {Name = "Giới tính", Order = 4};

        /// <summary>
        /// Vị trí
        /// </summary>
        public Column PositionName { get; } = new Column {Name = "Vị trí", Order = 5};

        /// <summary>
        /// Phòng ban
        /// </summary>
        public Column DepartmentName { get; } = new Column {Name = "Phòng ban", Order = 6};

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public Column Address { get; } = new Column {Name = "Địa chỉ", Order = 7};

        /// <summary>
        /// Email nhân viên
        /// </summary>
        public Column Email { get; } = new Column {Name = "Email", Order = 8};

        /// <summary>
        /// Số điện thoại nhân viên
        /// </summary>
        public Column PhoneNumber { get; } = new Column {Name = "Điện thoại", Order = 9};

        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        public Column TelephoneNumber { get; } = new Column {Name = "Điện thoại cố định", Order = 10};

        /// <summary>
        /// Số chứng minh nhân dân
        /// </summary>
        public Column IdentityNumber { get; } = new Column {Name = "Số chứng minh nhân dân", Order = 11};

        /// <summary>
        /// Ngày cấp chứng minh nhân dân
        /// </summary>
        public Column IdentityDate { get; } = new Column {Name = "Ngày cấp", Order = 12};

        /// <summary>
        /// Địa điểm cấp chứng minh thư
        /// </summary>
        public Column IdentityPlace { get; } = new Column {Name = "Nơi cấp", Order = 13};

        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        public Column BankAccountNumber { get; } = new Column {Name = "Tài khoản ngân hàng", Order = 14};

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public Column BankName { get; } = new Column {Name = "Tên ngân hàng", Order = 15};

        /// <summary>
        /// Tên chi nhánh
        /// </summary>
        public Column BankBranchName { get; } = new Column {Name = "Chi nhánh", Order = 16};

        /// <summary>
        /// Tên tỉnh/thành phố ngân hàng
        /// </summary>
        public Column BankProvinceName { get; } = new Column {Name = "Tên tỉnh thành phố chi nhánh", Order = 17};

        /// <summary>
        /// Ngày vào công ty
        /// </summary>
        public Column JoinDate { get; } = new Column {Name = "Ngày tham gia", Order = 18};

    }
}
