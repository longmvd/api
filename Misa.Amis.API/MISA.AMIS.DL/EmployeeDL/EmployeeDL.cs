using Dapper;
using MISA.AMIS.Common.Constants;
using MISA.AMIS.Common.DTO;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Enums;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL
{
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {
        

        /// <summary>
        /// Lấy nhân viên theo điều kiện và phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Phân trang</returns>
        ///Created by: MDLONG(18/11/2022)
        public PagingResult<EmployeeDTO> GetByFilter(PagingRequest request)
        {
            PagingResult<EmployeeDTO> paging = new PagingResult<EmployeeDTO>();
            string storedProcedure = String.Format(Procedure.GET_BY_FILTER, "employee");
            var parameter = new DynamicParameters();
            parameter.Add("@Keyword", request.EmployeeFilter);
            parameter.Add("@Offset", request.PageNumber);
            parameter.Add("@Limit", request.PageSize);
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedure, parameter, commandType: CommandType.StoredProcedure);
                var totalRecord = result.Read<int>().First();
                var employees = result.Read<EmployeeDTO>().ToList();
                int totalPage =Convert.ToInt32(Math.Ceiling(totalRecord / (decimal)request.PageSize));
                paging.Data = employees;
                if(employees.ToArray().Length == 0)
                {
                    paging.TotalRecord = 0;
                    paging.TotalPage = 0;
                }
                else
                {
                    paging.TotalRecord = totalRecord;
                    paging.TotalPage = totalPage;
                }
                return paging;
            }
        }
    }
}
