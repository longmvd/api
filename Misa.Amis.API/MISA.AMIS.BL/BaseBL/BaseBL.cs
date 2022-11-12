using MISA.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return this._baseDL.UpdateOneByID(id, entity);
        }
    }
}
