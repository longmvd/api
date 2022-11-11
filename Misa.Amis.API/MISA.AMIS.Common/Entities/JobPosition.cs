using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Entities
{
    public class JobPosition : BaseEntity
    {
        public Guid JobPositionID { get; set; }

        public string JobPositionCode { get; set; }

        public string JobPositionName { get; set; }

        public string Description { get; set; }
    }
}
