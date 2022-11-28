using MISA.AMIS.Common.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL
{
    public interface IBaseDL<T>
    {
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Created by: MDLONG(11/11/2022)
        public IEnumerable<T> GetAll();

        /// <summary>
        /// Lấy bản ghi theo id
        /// </summary>
        /// <returns>1 bản ghi</returns>
        /// Created by: MDLONG(11/11/2022)
        public T GetByID(Guid id);

        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <returns>Id bản ghi</returns>
        /// Created by: MDLONG(11/11/2022)
        public Guid InsertOne(T entity);

        /// <summary>
        /// Cập nhật bản ghi theo id
        /// </summary>
        /// <returns>1 bản ghi</returns>
        /// Created by: MDLONG(11/11/2022)
        public int UpdateOneByID(Guid id, T entity);

        /// <summary>
        /// Xóa bản ghi theo id
        /// </summary>
        /// <returns>Số bản ghi bị xóa</returns>
        /// Created by: MDLONG(11/11/2022)
        public int DeleteOneByID(Guid id);

        /// <summary>
        /// Xóa nhiều bản ghi theo danh sách id
        /// </summary>
        /// <param name="ids">danh sách id</param>
        /// <returns>trạng thái xóa</returns>
        ///  Created by: MDLONG(11/11/2022)
        public int DeleteByIDs(string ids);

        /// <summary>
        /// Kiểm tra mã trùng
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>bool</returns>
        /// Created by: MDLONG(18/11/2022)
        public bool CheckDupplicatedCode(T entity);
    }
}
