using SD.IdentitySystem.IAppService.DTOs.Inputs;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.Attributes;
using SD.Infrastructure.Constants;
using SD.Infrastructure.MemberShip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SD.IdentitySystem.InitializationTool
{
    /// <summary>
    /// 主窗体
    /// </summary>
    public partial class MainWindow : Form
    {
        /// <summary>
        /// 身份认证服务契约接口
        /// </summary>
        private readonly IAuthenticationContract _authenticationContract;

        /// <summary>
        /// 权限服务契约接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="authorizationContract">权限服务契约接口</param>
        /// <param name="authenticationContract">身份认证服务契约接口</param>
        public MainWindow(IAuthorizationContract authorizationContract, IAuthenticationContract authenticationContract)
        {
            this._authorizationContract = authorizationContract;
            this._authenticationContract = authenticationContract;
            this.InitializeComponent();

            //用户名/密码
            string loginId = CommonConstants.AdminLoginId;
            string password = CommonConstants.InitialPassword;

            //登录
            LoginInfo loginInfo = this._authenticationContract.Login(loginId, password);
            AppDomain.CurrentDomain.SetData(SessionKey.CurrentUser, loginInfo);
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void MainWindow_Load(object sender, EventArgs e)
        {
            //加载信息系统列表
            IEnumerable<InfoSystemInfo> infoSystems = this._authorizationContract.GetInfoSystems().OrderBy(x => x.Number);
            foreach (InfoSystemInfo infoSystem in infoSystems)
            {
                this.Cbx_SystemKind.Items.Add(infoSystem);
            }

            this.Cbx_SystemKind.DisplayMember = nameof(InfoSystemInfo.Name);
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        private void Btn_OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = @"dll files (*.dll)|*.dll" };
            openFileDialog.ShowDialog();
            this.Txt_FilePath.Text = openFileDialog.FileName;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private async void Btn_Init_Click(object sender, EventArgs e)
        {
            #region # 验证

            if (string.IsNullOrWhiteSpace(this.Txt_FilePath.Text))
            {
                MessageBox.Show(@"文件路径不可为空！", @"Warning");
                return;
            }
            if (this.Cbx_SystemKind.SelectedItem == null)
            {
                MessageBox.Show(@"信息系统不可为空！", @"Warning");
                return;
            }

            #endregion

            string assemblyPath = this.Txt_FilePath.Text;
            string infoSystemNo = ((InfoSystemInfo)this.Cbx_SystemKind.SelectedItem).Number;

            await this.InitAuthorities(assemblyPath, infoSystemNo);
            MessageBox.Show(@"OK", @"OK");
        }

        /// <summary>
        /// 初始化权限集
        /// </summary>
        /// <param name="assemblyPath">程序集路径</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        private async Task InitAuthorities(string assemblyPath, string infoSystemNo)
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
                AuthorityParam authorityParam = new AuthorityParam
                {
                    AssemblyName = assembly.GetName().Name,
                    Namespace = methodInfo.DeclaringType?.Namespace,
                    ClassName = methodInfo.DeclaringType?.Name,
                    MethodName = methodInfo.Name,
                    AuthorityName = attribute.AuthorityName,
                    EnglishName = attribute.EnglishName,
                    Description = attribute.Description
                };

                bool authorityExisted = await Task.Run(() => this._authorizationContract.ExistsAuthority(authorityParam.AssemblyName, authorityParam.Namespace, authorityParam.ClassName, authorityParam.MethodName));
                if (!authorityExisted)
                {
                    authorityParams.Add(authorityParam);
                }
            }

            await Task.Run(() => this._authorizationContract.CreateAuthorities(infoSystemNo, authorityParams));
        }
    }
}
