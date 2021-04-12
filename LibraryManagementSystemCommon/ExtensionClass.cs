using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LibraryManagementSystem.MODEL;

namespace LibraryManagementSystemCommon
{
    /*===============================================
    * CLR 版本  :4.0.30319.42000
    * 命名空间  :LibraryManagementSystemCommon
    * 文件名称  :DataTableToList.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-01-03 16:18:02 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :拓展类
    * 使用说明  :提供公共的拓展方法
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public static class ExtensionClass
    {
        /// <summary>
        /// DataTable转换成List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<T> TableToList<T>(this DataTable table)
        {
            var properties = typeof(T).GetProperties();
            var list = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                var t = (T)Activator.CreateInstance(typeof(T));
                foreach (var property in properties)
                {
                    if (table.Columns.Contains(property.Name) && row[property.Name] != DBNull.Value)
                    {
                        property.SetValue(t, row[property.Name]);
                    }
                }

                list.Add(t);
            }

            return list;
        }

        /// <summary>
        /// 数据塑性
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="data">数据源</param>
        /// <param name="shapeField">需要的字段</param>
        /// <returns></returns>
        public static List<ExpandoObject> ShapeData<T>(this IEnumerable<T> data, string shapeField)
        {
            var fields = shapeField.Split(',');
            var propertiesList = new List<PropertyInfo>();
            var expandoObjects = new List<ExpandoObject>();
            if (string.IsNullOrWhiteSpace(shapeField))
            {
                propertiesList.AddRange(typeof(T).GetProperties());
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(shapeField))
                {
                    propertiesList.AddRange(fields.Select(item => typeof(T).GetProperty(item, BindingFlags.IgnoreCase
                            | BindingFlags.Instance | BindingFlags.Public))
                        .Where(property => property != null));
                }
            }

            foreach (var item in data)
            {
                var expandoObject = new ExpandoObject();
                foreach (var propertyInfo in propertiesList)
                {
                    var value = propertyInfo.GetValue(item);
                    ((IDictionary<string, object>)expandoObject).Add(propertyInfo.Name, value);
                }

                expandoObjects.Add(expandoObject);
            }

            return expandoObjects;
        }

        /// <summary>
        /// SqlDataReader 数据转换成List输出
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static async Task<List<T>> ReaderToList<T>(this SqlDataReader reader)
        {
            var list = new List<T>();
            var type = typeof(T);
            await using (reader)
            {
                //读取数据
                while (reader.Read())
                {
                    var t = (T)Activator.CreateInstance(type);

                    //反射获取这个类型的所有属性，遍历获取与数据库中同一名称的属性并给其赋值
                    foreach (var property in type.GetProperties())
                    {
                        var value = reader[property.Name];
                        if (value is DBNull)
                            value = null;

                        //给这一类型的某个属性赋值；
                        property.SetValue(t, value);

                    }

                    list.Add(t);
                }
            }

            return list;
        }
    }
}
