using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.BL;
using MISA.AMIS.Common;
using MISA.AMIS.Common.Attributes;
using MISA.AMIS.Common.DTO;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Enums;
using MISA.AMIS.DL;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace MISA.AMIS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {
        #region Field
        private IBaseBL<T> _baseBL;
        #endregion

        #region Constructor
        public BasesController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
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
                var bases = _baseBL.GetAll();

                //xử lý kết quả trả về
                if (bases != null)
                {
                    return StatusCode(StatusCodes.Status200OK, bases);
                }
                return StatusCode(StatusCodes.Status200OK, new List<T>());

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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

        /// <summary>
        /// Lấy thông tin 1 phòng ban theo id
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns>1 bản ghi</returns>
        /// Author: MDLONG(12/11/2022)
        [HttpGet("{recordId}")]
        public IActionResult GetOneByID([FromRoute] Guid recordId)
        {
            try
            {
                var entity = _baseBL.GetByID(recordId);

                if (entity != null)
                {
                    return StatusCode(StatusCodes.Status200OK, entity);

                }
                return StatusCode(StatusCodes.Status404NotFound, new ErrorResult
                {
                    ErrorCode = ErrorCode.NotFound,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.UserMsg_Not_Found,
                    MoreInfo = "",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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

        /// <summary>
        /// Lấy mã lớn nhất
        /// </summary>
        /// <returns>1 bản ghi</returns>
        /// Author: MDLONG(12/11/2022)
        [HttpGet("TheBiggestCode")]
        public IActionResult GetTheBiggestCode()
        {
            try
            {
                var code = _baseBL.GetTheBiggestCode();
                return StatusCode(StatusCodes.Status200OK, code);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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


        /// <summary>
        /// API thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Thông tin cần thêm</param>
        /// <returns>ID phòng ban được thêm</returns>
        /// Author: MDLONG(12/11/2022)
        [HttpPost]
        public IActionResult InsertBase([FromBody] T entity)
        {
            try
            {
                var response = _baseBL.InsertOne(entity, ModelState);
                if (response.Success)
                {
                    return StatusCode((int)response.StatusCode, response.Data);
                }
                else
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return StatusCode((int)response.StatusCode, new ErrorResult
                        {
                            ErrorCode = ErrorCode.DuplicateCode,
                            DevMsg = Resource.Dev_Dupplicated_Code,
                            UserMsg = Resource.UserMsg_Dupplicated_Code,
                            MoreInfo = response.Data,
                            TraceId = HttpContext.TraceIdentifier
                        });
                    }

                    return StatusCode((int)response.StatusCode, new ErrorResult
                    {
                        ErrorCode = ErrorCode.InvalidData,
                        DevMsg = Resource.DevMsg_Exception,
                        UserMsg = Resource.UserMsg_Invalid_Data,
                        MoreInfo = response.Data,
                        TraceId = HttpContext.TraceIdentifier
                    });

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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

        /// <summary>
        /// API sửa thông tin phòng ban
        /// </summary>
        /// <param name="depaCodertment">ID phòng ban</paCodem>
        /// <param name="base">Thông tin cần sửa</para, base.BaseNamem>;
        /// <returns>ID phòng ban đã sửa</returns>
        /// Author: MDLONG(12/11/2022)
        [HttpPut("{recordId}")]
        public IActionResult UpdateBase([FromRoute] Guid recordId, [FromBody] T entity)
        {

            try
            {
                var response = _baseBL.UpdateOneByID(recordId, entity, ModelState);
                if (response.Success)
                {
                    return StatusCode((int)response.StatusCode, response.Data);
                }

                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                {
                    ErrorCode = response.ErrorCode,
                    DevMsg = Resource.UserMsg_Edit_Failed,
                    UserMsg = Resource.UserMsg_Edit_Failed,
                    MoreInfo = response.Data,
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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

        /// <summary>
        /// Xóa thông tin 1 phòng ban theo id
        /// </summary>
        /// <param name="recordId">Id bản ghi</param>
        /// <returns>Số bản ghi bị xóa</returns>
        /// Author: MDLONG(12/11/2022)
        [HttpDelete("{recordId}")]
        public IActionResult DeleteOneByID([FromRoute] Guid recordId)
        {
            try
            {
                var result = _baseBL.DeleteOneByID(recordId);

                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, result);

                }
                return StatusCode(StatusCodes.Status204NoContent, new ErrorResult
                {
                    ErrorCode = ErrorCode.NotFound,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.UserMsg_Not_Found,
                    MoreInfo = "",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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

        /// <summary>
        /// Xóa nhiều bản ghi theo id
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteByIDs([FromBody] List<string> ids)
        {
            try
            {
                ServiceResponse response = _baseBL.DeleteByIDs(ids);
                if (response.Success)
                {
                    return StatusCode(StatusCodes.Status200OK, response.Data);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = ErrorCode.NotFound,
                        DevMsg = Resource.DevMsg_ID_Not_Exist,
                        UserMsg = Resource.UserMsg_Delete_Failed,
                        MoreInfo = response.Data,
                        TraceId = HttpContext.TraceIdentifier
                    });
                }
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
    }

}
