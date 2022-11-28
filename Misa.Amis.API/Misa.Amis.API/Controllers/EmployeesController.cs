using ClosedXML.Excel;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.API.Controllers;
using MISA.AMIS.BL;
using MISA.AMIS.Common;
using MISA.AMIS.Common.DTO;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Enums;
using MySqlConnector;
using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Resources;
using System.Transactions;

namespace Misa.Amis.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BasesController<Employee>
    {
        #region Field
        private IEmployeeBL _employeeBL;
        #endregion

        #region Constructor
        public EmployeesController(IEmployeeBL employeeBL) : base(employeeBL)
        {
            _employeeBL = employeeBL;
        }
        #endregion

        [HttpGet("filter")]
        public IActionResult GetByFilter([FromQuery] PagingRequest request)
        {

            try
            {
                return StatusCode(StatusCodes.Status200OK, _employeeBL.GetByFilter(request));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.UserMsg_Exception,
                    MoreInfo = "",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }

        [HttpGet("excel")]
        public IActionResult ExportEmployeesToExcel([FromQuery] PagingRequest request)
        {
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    var table = _employeeBL.ExportToExcel(request);

                    table.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh_sach_nhan_vien " + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss") + ".xlsx");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.UserMsg_Exception,
                    MoreInfo = Resource.MoreInfor,
                    TraceId = HttpContext.TraceIdentifier
                });
            }

        }
    }
}
