﻿using MISA.AMIS.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Entities
{
    public class Employee : BaseEntity
    {
        #region Properties
        /// <summary>
        /// Id nhân viên
        /// </summary>
        [Key]
        public Guid EmployeeID { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [Required(ErrorMessage = "Mã nhân viên không được để trống")]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [Required(ErrorMessage = "Tên nhân viên không được để trống")]
        public string EmployeeName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính nhân viên
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Email nhân viên
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Số điện thoại nhân viên
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        public string? TelephoneNumber { get; set; }

        /// <summary>
        /// Số chứng minh nhân dân
        /// </summary>
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp chứng minh nhân dân
        /// </summary>
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Địa điểm cấp chứng minh thư
        /// </summary>
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// Lương
        /// </summary>
        public Double? Salary { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        public string? BankAccountNumber { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }

        /// <summary>
        /// Tên chi nhánh
        /// </summary>
        public string? BankBranchName { get; set; }

        /// <summary>
        /// Tên tỉnh/thành phố ngân hàng
        /// </summary>
        public string? BankProvinceName { get; set; }

        /// <summary>
        /// Ngày vào công ty
        /// </summary>
        public DateTime? JoinDate { get; set; }

        /// <summary>
        /// Tình trạng công việc
        /// </summary>
        public int? WorkStatus { get; set; }

        /// <summary>
        /// Id phòng ban
        /// </summary>
        [Required(ErrorMessage="Phòng ban không để trống")]
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// Vị trí
        /// </summary>
        public string? PositionName { get; set; }

        /// <summary>
        /// Là nhà cung cấp
        /// </summary>
        public bool? IsSupplier { get; set; } 

        /// <summary>
        /// Là khách hàng
        /// </summary>
        public bool? IsCustomer { get; set; }
        #endregion
    }
}
