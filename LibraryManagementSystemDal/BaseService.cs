using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LibraryManagementSystem.IDAL;
using LibraryManagementSystem.MODEL;
using LibraryManagementSystemCommon;

namespace LibraryManagementSystem.DAL
{
    /*===============================================
    * CLR 版本  :4.0.30319.42000
    * 命名空间  :LibraryManagementSystem.DAL
    * 文件名称  :BaseService.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-01-09 19:10:01 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public class BaseService<T> : IBaseService<T> where T : BaseEntity, new()
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Add(T model)
        {
            var property = GetProperties(true);
            var column = string.Join(",", property.Select(m => $"[{m.Name}]"));
            var parameterStr = string.Join(",", property.Select(m => $"@{m.Name}").ToArray());
            var sqlText = $"INSERT INTO {typeof(T).Name}({column}) VALUES({parameterStr})";
            var parameters = GetParameters(model, property);
            return await DataBaseHelper<T>.ExecuteSql(sqlText, async comm =>
            {
                comm.Parameters.AddRange(parameters);
                return await comm.ExecuteNonQueryAsync();
            }) == 1;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(Guid id)
        {
            var sql = $"UPDATE {typeof(T).Name} SET IsRemove = 1 ";
            return await DataBaseHelper<T>.ExecuteSql(sql, async comm => await comm.ExecuteNonQueryAsync()) == 1;
        }

        public async Task<int> TotalCount(bool includeRemove)
        {
            var sql = $"SELECT COUNT(*) FROM {typeof(T).Name}";
            var count = await DataBaseHelper<T>.ExecuteSql(includeRemove ? sql : sql + " WHERE IsRemove = 0",
                async comm => await comm.ExecuteScalarAsync());
            return count == null ? 0 : Convert.ToInt32(count);
        }

        /// <summary>
        /// 根据ID单个查询
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeRemove"></param>
        /// <returns></returns>
        public async Task<T> Query(Guid id, bool includeRemove)
        {
            var sql = $"SELECT * FROM {typeof(T).Name} WHERE Id = '{id}' ";
            return await DataBaseHelper<T>.ExecuteSql(
                includeRemove ? sql : $"{sql} AND IsRemove = 0 ",
                async comm =>
                {
                    var reader = await comm.ExecuteReaderAsync();
                    var data = await reader.ReaderToList<T>();
                    return data[0];
                });
        }

        /// <summary>
        /// 查询是否存在
        /// </summary>
        /// <param name="dic">查询参数</param>
        /// <returns></returns>
        public async Task<int> IsExist(Dictionary<string, object> dic)
        {
            var queryPara =
                dic.Keys.Aggregate(string.Empty, (current, item) => current + $" AND {item} = '{dic[item]}'");
            var res = await DataBaseHelper<T>.ExecuteSql(
                $"SELECT COUNT(Id) FROM {typeof(T).Name} WHERE 1=1 {queryPara}",
                async comm => await comm.ExecuteScalarAsync());
            return res == null ? 0 : Convert.ToInt32(res);
        }

        /// <summary>
        /// 修改单个字段
        /// </summary>
        /// <param name="id"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> UpdateSingle(Guid id, string column, string value)
        {
            var sqlText = $"UPDATE {typeof(T).Name} SET [{column}] = @{column} WHERE Id = '{id}' ";
            var parameter = new SqlParameter(column, value);
            return await DataBaseHelper<T>.ExecuteSql(sqlText, async comm =>
            {
                comm.Parameters.Add(parameter);
                return await comm.ExecuteNonQueryAsync();
            }) == 1;
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="includeRemove">是否已作废</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> QueryAll(bool includeRemove)
        {
            var sqlStr = $"SELECT * FROM {typeof(T).Name}";
            return await DataBaseHelper<T>.ExecuteSql(includeRemove ? sqlStr : sqlStr + " WHERE IsRemove = 0",
                async comm =>
                {
                    var reader = await comm.ExecuteReaderAsync();
                    return await reader.ReaderToList<T>() as IEnumerable<T>;
                });
        }

        /// <summary>
        /// 查询单个字段
        /// </summary>
        /// <param name="columns">列名</param>
        /// <param name="includeRemove">是否包含已移除数据</param>
        /// <returns></returns>
        public async Task<DataTable> QueryAll(string columns, bool includeRemove)
        {
            var column = columns.Split(",");
            var columnsArr = column.Aggregate(string.Empty, (current, item) => $"{current}" + $"{item},");
            var columnStr = columnsArr.Substring(0, columnsArr.LastIndexOf(','));
            var sqlStr = $"SELECT {columnStr} FROM {typeof(T).Name}";
            return await DataBaseHelper<T>.ExecuteSql(includeRemove ? sqlStr : sqlStr + " WHERE IsRemove = 0",
                async comm =>
                {
                    return await Task.Run(() =>
                    {
                        var adapter = new SqlDataAdapter(comm);
                        var table = new DataTable();
                        adapter.Fill(table);
                        return table;
                    });
                });
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> Update(T model)
        {
            var property = GetProperties(false);
            var column = string.Join(",", property.Select(m => $"[{m.Name}] = @{m.Name}"));
            var sqlText = $"UPDATE {typeof(T).Name} SET {column} WHERE Id = '{model.Id}' ";
            var parameters = GetParameters(model, property);
            return await DataBaseHelper<T>.ExecuteSql(sqlText, async comm =>
            {
                comm.Parameters.AddRange(parameters);
                return await comm.ExecuteNonQueryAsync();
            }) == 1;
        }

        /// <summary>
        /// 查询全部(带参数)[Linq语句查询]
        /// </summary>
        /// <param name="lambdaFunc"></param>
        /// <param name="includeRemove"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> QueryAll(Func<T, bool> lambdaFunc, bool includeRemove)
        {
            var data = await QueryAll(includeRemove);
            return data.Where(lambdaFunc);
        }

        /// <summary>
        /// 自定义带参查询全部[SQL语句参数]
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> QueryAll(Dictionary<string, string> dic)
        {
            var queryPara = string.Empty;
            var parameters = dic.Select(m => new SqlParameter(m.Key, m.Value)).ToList();
            foreach (var key in dic.Keys)
            {
                if (key.Contains("Time"))
                {
                    var times = dic[key].Split("~");
                    queryPara += $" AND [{key}] >= @Begin{key} AND [{key}] <= @End{key}";
                    parameters.AddRange(new SqlParameter[]
                        {new($"Begin{key}", times[0]), new($"End{key}", times[1])});
                    continue;
                }

                if (key.Contains("%") && key.Contains("||"))
                {
                    var columnName = key.Replace("||", "").Replace("%", "").Trim();
                    queryPara += $" OR [{columnName}] LIKE @Para{columnName}";
                    parameters.Add(new SqlParameter
                        ($"Para{columnName}", dic[key]));
                    continue;
                }

                if (key.Contains("%"))
                {
                    var columnName = key.Replace("%", "").Trim();
                    queryPara += $" AND [{columnName}] LIKE @Para{columnName}";
                    parameters.Add(new SqlParameter
                        ($"Para{columnName}", dic[key]));
                    continue;
                }

                if (key.Contains("||"))
                {
                    var columnName = key.Replace("||", "").Trim();
                    queryPara += $" OR [{columnName}] = @Para{columnName}";
                    parameters.Add(new SqlParameter
                        ($"Para{columnName}", dic[key]));
                    continue;
                }

                queryPara += $" AND [{key}] = @{key}";
            }

            parameters =
                parameters.Where(m => !m.ParameterName.Contains("||") && !m.ParameterName.Contains("%"))
                    .ToList();
            return await DataBaseHelper<T>.ExecuteSql($"SELECT * FROM {typeof(T).Name} WHERE 1 = 1 {queryPara}",
                async comm =>
                {
                    comm.Parameters.AddRange(parameters.ToArray());
                    var reader = await comm.ExecuteReaderAsync();
                    var data = await reader.ReaderToList<T>() as IEnumerable<T>;
                    return data;
                });
        }

        /// <summary>
        /// 获取全部属性
        /// </summary>
        /// <param name="isEqualsId">是否包含 Id属性 </param>
        /// <returns></returns>
        private static PropertyInfo[] GetProperties(bool isEqualsId)
        {
            var properties = typeof(T).GetProperties();
            return isEqualsId ? properties : properties.Where(m => !m.Name.Equals("Id")).ToArray();
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> IsExist(Guid id)
        {
            var sql = $"SELECT COUNT(*) FROM {typeof(T).Name} WHERE Id = @id";
            return await DataBaseHelper<T>.ExecuteSql(sql, async comm =>
            {
                comm.Parameters.Add(new SqlParameter("id", id));
                var res = await comm.ExecuteScalarAsync();
                return res != null && (int)res == 1;
            });
        }

        /// <summary>
        /// 执行存储过程[查询]
        /// </summary>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="procName"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public async Task<List<TOut>> RunProc<TOut>(string procName, Dictionary<string, string> dic)
        {
            return await DataBaseHelper<T>.ExecuteProc(procName, async comm =>
            {
                comm.CommandType = CommandType.StoredProcedure;
                var para = dic.Select(m => new SqlParameter(m.Key, m.Value)).ToArray();
                comm.Parameters.AddRange(para);
                var res = await comm.ExecuteReaderAsync();
                return await res.ReaderToList<TOut>();
            });
        }

        /// <summary>
        /// 获取SqlParameters
        /// </summary>
        /// <param name="model">实体类</param>
        /// <param name="properties">对象所有属性</param>
        /// <returns></returns>
        private SqlParameter[] GetParameters(T model, IEnumerable<PropertyInfo> properties) =>
            properties.Select(m => new SqlParameter(m.Name, m.GetValue(model))).ToArray();
    }
}
