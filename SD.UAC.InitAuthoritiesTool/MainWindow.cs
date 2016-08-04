using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using SD.UAC.Common.Attributes;
using SD.UAC.IAppService.DTOs.Inputs;
using SD.UAC.IAppService.DTOs.Outputs;
using SD.UAC.IAppService.Interfaces;

namespace SD.UAC.InitAuthoritiesTool
{
    /// <summary>
    /// 主窗体
    /// </summary>
    public partial class MainWindow : Form
    {
        /// <summary>
        /// 权限服务契约接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 构造器
        /// </summary>
        public MainWindow(IAuthorizationContract authorizationContract)
        {
            this._authorizationContract = authorizationContract;
            this.InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void MainWindow_Load(object sender, EventArgs e)
        {
            IEnumerable<InfoSystemKindInfo> systemKinds = this._authorizationContract.GetSystemKinds();

            foreach (InfoSystemKindInfo systemKind in systemKinds)
            {
                this.Cbx_SystemKind.Items.Add(systemKind);
            }

            this.Cbx_SystemKind.SelectedItem = systemKinds.FirstOrDefault();
            this.Cbx_SystemKind.DisplayMember = "Name";
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        private void Brn_OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = @"dll files (*.dll)|*.dll";

            dialog.ShowDialog();
            this.Txt_FilePath.Text = dialog.FileName;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private async void Btn_Init_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.Txt_FilePath.Text))
            {
                MessageBox.Show("文件路径不可为空！", "Warning");
                return;
            }
            if (this.Cbx_SystemKind.SelectedItem == null)
            {
                MessageBox.Show("信息系统类别不可不选！", "Warning");
                return;
            }

            string assemblyPath = this.Txt_FilePath.Text;
            string systemKindNo = ((InfoSystemKindInfo)this.Cbx_SystemKind.SelectedItem).Number;


            await Task.Run(() => this.InitAuthorities(assemblyPath, systemKindNo));
            MessageBox.Show("OK", "OK");
        }

        /// <summary>
        /// 初始化权限集
        /// </summary>
        /// <param name="assemblyPath">程序集路径</param>
        /// <param name="systemKindNo">信息系统类别编号</param>
        private void InitAuthorities(string assemblyPath, string systemKindNo)
        {
            //加载程序集、加载权限
            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            Type[] types = assembly.GetTypes();

            //加载需认证的方法
            IEnumerable<MethodInfo> methodInfos = types.SelectMany(x => x.GetMethods()).Where(x => x.IsDefined(typeof(RequireAuthorizationAttribute)));

            //构造权限参数模型集
            IList<AuthorityParam> authorityParams = new List<AuthorityParam>();

            foreach (MethodInfo methodInfo in methodInfos)
            {
                RequireAuthorizationAttribute attribute = methodInfo.GetCustomAttribute<RequireAuthorizationAttribute>();

                AuthorityParam authorityParam = new AuthorityParam();
                authorityParam.AssemblyName = assembly.GetName().Name;
                authorityParam.ClassName = methodInfo.DeclaringType.Name;
                authorityParam.MethodName = methodInfo.Name;
                authorityParam.AuthorityName = attribute.AuthorityName;
                authorityParam.EnglishName = attribute.EnglishName;
                authorityParam.Description = attribute.Description;

                authorityParams.Add(authorityParam);
            }

            this._authorizationContract.CreateAuthorities(systemKindNo, authorityParams);
        }
    }
}
