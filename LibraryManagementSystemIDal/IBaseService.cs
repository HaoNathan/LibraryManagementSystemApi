using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using LibraryManagementSystem.MODEL;

namespace LibraryManagementSystem.IDAL
{
    /*===============================================
    * CLR 版本  :4.0.30319.42000
    * 命名空间  :LibraryManagementSystem.IDAL
    * 文件名称  :IBaseService.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-01-09 17:15:34 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public interface IBaseService<T> where T : BaseEntity, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> Add(T model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(Guid id);

        /// <summary>
        /// 获取合计
        /// </summary>
        /// <returns></returns>
        Task<int> TotalCount(bool includeRemove);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> Update(T model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> UpdateSingle(Guid id,string column,string value);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryAll(bool includeRemove);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<DataTable> QueryAll(string columns,bool includeRemove);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lambdaFunc"></param>
        /// <param name="includeRemove"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryAll(Func<T, bool> lambdaFunc, bool includeRemove);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryAll(Dictionary<string, string> dic);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeRemove"></param>
        /// <returns></returns>
        Task<T> Query(Guid id,bool includeRemove);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        Task<int> IsExist(Dictionary<string, object> dic);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<bool> IsExist(Guid id);

        Task<List<TOut>> RunProc<TOut>(string procName,Dictionary<string,string>dic);
    }
}
