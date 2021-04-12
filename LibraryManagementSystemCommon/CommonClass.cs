using System;
using System.Net;
using System.Net.Sockets;


namespace LibraryManagementSystemCommon
{
    /*===============================================
    * CLR 版本  :4.0.30319.42000
    * 命名空间  :LibraryManagementSystemCommon
    * 文件名称  :CommonClass.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-01-30 20:34:56 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public static class CommonClass
    {
        /// <summary>
        /// 获取本地Ip地址
        /// </summary>
        /// <returns>本机名及IP</returns>
        [Obsolete]
        public static string GetLocalIp()
        {
            var ip = string.Empty;
            var hostName = Dns.GetHostName();
            var ipEntry = Dns.GetHostEntry(hostName);
            foreach (var item in ipEntry.AddressList)
            {
                if (item.AddressFamily == AddressFamily.InterNetwork)
                {
                    ip = $"[{hostName}]{item}";
                }
            }

            return ip;
        }

        public static T SetModelValue<TSource, T>(TSource source, T t, string field)
        {
            var fields = field.Split(",");
            foreach (var item in fields)
            foreach (var property in typeof(T).GetProperties())
            foreach (var propertyInfo in typeof(TSource).GetProperties())
                if (property.Name.ToLower().Equals(item.ToLower()) && property.Name.Equals(propertyInfo.Name))
                    property.SetValue(t, propertyInfo.GetValue(source));
            return t;
        }
    }
}
