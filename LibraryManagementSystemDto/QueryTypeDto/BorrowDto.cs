using System;

namespace LibraryManagementSystem.DTO.QueryTypeDto
{
    /// <summary>
    /// 图书借阅Dto
    /// </summary>
    public class BorrowDto    
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
        ///学生编号
        /// </summary>
        public Guid StudentId { get; set; }

        ///<summary>
        ///到期时间
        /// </summary>
        public DateTime EndTime { get; set; }

        ///<summary>
        ///归还时间
        /// </summary>
        public DateTime BackTime { get; set; }

        ///<summary>
        ///是否超时
        /// </summary>
        public bool IsExpired { get; set; }

        ///<summary>
        ///超时罚款
        /// </summary>
        public decimal Fine { get; set; }

        /// <summary>
        /// 管理员
        /// </summary>
        public Guid AdminId { get; set; }

        ///<summary>
        ///年龄
        /// </summary>
        public int Age { get; set; }

        ///<summary>
        ///所在系部
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// 系部名称
        /// </summary>
        public string DepartmentName { get; set; }

        ///<summary>
        ///性别
        /// </summary>
        public bool Sex { get; set; }

        ///<summary>
        ///学生姓名
        /// </summary>
        public string StudentName { get; set; }

        ///<summary>
        ///学号
        /// </summary>
        public string StudentNo { get; set; }

        ///<summary>
        ///班级
        /// </summary>
        public string Class { get; set; }

        ///<summary>
        ///联系方式
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 操作员名称
        /// </summary>
        public string AdminName { get; set; }

        ///<summary>
        ///书籍状态
        /// </summary>
        public int BookState { get; set; }

        /// <summary>
        /// 书籍Id
        /// </summary>
        public Guid BookId { get; set; }

        ///<summary>
        ///书籍类型
        /// </summary>
        public Guid BookCategoryId { get; set; }

        ///<summary>
        ///类别名称
        /// </summary>
        public string CategoryName { get; set; }

        ///<summary>
        ///出版社
        /// </summary>
        public Guid PublishingId { get; set; }

        ///<summary>
        ///出版社名称
        /// </summary>
        public string PublishingName { get; set; }

        ///<summary>
        ///价格
        /// </summary>
        public decimal Price { get; set; }

        ///<summary>
        ///发布时间
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        ///<summary>
        ///书籍名
        /// </summary>
        public string BookName { get; set; }

        ///<summary>
        ///封面
        /// </summary>
        public string BookPhoto { get; set; }

        ///<summary>
        ///作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// ISBN号
        /// </summary>
        public string ISBN { get; set; }
    }
}