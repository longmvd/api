using Dapper;
using MISA.AMIS.Common.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL
{
    public class EmployeeDL : IEmployeeDL
    {
        public IEnumerable<dynamic> GetAllEmployee()
        {
            string connectionString = "Server=localhost;Database=misa.web09.ctm.mdlong;Uid=root;Pwd=123456;";
            var mySqlConnection = new MySqlConnection(connectionString);
            string storeProcedure = "Proc_employee_SelectAll";
            var employees = mySqlConnection.Query(storeProcedure, commandType: System.Data.CommandType.StoredProcedure);

            //xử lý kết quả trả về
            if (employees != null)
                return employees;

            return new List<Employee>();
        }

        public Employee GetEmployeeById(Guid employeeId)
        {
            throw new NotImplementedException();
        }

        public int DeleteEmployee(Guid employeeId)
        {
            throw new NotImplementedException();
        }

        public Guid InsertEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public int UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
