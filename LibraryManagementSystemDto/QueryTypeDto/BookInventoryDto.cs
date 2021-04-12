using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DTO.QueryTypeDto
{
    /*===============================================
    * CLR 版本  :4.0.30319.42000
    * 命名空间  :LibraryManagementSystem.DTO.QueryTypeDto
    * 文件名称  :BookInventoryDto.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-02-20 13:58:15 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public class BookInventoryDto
    {
        /// <summary>
        /// ID  
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        ///是否移除
        /// </summary>
        public bool IsRemove { get; set; }
        
        ///<summary>
        /// 书籍信息编号
        /// </summary>
        public Guid BookInfoId { get; set; }

        ///<summary>
        /// 登记人员
        /// </summary>
        public Guid AdminId { get; set; }

        ///<summary>
        ///登记人名称
        /// </summary>
        public string AdminName { get; set; }

        ///<summary>
        ///入库数量
        /// </summary>
        public int InventoryQuantity { get; set; }
    }
}
