namespace Misa.Amis.API.Entities
{
    public class Department
    {
        #region Properties
        public Guid DepartmentID { get; set; }

        public string DepartmentCode { get; set; }

        public string DepartmentName { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set; } 

        public string Description { get; set; }
        #endregion

    }
}
