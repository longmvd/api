using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MISA.AMIS.Common;
using MISA.AMIS.Common.DTO;
using MISA.AMIS.Common.Enums;
using MISA.AMIS.DL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
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

        #region Method

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
        public ServiceResponse DeleteByIDs(List<string> ids)
        {
            ServiceResponse response = new ServiceResponse() { Success = true, StatusCode = System.Net.HttpStatusCode.OK} ;
            string stringIds = "";
            int numberOfRecord = ids.Count;
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
            int deletedRow = _baseDL.DeleteByIDs(stringIds, numberOfRecord);
            response.Data = deletedRow;
            if(deletedRow == 0)
            {
                response.Success = false;
                response.Data = new { IDs =  new List<string>() {Resource.DevMsg_ID_Not_Exist } };
                response.StatusCode = System.Net.HttpStatusCode.NoContent;
            }
            return response;
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
        public virtual T GetByID(Guid id)
        {
            return this._baseDL.GetByID(id);
        }

        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Mã nhân viên được thêm mới</returns>
        /// Created by: MDLONG(18/11/2022)
        public virtual ServiceResponse InsertOne(T entity, ModelStateDictionary modelStateDictionary)
        {
            var response = ValidateData(entity, modelStateDictionary);
            if (response.Success)
            {
                var result = this._baseDL.InsertOne(entity);
                if(result != Guid.Empty)
                {
                    response.Data = result;
                    response.StatusCode = System.Net.HttpStatusCode.Created;
                }
                else
                {
                    response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                    response.Success = false;
                    response.Data = null;
                }
            }
            return response;
        }

        /// <summary>
        /// Cập nhật 1 bản ghi
        /// </summary>
        /// <param name="id">id bản ghi</param>
        /// <param name="entity">thông tin mới</param>
        /// <returns>kết quả cập nhật</returns>
        /// Created by: MDLONG(18/11/2022)
        public ServiceResponse UpdateOneByID(Guid id, T entity, ModelStateDictionary modelStateDictionary)
        {
            ServiceResponse respone = ValidateData(entity, modelStateDictionary);
            T oldEntity = this._baseDL.GetByID(id);
            //nếu mã trùng nhưng là mã của chính nó thì vẫn đúng
            if(respone.ErrorCode == ErrorCode.DuplicateCode)
            {
                if (isTheOne(entity, oldEntity))
                {
                    respone.Success = true;
                }
            }
            if (respone.Success)
            {
                TypeDescriptor.GetProperties(entity)["CreatedDate"].SetValue(entity, TypeDescriptor.GetProperties(oldEntity)["CreatedDate"].GetValue(oldEntity));
                TypeDescriptor.GetProperties(entity)["CreatedBy"].SetValue(entity, TypeDescriptor.GetProperties(oldEntity)["CreatedBy"].GetValue(oldEntity));
                TypeDescriptor.GetProperties(entity)["ModifiedDate"].SetValue(entity, DateTime.Now);
                respone.Data = this._baseDL.UpdateOneByID(id, entity);
                respone.StatusCode = System.Net.HttpStatusCode.OK;
                return respone;
            }
            return respone;

        }

        private static bool isTheOne(T entity, T oldEntity)
        {
            var key = typeof(T).GetProperty($"{typeof(T).Name}Code");
            return key.GetValue(entity).Equals(key.GetValue(oldEntity));
        }

        /// <summary>
        /// Lấy ra mã nhân viên lớn nhất
        /// </summary>
        /// <returns></returns>
        public string GetTheBiggestCode()
        {
            return _baseDL.GetTheBiggestCode();
        }

        /// <summary>
        /// Kiểm tra mã trùng
        /// </summary>
        /// <param name="entity">đối tượng</param>
        /// <returns>bool</returns>
        /// Created by: MDLONG(18/11/2022)
        public bool CheckDupplicatedCode(T entity)
        {
            return this._baseDL.CheckDupplicatedCode(entity);
        }

        private ServiceResponse ValidateData(T entity, ModelStateDictionary modelStateDictionary)
        {
            var response = new ServiceResponse() { Success = true, StatusCode = System.Net.HttpStatusCode.OK};
            var properties = typeof(T).GetProperties();

            //custom lỗi binding dữ liệu
            var errorObject = new ExpandoObject() as IDictionary<string, object>;
            bool isValidObject = true;
            bool isValidCode = true;
            foreach (var modelState in modelStateDictionary)
            {
                // lấy ra danh sách lỗi
                var errors = modelState.Value.Errors.Select(error => error.ErrorMessage).ToList();
                if (errors.Count > 0)
                {
                    //nếu có lỗi thì gán property có lỗi tương ứng với dánh sách lỗi
                    var key = modelState.Key;
                    if (key.Contains("$"))
                    {
                        //nếu dữ liệu gửi lên sai thì tất cả đều sai
                        isValidObject = false;
                        isValidCode = false;
                    }
                    if (key.Contains("Code"))
                    {
                        isValidCode = false;
                    }
                    
                    errorObject.Add(key, errors);
                }
            }

            //kiểm tra mã trùng khi dữ liệu hợp lệ
            if (isValidCode)
            {
                bool isDupplicate = CheckDupplicatedCode(entity);
                if (isDupplicate)
                {
                    var code = $"{typeof(T).Name}Code";
                    errorObject = new ExpandoObject() as IDictionary<string, object>;
                    errorObject.Add(code, new List<string>() { string.Format(Resource.UserMsg_Dupplicated_Code, typeof(T).GetProperty(code).GetValue(entity)) });
                    response.StatusCode = System.Net.HttpStatusCode.OK;
                    response.Data = errorObject;
                    response.Success = false;
                    response.ErrorCode = ErrorCode.DuplicateCode;
                    return response;
                }

            }


            //Nếu có lỗi
            if (errorObject.Count > 0)
            {
                response.Success = false;
                response.ErrorCode = ErrorCode.InvalidData;
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;

                if (isValidObject)
                {
                    //sắp xếp lỗi theo thứ tự property
                    var orderedErrorObject = new ExpandoObject() as IDictionary<string, object>;
                    foreach (var property in properties)
                    {
                        var propName = property.Name;
                        bool isContainsKey = ((IDictionary<String, object>)errorObject).ContainsKey(propName);
                        if(isContainsKey)
                        {
                            var value = errorObject[propName];
                            orderedErrorObject.Add(propName, value);
                        }
                    }
                    response.Data = orderedErrorObject;
                }
                else
                {
                    //nếu dữ liệu gửi lên không binding được thì trả luôn lỗi các trường không thể binding
                    response.Data = errorObject;
                }
            }

            return response;
        }

        #endregion

    }


}
