using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.Amis.API.Entities;
using MySqlConnector;
using System.Data;
using System.Transactions;

namespace Misa.Amis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        /// <summary>
        /// Lấy thông tin tất cả phòng ban
        /// </summary>
        /// <returns>Danh sách phòng ban</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                string connectionString = "Server=localhost;Database=misa.web09.ctm.mdlong;Uid=root;Pwd=123456;";
                var mySqlConnection = new MySqlConnection(connectionString);
                string sqlCommand = "SELECT * FROM department";
                var departments = mySqlConnection.Query(sqlCommand);

                return StatusCode(StatusCodes.Status200OK, departments);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    ErrorCode = 1,
                    ErrorMessage = e.Message
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
                string connectionString = "Server=localhost;Database=misa.web09.ctm.mdlong;Uid=root;Pwd=123456;";
                var mySqlConnection = new MySqlConnection(connectionString);
                string storedProcedure = "Proc_department_SelectByID";
                var parameters = new DynamicParameters();
                parameters.Add("@DepartmentID", departmentId);
                var department = mySqlConnection.QueryFirstOrDefault<Department>(storedProcedure,parameters, commandType: CommandType.StoredProcedure);

                return StatusCode(StatusCodes.Status200OK, department);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    ErrorCode = 1,
                    DevMsg = "",
                    UserMsg = "Không tìm thấy phòng ban",
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

                string connectionString = "Server=localhost;Database=misa.web09.ctm.mdlong;Uid=root;Pwd=123456;";
                var mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();
                var transaction = mySqlConnection.BeginTransaction();

                try
                {
                    Guid id = Guid.NewGuid();
                    string storedProcedureName = "Proc_department_Insert";
                    var parameters = new DynamicParameters();
                    parameters.Add("@DepartmentID", id);
                    parameters.Add("@DepartmentCode", department.DepartmentCode);
                    parameters.Add("@DepartmentName", department.DepartmentName);
                    parameters.Add("@CreatedDate", department.CreatedDate);
                    parameters.Add("@CreatedBy", department.CreatedBy);
                    parameters.Add("@ModifiedDate", department.ModifiedDate);
                    parameters.Add("@ModifiedBy", department.ModifiedBy);
                    parameters.Add("@Description", department.Description);

                    int result = mySqlConnection.Execute(storedProcedureName, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);
                    transaction.Commit();        
                    if(result > 0)
                        return StatusCode(StatusCodes.Status201Created, id);
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
                    transaction.Rollback();
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
                finally
                {
                    mySqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 1,
                    DevMsg = ""
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
            return StatusCode(StatusCodes.Status200OK, departmentId);
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
                string connectionString = "Server=localhost;Database=misa.web09.ctm.mdlong;Uid=root;Pwd=123456;";
                var mySqlConnection = new MySqlConnection(connectionString);
                string storedProcedure = "Proc_department_DeleteByID";
                var parameters = new DynamicParameters();
                parameters.Add("@DepartmentID", departmentId);
                int numberOfRow = mySqlConnection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

                return StatusCode(StatusCodes.Status200OK, numberOfRow);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    ErrorCode = 1,
                    DevMsg = "",
                    UserMsg = "Phòng ban không tồn tại",
                    MoreInfo = e.Message,
                    TraceId = HttpContext.TraceIdentifier
                });

            }
        }
    }
}
