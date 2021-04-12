namespace LibraryManagementSystem.MODEL
{
    /// <summary>
    /// 管理员权限实体类
    /// </summary>
    public class Authority:BaseEntity
    {
        ///<summary>
        ///权限
        /// </summary>
        public string Authorities { get; set; }

        /// <summary>
        /// 权限编号
        /// </summary>
        public string AuthorityNum { get; set; }

        /// <summary>
        /// 权限父节点
        /// </summary>
        public string AuthorityParent { get; set; }
    }
}