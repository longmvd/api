using Microsoft.AspNetCore.Mvc.ModelBinding;
using MISA.AMIS.Common.DTO;
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
        /// Lấy ra mã nhân viên lớn nhất
        /// </summary>
        /// <returns></returns>
        public string GetTheBiggestCode();

        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <returns>Id bản ghi</returns>
        /// Created by: MDLONG(11/11/2022)
        public ServiceResponse InsertOne(T entity, ModelStateDictionary modelStateDictionary);

        /// <summary>
        /// Cập nhật bản ghi theo id
        /// </summary>
        /// <returns>1 bản ghi</returns>
        /// Created by: MDLONG(11/11/2022)
        public ServiceResponse UpdateOneByID(Guid id, T entity, ModelStateDictionary modelStateDictionary);

        /// <summary>
        /// Xóa bản ghi theo id
        /// </summary>
        /// <returns>Số bản ghi bị xóa</returns>
        /// Created by: MDLONG(11/11/2022)
        public int DeleteOneByID(Guid id);

        /// <summary>
        /// Xóa nhiều bản ghi theo id
        /// </summary>
        /// <returns>Số bản ghi bị xóa</returns>
        /// Created by: MDLONG(11/11/2022)
        public ServiceResponse DeleteByIDs(List<string> ids);

        /// <summary>
        /// Kiểm tra mã trùng
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Kết quả kiểm tra</returns>
        /// Created by: MDLONG(18/11/2022)
        public bool CheckDupplicatedCode(T entity);



    }
}
