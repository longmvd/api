using MISA.AMIS.Common.DTO;
using MISA.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL
{
    public interface IEmployeeDL : IBaseDL<Employee>
    {
        /// <summary>
        /// Phân trang nhân viên
        /// </summary>
        /// <param name="request">Yêu cầu phân trang</param>
        /// <returns>Trang danh sách nhân viên</returns>
        public PagingResult<Employee> GetByFilter(PagingRequest request);
    }
}
