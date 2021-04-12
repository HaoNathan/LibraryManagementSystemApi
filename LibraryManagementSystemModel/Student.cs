using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.MODEL
{
    /// <summary>
    /// 学生信息实体类
    /// </summary>
    public class Student:BaseEntity
    {
        ///<summary>
        ///年龄
        /// </summary>
        public int Age { get; set; }

        ///<summary>
        ///所在系部
        /// </summary>
        public Guid DepartmentId { get; set; }

        ///<summary>
        ///出生年月
        /// </summary>
        public DateTime BirthDay { get; set; }

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

    }
}
