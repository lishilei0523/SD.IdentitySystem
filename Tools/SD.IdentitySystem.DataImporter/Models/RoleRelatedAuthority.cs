namespace SD.IdentitySystem.DataImporter.Models
{
    /// <summary>
    /// 角色相关权限
    /// </summary>
    public class RoleRelatedAuthority
    {
        public string 信息系统编号 { get; set; }
        public string 信息系统名称 { get; set; }
        public string 角色名称 { get; set; }
        public string 应用程序类型 { get; set; }
        public string 权限名称 { get; set; }
    }
}
