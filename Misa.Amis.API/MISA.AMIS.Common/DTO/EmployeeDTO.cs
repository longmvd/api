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
    public class EmployeeDTO : BaseEntity
    {
        #region Properties
        [Key]
        public Guid EmployeeId { get; set; }

        public string EmployeeCode { get; set; }

        public string EmployeeName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string TelephoneNumber { get; set; }

        public string IdentityNumber { get; set; }

        public DateTime IdentityDate { get; set; }

        public string IdentityPlace { get; set; }

        public string TaxCode { get; set; }

        public Double Salary { get; set; }

        public string BankAccountNumber { get; set; }

        public string BankName { get; set; }

        public string BankBranchName { get; set; }

        public string BankProvinceName { get; set; }

        public DateTime JoinDate { get; set; }

        public int WorkSatus { get; set; }

        public Department Department { get; set; }

        public JobPosition JobPosition { get; set; }
        #endregion
    }
}
