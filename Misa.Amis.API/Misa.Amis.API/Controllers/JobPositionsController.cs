using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Common.Entities;
using MySqlConnector;
using System.Data;

namespace Misa.Amis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPositionsController : ControllerBase
    {

        private readonly string connectionString = "Server=localhost;Database=misa.web09.ctm.mdlong;Uid=root;Pwd=123456;";

        /// <summary>
        /// Lấy tất cả bản ghi của bị trí
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllJobPositon()
        {

            return StatusCode(StatusCodes.Status200OK,
                new List<JobPosition> {
                    new JobPosition{JobPositionID = Guid.NewGuid(), JobPositionCode = "J001", JobPositionName = "Giám đốc" },
                    new JobPosition{JobPositionID = Guid.NewGuid(), JobPositionCode = "J002", JobPositionName = "Giám đốc" },
                    new JobPosition{JobPositionID = Guid.NewGuid(), JobPositionCode = "J003", JobPositionName = "Giám đốc" }});
        }


        /// <summary>
        /// Lấy thông tin 1 vị trí theo id
        /// </summary>
        /// <param name="jobPositionId"></param>
        /// <returns></returns>
        [HttpGet("{jobPositionId}")]
        public IActionResult GetOneById([FromRoute] Guid jobPositionId)
        {
            try
            {
                var mySqlConnection = new MySqlConnection(connectionString);
                string storedProcedure = "Proc_jobPosition_SelectByID";
                var parameters = new DynamicParameters();
                parameters.Add("@JobPositionID", jobPositionId);
                var jobPosition = mySqlConnection.QueryFirstOrDefault<JobPosition>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

                if (jobPosition != null)
                {
                    return StatusCode(StatusCodes.Status200OK, jobPosition);

                }
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    ErrorCode = 1,
                    DevMsg = "",
                    UserMsg = "Không tìm thấy vị trí",
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
        /// API thêm mới vị trí
        /// </summary>
        /// <param name="jobPosition">Thông tin vị trí cần thêm</param>
        /// <returns>ID vị trí được thêm</returns>
        [HttpPost]
        public IActionResult InsertJobPosition([FromBody] JobPosition jobPosition)
        {
            try
            {

                var mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();
                var transaction = mySqlConnection.BeginTransaction();

                try
                {
                    Guid id = Guid.NewGuid();
                    string storedProcedureName = "Proc_jobPosition_Insert";
                    var parameters = new DynamicParameters();
                    parameters.Add("@JobPositionID", id);
                    parameters.Add("@JobPositionCode", jobPosition.JobPositionCode);
                    parameters.Add("@JobPositionName", jobPosition.JobPositionName);
                    parameters.Add("@CreatedDate", jobPosition.CreatedDate);
                    parameters.Add("@CreatedBy", jobPosition.CreatedBy);
                    parameters.Add("@ModifiedDate", jobPosition.ModifiedDate);
                    parameters.Add("@ModifiedBy", jobPosition.ModifiedBy);
                    parameters.Add("@Description", jobPosition.Description);

                    int result = mySqlConnection.Execute(storedProcedureName, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);
                    transaction.Commit();
                    if (result > 0)
                        return StatusCode(StatusCodes.Status201Created, id);
                    else
                        return StatusCode(StatusCodes.Status501NotImplemented, new
                        {
                            ErrorCode = 1,
                            DevMsg = "Insert data from database failed!",
                            UserMsg = "Thêm vị trí thất bại",
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
                        UserMsg = "Thêm vị trí thất bại",
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
        /// API sửa thông tin vị trí
        /// </summary>
        /// <param name="depaCodertment">ID vị trí</paCodem>
        /// <param name="jobPosition">Thông tin cần sửa</para, jobPosition.JobPositionNamem>;
        /// <returns>ID vị trí đã sửa</returns>
        [HttpPut("{jobPositionId}")]
        public IActionResult UpdateJobPosition([FromRoute] Guid jobPositionId, [FromBody] JobPosition jobPosition)
        {
            if (jobPositionId == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    ErrorCode = 1,
                    DevMsg = "Không tồn tại vị trí",
                    UserMsg = "Không tồn tại vị trí cần xóa",
                    MoreInfo = "",
                    TraceId = HttpContext.TraceIdentifier

                });
            }
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                string sqlCommand = "Proc_jobPosition_SelectById";
                var parameter = new DynamicParameters();
                parameter.Add("@JobPositionID", jobPositionId);
                var oldJobPosition = mySqlConnection.QueryFirstOrDefault<JobPosition>(sqlCommand, parameter, commandType: CommandType.StoredProcedure);
                mySqlConnection.Open();
                var transaction = mySqlConnection.BeginTransaction();
                try
                {


                    string storedProcedure = "Proc_jobPosition_Update";

                    parameter.Add("@JobPositionCode", jobPosition.JobPositionCode);
                    parameter.Add("@JobPositionName", jobPosition.JobPositionName);
                    parameter.Add("@Description", jobPosition?.Description);
                    parameter.Add("@CreatedDate", oldJobPosition.CreatedDate);
                    parameter.Add("@CreatedBy", oldJobPosition.CreatedBy);
                    parameter.Add("@ModifiedDate", DateTime.Now);
                    parameter.Add("@ModifiedBy", jobPosition.ModifiedBy);


                    var numberOfRow = mySqlConnection.Execute(storedProcedure, parameter, transaction, commandType: CommandType.StoredProcedure);
                    transaction.Commit();
                    if (numberOfRow > 0)
                    {
                        return StatusCode(StatusCodes.Status200OK, numberOfRow);
                    }
                    return StatusCode(StatusCodes.Status400BadRequest, new
                    {
                        ErrorCode = 1,
                        DevMsg = "There's no jobPosition",
                        UserMsg = "vị trí không tồn tại",
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
        /// Xóa thông tin 1 vị trí theo id
        /// </summary>
        /// <param name="jobPositionId"></param>
        /// <returns></returns>
        [HttpDelete("{jobPositionId}")]
        public IActionResult DeleteOneById([FromRoute] Guid jobPositionId)
        {
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                mySqlConnection.Open();
                var transaction = mySqlConnection.BeginTransaction();
                try
                {
                    string storedProcedure = "Proc_jobPosition_DeleteByID";
                    var parameters = new DynamicParameters();
                    parameters.Add("@JobPositionID", jobPositionId);
                    int numberOfRow = mySqlConnection.Execute(storedProcedure, parameters, transaction, commandType: CommandType.StoredProcedure);
                    transaction.Commit();
                    if (numberOfRow > 0)
                        return StatusCode(StatusCodes.Status200OK, numberOfRow);

                    return StatusCode(StatusCodes.Status400BadRequest, new
                    {
                        ErrorCode = 1,
                        DevMsg = "There's no jobPosition",
                        UserMsg = "Không tồn tại vị trí cần xóa",
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
    }
}
