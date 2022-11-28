using MISA.AMIS.DL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL
{
    public class BaseBL<T> : IBaseBL<T>
    {

        #region Field
        private IBaseDL<T> _baseDL;
        #endregion
        #region Constructor
        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        } 
        #endregion

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Kết quả xóa</returns>
        /// Created by: MDLONG(18/11/2022)
        public int DeleteOneByID(Guid id)
        {
            return this._baseDL.DeleteOneByID(id);
        }

        /// <summary>
        /// Xóa nhiều bản ghi theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Số bản ghi bị xóa</returns>
        /// Created by: MDLONG(18/11/2022)
        public int DeleteByIDs(List<string> ids)
        {
            string stringIds = "";
            int length = ids.ToArray().Length;
            for (int i = 0; i < length; i++)
            {
                if (i < length - 1)
                {
                    stringIds += ids[i] + ",";
                }
                else
                {
                    stringIds += ids[i];
                }

            }
            return _baseDL.DeleteByIDs(stringIds);
        }

        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: MDLONG(18/11/2022)
        public IEnumerable<T> GetAll()
        {
            return this._baseDL.GetAll();
        }

        /// <summary>
        /// Lấy bản ghi theo id
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: MDLONG(18/11/2022)
        public T GetByID(Guid id)
        {
            return this._baseDL.GetByID(id);
        }

        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Mã nhân viên được thêm mới</returns>
        /// Created by: MDLONG(18/11/2022)
        public Guid InsertOne(T entity)
        {
            return this._baseDL.InsertOne(entity);
        }

        /// <summary>
        /// Cập nhật 1 bản ghi
        /// </summary>
        /// <param name="id">id bản ghi</param>
        /// <param name="entity">thông tin mới</param>
        /// <returns>kết quả cập nhật</returns>
        /// Created by: MDLONG(18/11/2022)
        public int UpdateOneByID(Guid id, T entity)
        {
            T oldEntity = this._baseDL.GetByID(id);

            TypeDescriptor.GetProperties(entity)["CreatedDate"].SetValue(entity, TypeDescriptor.GetProperties(oldEntity)["CreatedDate"].GetValue(oldEntity));
            TypeDescriptor.GetProperties(entity)["CreatedBy"].SetValue(entity, TypeDescriptor.GetProperties(oldEntity)["CreatedBy"].GetValue(oldEntity));
            TypeDescriptor.GetProperties(entity)["ModifiedDate"].SetValue(entity, DateTime.Now);
            return this._baseDL.UpdateOneByID(id, entity);
        }

        /// <summary>
        /// Kiểm tra mã trùng
        /// </summary>
        /// <param name="entity">đối tượng</param>
        /// <returns>bool</returns>
        /// Created by: MDLONG(18/11/2022)
        public bool CheckDupplicatedCode(T entity) { 
            return this._baseDL.CheckDupplicatedCode(entity); 
        }
    }


}
