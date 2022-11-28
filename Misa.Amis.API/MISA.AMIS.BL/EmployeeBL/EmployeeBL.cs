using ClosedXML.Excel;
using MISA.AMIS.Common;
using MISA.AMIS.Common.DTO;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Enums;
using MISA.AMIS.DL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.AMIS.BL
{
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {
        #region Field
        private IEmployeeDL _employeeDL;
        #endregion

        #region Constructor
        public EmployeeBL(IEmployeeDL employeeBL) : base(employeeBL)
        {
            _employeeDL = employeeBL;
        }

        /// <summary>
        /// Lấy nhân viên theo điều kiện và phân trang
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Paging</returns>
        ///Created by: MDLONG(18/11/2022)
        public PagingResult<EmployeeDTO> GetByFilter(PagingRequest request)
        {
            request = ValidateRequest(request);
            return _employeeDL.GetByFilter(request);
        }

        /// <summary>
        /// Xuất khẩu nhân viên ra file excel
        /// </summary>
        /// <returns>Dữ liệu DataTable để xuất file</returns>
        /// Created by: MDL (26/11/2022)
        public XLWorkbook ExportToExcel(PagingRequest request)
        {
            // Lấy danh sách tất cả nhân viên
            var dataTable = createTable(request);

            XLWorkbook wb = new XLWorkbook();

            var ws = wb.AddWorksheet(dataTable);

            // Điều chỉnh độ rộng của ô vừa với độ dài của dữ liệu
            ws.Columns("A:T").AdjustToContents();

            // Tùy chỉnh style cho cột từ A đến T
            ws.Columns("A:T").Style.Font.SetFontName("Times New Roman").Font.SetFontSize(11).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left).Alignment.SetWrapText(true);

            // Lấy dòng đầu tiên của bảng tính
            var table = ws.Tables.FirstOrDefault();
            if (table != null)
            {
                // Bỏ filter của bảng
                table.ShowAutoFilter = false;

                // Set kiểu viền và màu cho background
                table.Cells().Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin).Fill.SetBackgroundColor(XLColor.Green);

                // Tùy chỉnh style cho header của bảng
                table.Row(1).Style.Font.SetFontColor(XLColor.FromTheme(XLThemeColor.Text1)).Fill.SetBackgroundColor(XLColor.Pink).Font.SetFontName("Arial").Font.SetFontSize(10).Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                // Căn giữa cho văn bản ở cột D (Ngày sinh)
                table.Column("D").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                table.Column("T").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            }

            // Thêm 2 dòng phía trên
            ws.Row(1).InsertRowsAbove(2);

            // Gán giá trị cho ô A1
            ws.Cell("A1").SetValue("DANH SÁCH NHÂN VIÊN");

            // Tùy chỉnh style cho ô A1 và A2
            ws.Cells("A1,A2").Style.Font.SetBold(true).Font.SetFontSize(16).Font.SetFontName("Arial").Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            // Merge từ A1 đến I1, từ A2 đến I2
            ws.Range("A1:T1").Merge();
            ws.Range("A2:T2").Merge();
            return wb;


        }

        /// <summary>
        /// lấy ra loại giới tính
        /// </summary>
        /// <param name="gender"></param>
        /// <returns>string</returns>
        /// Created by: MDLONG(27/11/2022)
        private string? getStringGender(Gender gender)
        {
            switch (gender)
            {
                case Gender.Male:
                    return "Nam";
                case Gender.Female:
                    return "Nữ";
                case Gender.Other:
                    return "Khác";
                default:
                    return gender.ToString();
            }
        }

        /// <summary>
        /// Tạo dữ liệu bảng
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private DataTable createTable(PagingRequest request)
        {
            var employees = this.GetByFilter(request).Data;

            // using System.Data;
            DataTable dataTable = new()
            {
                TableName = Resource.Employee_Excel_Title
            };


            var field = new EmployeeField();
            foreach (PropertyInfo prop in field.GetType().GetProperties())
            {
                var column = (Column)prop.GetValue(field, null);
                dataTable.Columns.Add(new DataColumn(column.Name));
                dataTable.Columns[column.Name].SetOrdinal(column.Order);
            }

            int indexOfEmployee = 1;

            foreach (var employee in employees)
            {
                var columns = new object[20];
                var props = field.GetType().GetProperties();

                foreach (PropertyInfo prop in props)
                {
                    var column = (Column)prop.GetValue(field, null);
                    object? value = null;
                    if (prop.Name.Equals("ColumnOrder"))
                    {
                        columns[0] = indexOfEmployee;
                    }
                    else
                    if (prop.Name.Contains("Date"))
                    {
                        var date = (DateTime?)employee.GetType()
                                                   .GetProperty(prop.Name)
                                                   ?.GetValue(employee, null);
                        columns[column.Order] = date?.ToString("dd/MM/yyyy");
                        //value = date?.ToString("dd/MM/yyyy");
                    }
                    else if (prop.Name.Equals("Gender"))
                    {
                        columns[column.Order] = getStringGender((Gender)employee.GetType()
                                                    .GetProperty(prop.Name)
                                                    ?.GetValue(employee, null));
                        //value = gender;
                    }
                    else
                    {
                        columns[column.Order] = employee.GetType().GetProperty(prop.Name)?.GetValue(employee, null);
                    }
                    //columns[column.Order] = value;

                }
                dataTable.Rows.Add(columns);
                // Thêm dữ liệu của từng nhân viên cho 1 hàng
                //dataTable.Rows.Add(indexOfEmployee, employee.EmployeeCode, employee.EmployeeName, ConvertToGenderVietnamese(employee.Gender), employee.DateOfBirth?.ToString("dd-MM-yyyy"), employee.PositionName, employee.DepartmentName, employee.BankAccountNumber, employee.BankName);
                indexOfEmployee++;
            }
            return dataTable;

        }

        /// <summary>
        /// xử lý validate request
        /// </summary>
        /// <returns></returns>
        /// Created by: MDLONG(20/11/2022)
        private PagingRequest ValidateRequest(PagingRequest request)
        {
            if (request.PageNumber == 0)
            {
                request.PageNumber = 1;
            }
            if (request.PageSize == 0)
            {
                request.PageSize = 10;
            }
            if (request.EmployeeFilter == null)
            {
                request.EmployeeFilter = "";
            }
            request.EmployeeFilter = request.EmployeeFilter.Trim();
            int offset = (request.PageNumber - 1) * request.PageSize;
            request.PageNumber = offset; //sql lấy từ vị trí 0
            return request;
        }
        #endregion
    }
}
