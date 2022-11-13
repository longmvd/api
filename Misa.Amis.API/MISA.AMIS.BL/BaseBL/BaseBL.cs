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
        public int DeleteOneByID(Guid id)
        {
            return this._baseDL.DeleteOneByID(id);
        }

        public IEnumerable<T> GetAll()
        {
            return this._baseDL.GetAll();
        }

        public T GetByID(Guid id)
        {
            return this._baseDL.GetByID(id);
        }

        public Guid InsertOne(T entity)
        {
            return this._baseDL.InsertOne(entity);
        }

        public int UpdateOneByID(Guid id, T entity)
        {
            T oldEntity = this._baseDL.GetByID(id);

            TypeDescriptor.GetProperties(entity)["CreatedDate"].SetValue(entity, TypeDescriptor.GetProperties(oldEntity)["CreatedDate"].GetValue(oldEntity));
            TypeDescriptor.GetProperties(entity)["CreatedBy"].SetValue(entity, TypeDescriptor.GetProperties(oldEntity)["CreatedBy"].GetValue(oldEntity));
            TypeDescriptor.GetProperties(entity)["ModifiedDate"].SetValue(entity, DateTime.Now);
            return this._baseDL.UpdateOneByID(id, entity);
        }
        
    }
}
