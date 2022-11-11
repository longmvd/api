using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Common.Entities;
using MySqlConnector;
using Dapper;
using MISA.AMIS.DL;
using MISA.AMIS.BL.EmployeeBL;

namespace Misa.Amis.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        #region Field
        private IEmployeeBL _employeeBL;
        private readonly string connectionString = "Server=localhost;Database=misa.web09.ctm.mdlong;Uid=root;Pwd=123456;";
        #endregion

        #region Constructor
        public EmployeesController(IEmployeeBL employeeBL)
        {
            _employeeBL = employeeBL;
        }
        #endregion


        /// <summary>
        /// Lấy tất cả bản ghi của nhân viên
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllEmployee()
        {
            try
            {
                var employees = _employeeBL.GetAllEmployee();
                return StatusCode(StatusCodes.Status200OK, employees);


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 1,
                    DevMsg = "Get data from database failed!",
                    UserMsg = "Có lỗi xảy ra, vui lòng liên hệ Misa!",
                    MoreInfo = "",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }

        /// <summary>
        /// API Thêm mới nhân viên
        /// </summary>
        /// <param name="employee"> Đối tượng nhân viên cần thêm mới</param>
        /// <returns></returns>
        /// CreatedBy: MDLONG
        [HttpPost]
        public IActionResult InsertEmployee([FromBody] Employee employee)
        {
            return StatusCode(StatusCodes.Status201Created, Guid.NewGuid());
        }

        /// <summary>
        /// API sửa nhân viên theo ID
        /// </summary>
        /// <param name="employeeId">ID nhân viên muốn sửa</param>
        /// <param name="employee">Thông tin cần sửa</param>
        /// <returns></returns>
        [HttpPut("{employeeId}")]
        public IActionResult UpdateEmployee(
            [FromRoute] Guid employeeId,
            [FromBody] Employee employee)
        {
            return StatusCode(StatusCodes.Status200OK, employeeId);

        }

        /// <summary>
        /// Xóa nhân viên theo ID
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>ID nhân viên</returns>
        [HttpDelete("{employeeId}")]
        public IActionResult DeleteEployee([FromRoute] Guid employeeId)
        {
            return StatusCode(StatusCodes.Status200OK, employeeId);
        }
    }
}
