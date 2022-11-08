namespace Misa.Amis.API.Entities
{
    public class JobPosition
    {
        public Guid JobPositionID { get; set; }

        public string JobPositionCode { get; set; }

        public string JobPositionName { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
    }
}