using Dapper;
using MISA.AMIS.Common.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using MISA.AMIS.Common.Constants;

namespace MISA.AMIS.DL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        /// <summary>
        /// Xóa 1 bản ghi theo id
        /// </summary>
        /// <param name="id">ID bản ghi cần xóa</param>
        /// <returns>Số bản ghi bị xóa</returns>
        /// Author: MDLONG(13/11/2022)
        public int DeleteOneByID(Guid id)
        {
            string storedProcedure = String.Format("Proc_{0}_DeleteByID", typeof(T).Name);
            var parameters = new DynamicParameters();
            parameters.Add(String.Format("@{0}ID", typeof(T).Name), id);

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                var transaction = mySqlConnection.BeginTransaction();
                try
                {
                    int numberOfRow = mySqlConnection.Execute(storedProcedure, parameters, transaction, commandType: CommandType.StoredProcedure);
                    transaction.Commit();
                    return numberOfRow;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    transaction.Rollback();
                    return 0;
                }
                finally
                {
                    mySqlConnection.Close();
                }
            }
        }

        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Author: MDLONG(13/11/2022)
        public IEnumerable<T> GetAll()
        {
            string storedProcedure = String.Format(Procedure.GET_ALL, typeof(T).Name);
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var result = mySqlConnection.Query<T>(storedProcedure, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        /// <summary>
        /// Lấy bản ghi theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>1 Bản ghi</returns>
        /// Author: MDLONG(13/11/2022)
        public T GetByID(Guid id)
        {
            string storedProcedure = String.Format(Procedure.GET_BY_ID, typeof(T).Name);
            var parameters = new DynamicParameters();
            parameters.Add($"@{typeof(T).Name}ID", id);

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryFirstOrDefault<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Id bản ghi mới</returns>
        /// Author: MDLONG(13/11/2022)
        public Guid InsertOne(T entity)
        {

            Guid id = Guid.NewGuid();
            string storedProcedureName = String.Format(Procedure.INSERT_ONE, typeof(T).Name);
            var parameters = new DynamicParameters();
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(entity))
            {
                string property = descriptor.Name;
                object value = descriptor.GetValue(entity);
                string idField = $"{typeof(T).Name}ID";
                if (property.Equals(idField))
                    parameters.Add($"@{property}", id);
                else
                    parameters.Add($"@{property}", value);
            }
            
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                var transaction = mySqlConnection.BeginTransaction();
                try
                {
                    int result = mySqlConnection.Execute(storedProcedureName, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);
                    transaction.Commit();
                    return id;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return Guid.Empty;
                }
                finally
                {
                    mySqlConnection.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <param name="entity">Thông tin mới của bản ghi</param>
        /// <returns></returns>
        /// Author: MDLONG(13/11/2022)
        public int UpdateOneByID(Guid id, T entity)
        {
            string storedProcedureName = String.Format(Procedure.UPDATE_BY_ID, typeof(T).Name);

            var parameters = new DynamicParameters();
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(entity))
            {
                string property = descriptor.Name;
                object value = descriptor.GetValue(entity);
                
                string idField = $"{typeof(T).Name}ID";
                if (property.Equals(idField))
                    parameters.Add($"@{property}", id);
                else
                    parameters.Add($"@{property}", value);
            }

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                var transaction = mySqlConnection.BeginTransaction();
                try
                {
                    int result = mySqlConnection.Execute(storedProcedureName, parameters, transaction, commandType: CommandType.StoredProcedure);
                    transaction.Commit();
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    transaction.Rollback();
                    return 0;
                }
                finally
                {
                    mySqlConnection.Close();
                }

            }
        }

    }
}
