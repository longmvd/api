﻿using MISA.AMIS.Common.DTO;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL
{
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {
        #region Field
        private IEmployeeDL _employeeDL;
        #endregion

        #region Constructor
        public EmployeeBL(IEmployeeDL employeeBL): base(employeeBL)
        {
            _employeeDL = employeeBL;
        }

        public PagingResult<Employee> GetByFilter(PagingRequest request)
        {
            return _employeeDL.GetByFilter(request);
        }
        #endregion
    }
}
