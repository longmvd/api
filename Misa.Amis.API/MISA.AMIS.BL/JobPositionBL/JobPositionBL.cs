using MISA.AMIS.Common.Entities;
using MISA.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL
{
    public class JobPositionBL : BaseBL<JobPosition>, IJobPositionBL
    {
        private IJobPositionDL _jobpositiontDL;
        public JobPositionBL(IJobPositionDL jobpositiontDL) : base(jobpositiontDL)
        {
            _jobpositiontDL = jobpositiontDL;
        }
    }
}
