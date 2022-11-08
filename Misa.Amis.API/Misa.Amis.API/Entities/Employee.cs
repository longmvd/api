using Misa.Amis.API.Enums;

namespace Misa.Amis.API.Entities
{
    public class Employee
    {
        #region Properties
        public Guid EmployeeId { get; set; }

        public string EmployeeCode { get; set; }

        public string EmployeeName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string TelephoneNumber { get; set; }

        public string TaxCode { get; set; }

        public Double Salary { get; set; }

        public string BankAccount { get; set; }

        public string BankName { get; set; }

        public string BankBranchName { get; set; }

        public string BankProvinceName { get; set; }

        public DateTime JoinDate { get; set; }

        public int WorkSatus { get; set; }

        public DateTime IdentityDate { get; set; }

        public string IdentityPlace { get; set; }

        public string IdentityNumber { get; set; }

        public Department Department { get; set; }

        public JobPosition JobPosition { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set; } 
        #endregion






    }
}
