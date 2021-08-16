namespace SD.IdentitySystem.DataImporter.Models
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class Menu
    {
        public string 信息系统编号 { get; set; }
        public string 信息系统名称 { get; set; }
        public string 应用程序类型 { get; set; }
        public string 菜单名称 { get; set; }
        public string 链接地址 { get; set; }
        public int 排序 { get; set; }
        public string 图标 { get; set; }
        public string 路径 { get; set; }
        public string Id { get; set; }
        public string 上级Id { get; set; }
    }
}
