using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Entities
{
    public class JobPosition : BaseEntity
    {
        /// <summary>
        /// Id vị trí công việc
        /// </summary>
        [Key]
        public Guid JobPositionID { get; set; }

        /// <summary>
        /// Mã vị trí
        /// </summary>
        [Required(ErrorMessage = "Mã Không để trống")]
        public string JobPositionCode { get; set; }

        /// <summary>
        /// Tên vị trí
        /// </summary>
        [Required(ErrorMessage = "Tên Không để trống")]
        public string JobPositionName { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }
    }
}
