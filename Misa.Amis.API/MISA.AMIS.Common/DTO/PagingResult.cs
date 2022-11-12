using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.DTO

{
    public class PagingResult<T>
    {
        //Tổng số trang
        public int TotalPage { get; set; }

        //Tổng số bản ghi
        public int TotalRecord { get; set; }

        //Danh sách bản ghi
        public List<T> Data { get; set; }
    }
}
