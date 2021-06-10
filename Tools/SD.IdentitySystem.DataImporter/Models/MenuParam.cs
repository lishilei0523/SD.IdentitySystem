using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realgoal.MOM.DataDrive.Bussiness.Models.MOM.IdentityContext
{
    /// <summary>
    /// 菜单参数
    /// </summary>
    public class MenuParam
    {
        public string 所属系统 { get; set; }
        public string 应用程序类型 { get; set; }
        public string 菜单名称 { get; set; }
        public string 参数名 { get; set; }
        public string 参数备注 { get; set; }
        public string 参数值 { get; set; }
        public bool 是否查询 { get; set; }
    }
}
