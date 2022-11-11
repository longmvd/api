using MISA.AMIS.Common.Entities;
using MISA.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL.EmployeeBL
{
    public class EmployeeBL : IEmployeeBL
    {
        #region Field
        private IEmployeeDL _employeeDL;
        #endregion

        #region Constructor
        public EmployeeBL(IEmployeeDL employeeBL)
        {
            _employeeDL = employeeBL;
        }
        #endregion


        public IEnumerable<dynamic> GetAllEmployee()
        {
            return _employeeDL.GetAllEmployee();
        }

        public int DeleteEmployee(Guid employeeId)
        {
            return _employeeDL.DeleteEmployee(employeeId);
        }


        public Employee GetEmployeeById(Guid employeeId)
        {
            return _employeeDL.GetEmployeeById(employeeId);
        }

        public Guid InsertEmployee(Employee employee)
        {
            return _employeeDL.InsertEmployee(employee);
        }

        public int UpdateEmployee(Employee employee)
        {
            return _employeeDL.UpdateEmployee(employee);
        }
    }
}
