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
        /// Lấy ra mã lớn nhất
        /// </summary>
        /// <returns></returns>
        public string GetTheBiggestCode()
        {
            string storedProcedure = String.Format(Procedure.GET_THE_BIGGEST_CODE, typeof(T).Name);
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryFirstOrDefault<string>(storedProcedure, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        /// <summary>
        /// Xóa 1 bản ghi theo id
        /// </summary>
        /// <param name="id">ID bản ghi cần xóa</param>
        /// <returns>Số bản ghi bị xóa</returns>
        /// Author: MDLONG(13/11/2022)
        public int DeleteOneByID(Guid id)
        {
            string storedProcedure = String.Format(Procedure.DELETE_BY_ID, typeof(T).Name);
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
        /// Xóa nhiều theo id
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>Số bản ghi bị xóa</returns>
        /// Created by: MDLONG(23/11/2022)
        public int DeleteByIDs(string ids, int numberOfRecord)
        {
            string storedProcedure = String.Format(Procedure.DELETE_BY_IDS, typeof(T).Name);
            var parameters = new DynamicParameters();
            parameters.Add(String.Format("@{0}IDs", typeof(T).Name), ids);

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                using(var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        int numberOfRow = mySqlConnection.Execute(storedProcedure, parameters, transaction, commandType: CommandType.StoredProcedure);
                        if (numberOfRow == numberOfRecord)
                            transaction.Commit();
                        else
                        {
                            numberOfRow = 0;
                            transaction.Rollback();
                        }
                        return numberOfRow ;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        transaction.Rollback();
                        return 0;
                    }
                }
            }
        }

        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng thêm mới</param>
        /// <returns>Id bản ghi mới</returns>
        ///  Created by: MDLONG(13/11/2022)
        public Guid InsertOne(T entity)
        {

            Guid id = Guid.NewGuid();
            string storedProcedureName = String.Format(Procedure.INSERT_ONE, typeof(T).Name);
            var parameters = new DynamicParameters();
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(entity))
            {
                //Lấy tên của prop
                string property = descriptor.Name;
                //Lấy giá trị
                object value = descriptor.GetValue(entity);
                //Nếu prop id
                string idField = $"{typeof(T).Name}ID";
                if (property.Equals(idField))
                    parameters.Add($"@{property}", id);
                else
                    parameters.Add($"@{property}", value);
            }

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();
                using (var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        int result = mySqlConnection.Execute(storedProcedureName, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);
                        transaction.Commit();
                        return id;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        transaction.Rollback();
                        return Guid.Empty;
                    }
                } 
            }
        }

        /// <summary>
        /// Cập nhật bản ghi theo id
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <param name="entity">Thông tin mới của bản ghi</param>
        /// <returns>kết quả cập nhật</returns>
        /// Created by: MDLONG(13/11/2022)
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

        /// <summary>
        /// Kiểm tra mã TRÙNG
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Kết quả kiểm tra</returns>
        ///Created by: MDLONG(18/11/2022)
        public bool CheckDupplicatedCode(T entity)
        {
            string storedProcedure = String.Format(Procedure.CHECK_DUPPLICATED_CODE, typeof(T).Name);
            var parameters = new DynamicParameters();
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(entity))
            {
                //Lấy tên của prop
                string property = descriptor.Name;
                //Nếu prop id
                string idField = $"{typeof(T).Name}Code";
                if (property.Equals(idField))
                {
                    object value = descriptor.GetValue(entity);
                    parameters.Add($"@{property}", value);
                    break;
                }
            }

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var result = mySqlConnection.QueryFirstOrDefault<Int16>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                return result > 0 ? true : false;
            }
        }

    }
}
