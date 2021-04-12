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
    * 文件名称  :BaseEntity.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-01-09 15:15:48 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public class BaseEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
        
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = DateTime.Now;
        
        /// <summary>
        /// 是否作废
        /// </summary>
        public bool IsRemove { get; set; }
    }
}
