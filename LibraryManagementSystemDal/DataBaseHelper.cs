using LibraryManagementSystem.MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace LibraryManagementSystem.DAL
{
    /*===============================================
    * CLR 版本  :4.0.30319.42000
    * 命名空间  :LibraryManagementSystem.DAL
    * 文件名称  :BaseService.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-01-03 21:54:03 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public static class DataBaseHelper<T> where T : BaseEntity, new()
    {
        /// <summary>
        /// 初始化获取配置文件中的数据库连接字符串
        /// </summary>
        static DataBaseHelper()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.json"))
                .Build();
            ConnectionStr = configuration["DatabaseConnection"];
        }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        // ReSharper disable once StaticMemberInGenericType
        private static readonly string ConnectionStr;

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="strSql"></param>
        /// <param name="func"></param>
        /// <exception cref="Exception"></exception>
        /// <returns></returns>
        public static async Task<TOut> ExecuteSql<TOut>(string strSql, Func<SqlCommand, Task<TOut>> func)
        {
            await using var conn = new SqlConnection(ConnectionStr);
            await using var comm = new SqlCommand(strSql, conn);
            conn.Open();
            var transaction = conn.BeginTransaction();
            try
            {
                comm.Transaction = transaction;
                var res = await func.Invoke(comm);
                transaction.Commit();
                return res;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public static async Task<List<TOut>> ExecuteProc<TOut>(string strSql, Func<SqlCommand, Task<List<TOut>>> func)
        {
            await using var conn = new SqlConnection(ConnectionStr);
            await using var comm = new SqlCommand(strSql, conn);
            conn.Open();
            var transaction = conn.BeginTransaction();
            try
            {
                comm.Transaction = transaction;
                var res = await func.Invoke(comm);
                transaction.Commit();
                return res;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
