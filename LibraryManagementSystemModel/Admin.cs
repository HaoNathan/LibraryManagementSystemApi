using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.MODEL
{
    /*===============================================
    * CLR 版本  :4.0.30319.42000
    * 命名空间  :LibraryManagementSystem.MODEL
    * 文件名称  :Admin.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-01-17 16:48:55 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :Admin实体类
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public class Admin:BaseEntity
    {
        ///<summary>
        ///用户名
        /// </summary>
        public string AdminName { get; set; }

        ///<summary>
        ///密码
        /// </summary>
        public string AdminPassword { get; set; }

        ///<summary>
        ///权限
        /// </summary>
        public string AdministratorRights { get; set; }

    }
}
