using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.Amis.API.Entities;

namespace Misa.Amis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPositionsController : ControllerBase
    {
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
        /// API thêm mới vị trí
        /// </summary>
        /// <param name="jobPosition">Thông tin vị trí cần thêm</param>
        /// <returns>ID vị trí được thêm</returns>
        [HttpPost]
        public IActionResult InsertJobPosition([FromBody] JobPosition jobPosition)
        {
            return StatusCode(StatusCodes.Status201Created, Guid.NewGuid());
        }

        /// <summary>
        /// API sửa thông tin vị trí
        /// </summary>
        /// <param name="jobPositionId">ID vị trí</param>
        /// <param name="jobPosition">Thông tin cần sửa</param>
        /// <returns>ID vị trí đã sửa</returns>
        [HttpPut("{jobPositionId}")]
        public IActionResult UpdateJobPosition([FromRoute] Guid jobPositionId, [FromBody] JobPosition jobPosition)
        {
            return StatusCode(StatusCodes.Status200OK, jobPositionId);
        }
    }
}
