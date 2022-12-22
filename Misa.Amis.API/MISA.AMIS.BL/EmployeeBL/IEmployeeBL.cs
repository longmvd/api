using ClosedXML.Excel;
using MISA.AMIS.Common.DTO;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL
{
    public interface IEmployeeBL : IBaseBL<Employee>
    {
        /// <summary>
        /// Lấy nhân viên theo điều kiện và phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Paging</returns>
        ///Created by: MDLONG(18/11/2022)
        public PagingResult<EmployeeDTO> GetByFilter(PagingRequest request);

        /// <summary>
        /// Lấy nhân viên theo id
        /// </summary>
        /// <param name="id">id nhân viên</param>
        /// <returns></returns>
        public EmployeeDTO GetByID(Guid id);

        /// <summary>
        /// Xuất khẩu ra excel
        /// </summary>
        /// <returns>Bảng excel</returns>
        ///Created by: MDLONG(18/11/2022)
        public XLWorkbook ExportToExcel(PagingRequest request);
    }

}
