using MISA.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL.EmployeeBL
{
    public interface IEmployeeBL
    {
        /// <summary>
        /// Lấy tất cả nhân viên
        /// </summary>
        /// <returns></returns>
        /// Created by: MDLONG(11/11/2022)
        public IEnumerable<dynamic> GetAllEmployee();

        /// <summary>
        /// Lấy 1 nhân viên theo id
        /// </summary>
        /// <param name="employeeId">id nhân viên muốn lấy</param>
        /// <returns></returns>
        /// Created by: MDLONG(11/11/2022)
        public Employee GetEmployeeById(Guid employeeId);

        /// <summary>
        /// Thêm 1 nhân viên mới
        /// </summary>
        /// <param name="employee">Nhân viên cần thêm</param>
        /// <returns>id nhân viên mới</returns>
        /// Created by: MDLONG(11/11/2022)
        public Guid InsertEmployee(Employee employee);

        /// <summary>
        /// Sửa 1 nhân viên
        /// </summary>
        /// <param name="employee">Nhân viên cần sửa</param>
        /// <returns></returns>
        /// Created by: MDLONG(11/11/2022)
        public int UpdateEmployee(Employee employee);

        /// <summary>
        /// Xóa 1 nhân viên
        /// </summary>
        /// <param name="employeeId">Id Nhân viên cần xóa</param>
        /// <returns></returns>
        /// Created by: MDLONG(11/11/2022)
        public int DeleteEmployee(Guid employeeId);
    }

}
