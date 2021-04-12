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
    * 文件名称  :BookInventory.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-01-17 16:53:41 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :书籍库存实体类
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    /// <summary>
    /// 书籍库存实体类
    /// </summary>
    public class BookInventory:BaseEntity
    {
        ///<summary>
        /// 书籍信息编号
        /// </summary>
        public Guid BookInfoId { get; set; }

        ///<summary>
        /// 登记人员
        /// </summary>
        public Guid AdminId { get; set; }

        ///<summary>
        ///入库数量
        /// </summary>
        public int InventoryQuantity { get; set; }

    }
}
