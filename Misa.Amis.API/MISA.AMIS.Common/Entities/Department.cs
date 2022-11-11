namespace MISA.AMIS.Common.Entities
{
    public class Department : BaseEntity
    {
        #region Properties
        public Guid DepartmentID { get; set; }

        public string DepartmentCode { get; set; }

        public string DepartmentName { get; set; }

        public string Description { get; set; }
        #endregion

    }
}
