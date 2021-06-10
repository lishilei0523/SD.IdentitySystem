namespace Realgoal.MOM.DataDrive.Bussiness.Models.MOM.IdentityContext
{
    /// <summary>
    /// 权限模型
    /// </summary>
    public class AuthorityModel
    {
        public string Id { get; set; }
        public string 父级Id { get; set; }
        public string 所属系统 { get; set; }
        public string 应用程序类型 { get; set; }
        public string 菜单名称 { get; set; }
        public string 连接地址 { get; set; }
        public int 排序 { get; set; }
        public string 图标 { get; set; }
        public string 路径 { get; set; }
        public string 角色 { get; set; }
    }
}
