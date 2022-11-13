using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.API.Controllers;
using MISA.AMIS.BL;
using MISA.AMIS.Common.DTO;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Enums;
using MySqlConnector;
using System;
using System.Data;
using System.Data.Common;
using System.Transactions;

namespace Misa.Amis.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class JobPositionsController : BasesController<JobPosition>
    {
        #region Field
        private IJobPositionBL _jobpositionBL;
        #endregion

        #region Constructor
        public JobPositionsController(IJobPositionBL jobpositionBL) : base(jobpositionBL)
        {
            _jobpositionBL = jobpositionBL;
        }
        #endregion
    }
}
