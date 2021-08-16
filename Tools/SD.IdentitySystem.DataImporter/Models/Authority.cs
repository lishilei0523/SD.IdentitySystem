namespace SD.IdentitySystem.DataImporter.Models
{
    /// <summary>
    /// 权限
    /// </summary>
    public class Authority
    {
        public string 信息系统编号 { get; set; }
        public string 信息系统名称 { get; set; }
        public string 应用程序类型 { get; set; }
        public string 权限名称 { get; set; }
        public string 权限路径 { get; set; }
    }
}
