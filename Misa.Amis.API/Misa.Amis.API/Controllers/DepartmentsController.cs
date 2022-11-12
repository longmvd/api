using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.BL;
using MISA.AMIS.Common.Entities;
using MySqlConnector;
using System;
using System.Data;
using System.Data.Common;
using System.Transactions;

namespace Misa.Amis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        #region Field
        private IDepartmentBL _departmentBL;
        #endregion

        #region Constructor
        public DepartmentsController(IDepartmentBL departmentBL)
        {
            _departmentBL = departmentBL;
        }
        #endregion


        /// <summary>
        /// Lấy thông tin tất cả phòng ban
        /// </summary>
        /// <returns>Danh sách phòng ban</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var departments = _departmentBL.GetAll();

                //xử lý kết quả trả về
                if (departments != null)
                {
                    return StatusCode(StatusCodes.Status200OK, departments);
                }
                return StatusCode(StatusCodes.Status200OK, new List<Department>());

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
        /// Lấy thông tin 1 phòng ban theo id
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpGet("{departmentId}")]
        public IActionResult GetOneById([FromRoute] Guid departmentId)
        {
            try
            {
                var department = _departmentBL.GetByID(departmentId);

                if (department != null)
                {
                    return StatusCode(StatusCodes.Status200OK, department);

                }
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    ErrorCode = 1,
                    DevMsg = "",
                    UserMsg = "Không tìm thấy phòng ban",
                    MoreInfo = "",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 1,
                    DevMsg = "Cactched an error",
                    UserMsg = "Có lỗi xảy ra vui lòng liên hệ Misa!",
                    MoreInfo = e.Message,
                    TraceId = HttpContext.TraceIdentifier
                });

            }
        }

        /// <summary>
        /// API thêm mới phòng ban
        /// </summary>
        /// <param name="department">Thông tin phòng ban cần thêm</param>
        /// <returns>ID phòng ban được thêm</returns>
        [HttpPost]
        public IActionResult InsertDepartment([FromBody] Department department)
        {
            try
            {
                Guid result = _departmentBL.InsertOne(department);
                if (!result.Equals(Guid.Empty))
                    return StatusCode(StatusCodes.Status201Created, result);
                else
                    return StatusCode(StatusCodes.Status501NotImplemented, new
                    {
                        ErrorCode = 1,
                        DevMsg = "Insert data from database failed!",
                        UserMsg = "Thêm phòng ban thất bại",
                        MoreInfo = "",
                        TraceId = HttpContext.TraceIdentifier
                    });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 1,
                    DevMsg = "Insert data from database failed!",
                    UserMsg = "Thêm phòng ban thất bại",
                    MoreInfo = e.Message,
                    TraceId = HttpContext.TraceIdentifier
                });

            }

        }

        /// <summary>
        /// API sửa thông tin phòng ban
        /// </summary>
        /// <param name="depaCodertment">ID phòng ban</paCodem>
        /// <param name="department">Thông tin cần sửa</para, department.DepartmentNamem>;
        /// <returns>ID phòng ban đã sửa</returns>
        [HttpPut("{departmentId}")]
        public IActionResult UpdateDepartment([FromRoute] Guid departmentId, [FromBody] Department department)
        {
            if (departmentId == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    ErrorCode = 1,
                    DevMsg = "Không tồn tại phòng ban",
                    UserMsg = "Không tồn tại phòng ban cần xóa",
                    MoreInfo = "",
                    TraceId = HttpContext.TraceIdentifier

                });
            }
            using (var mySqlConnection = new MySqlConnection(""))
            {
                string sqlCommand = "Proc_department_SelectById";
                var parameter = new DynamicParameters();
                parameter.Add("@DepartmentID", departmentId);
                var oldDepartment = mySqlConnection.QueryFirstOrDefault<Department>(sqlCommand, parameter, commandType: CommandType.StoredProcedure);
                mySqlConnection.Open();
                var transaction = mySqlConnection.BeginTransaction();
                try
                {


                    string storedProcedure = "Proc_department_Update";

                    parameter.Add("@DepartmentCode", department.DepartmentCode);
                    parameter.Add("@DepartmentName", department.DepartmentName);
                    parameter.Add("@Description", department?.Description);
                    parameter.Add("@CreatedDate", oldDepartment.CreatedDate);
                    parameter.Add("@CreatedBy", oldDepartment.CreatedBy);
                    parameter.Add("@ModifiedDate", DateTime.Now);
                    parameter.Add("@ModifiedBy", department.ModifiedBy);


                    var numberOfRow = mySqlConnection.Execute(storedProcedure, parameter, transaction, commandType: CommandType.StoredProcedure);
                    transaction.Commit();
                    if (numberOfRow > 0)
                    {
                        return StatusCode(StatusCodes.Status200OK, numberOfRow);
                    }
                    return StatusCode(StatusCodes.Status400BadRequest, new
                    {
                        ErrorCode = 1,
                        DevMsg = "There's no department",
                        UserMsg = "Phòng ban không tồn tại",
                        MoreInfo = "",
                        TraceId = HttpContext.TraceIdentifier
                    });


                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    transaction.Rollback();
                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        ErrorCode = 1,
                        DevMsg = "Catch error",
                        UserMsg = "Có lỗi xảy ra vui lòng liên hệ Misa",
                        MoreInfo = e.Message,
                        TraceId = HttpContext.TraceIdentifier
                    });
                }
            }
        }

        /// <summary>
        /// Xóa thông tin 1 phòng ban theo id
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpDelete("{departmentId}")]
        public IActionResult DeleteOneById([FromRoute] Guid departmentId)
        {
            try
            {
                var result = _departmentBL.DeleteOneByID(departmentId);

                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, result);

                }
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    ErrorCode = 1,
                    DevMsg = "",
                    UserMsg = "Không tìm thấy phòng ban",
                    MoreInfo = "",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 1,
                    DevMsg = "Cactched an error",
                    UserMsg = "Có lỗi xảy ra vui lòng liên hệ Misa!",
                    MoreInfo = e.Message,
                    TraceId = HttpContext.TraceIdentifier
                });

            }
        }
    }
}
