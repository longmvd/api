using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL
{
    public interface IBaseBL<T>
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
    }
}
