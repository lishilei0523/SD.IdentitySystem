using SD.Common;
using SD.IdentitySystem.IAppService.DTOs.Inputs;
using SD.IdentitySystem.IAppService.DTOs.Outputs;
using SD.IdentitySystem.IAppService.Interfaces;
using SD.Infrastructure.Attributes;
using SD.Infrastructure.Constants;
using SD.Infrastructure.Membership;
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
            IEnumerable<InfoSystemInfo> infoSystems = this._authorizationContract.GetInfoSystems().OrderBy(x => x.Number);
            IDictionary<string, string> applicationTypeDescriptions = typeof(ApplicationType).GetEnumMembers();

            foreach (InfoSystemInfo infoSystem in infoSystems)
            {
                this.Cbx_InfoSystems.Items.Add(infoSystem);
            }
            foreach (KeyValuePair<string, string> kv in applicationTypeDescriptions)
            {
                this.Cbx_ApplicationTypes.Items.Add(kv);
            }

            this.Cbx_InfoSystems.DisplayMember = nameof(InfoSystemInfo.Name);
            this.Cbx_ApplicationTypes.DisplayMember = "Value";
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
            if (this.Cbx_InfoSystems.SelectedItem == null)
            {
                MessageBox.Show(@"信息系统不可为空！", @"Warning");
                return;
            }
            if (this.Cbx_ApplicationTypes.SelectedItem == null)
            {
                MessageBox.Show(@"应用程序类型不可为空！", @"Warning");
                return;
            }

            #endregion

            string assemblyPath = this.Txt_FilePath.Text;

            InfoSystemInfo selectedSystemItem = (InfoSystemInfo)this.Cbx_InfoSystems.SelectedItem;
            KeyValuePair<string, string> selectedApplicationTypeItem = (KeyValuePair<string, string>)this.Cbx_ApplicationTypes.SelectedItem;

            string infoSystemNo = selectedSystemItem.Number;
            ApplicationType applicationType = (ApplicationType)Enum.Parse(typeof(ApplicationType), selectedApplicationTypeItem.Key);

            await this.InitAuthorities(assemblyPath, infoSystemNo, applicationType);
            MessageBox.Show(@"OK", @"OK");
        }

        /// <summary>
        /// 初始化权限集
        /// </summary>
        /// <param name="assemblyPath">程序集路径</param>
        /// <param name="infoSystemNo">信息系统编号</param>
        /// <param name="applicationType">应用程序类型</param>
        private async Task InitAuthorities(string assemblyPath, string infoSystemNo, ApplicationType applicationType)
        {
            //加载程序集、加载权限
            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            Type[] types = assembly.GetTypes();

            //加载需认证的方法
            IEnumerable<MethodInfo> methodInfos = types.SelectMany(x => x.GetMethods()).Where(x => x.IsDefined(typeof(RequireAuthorizationAttribute), true));

            //构造权限参数模型集
            IList<AuthorityParam> authorityParams = new List<AuthorityParam>();
            foreach (MethodInfo methodInfo in methodInfos)
            {
                object[] attributes = methodInfo.GetCustomAttributes(typeof(RequireAuthorizationAttribute), true);
                RequireAuthorizationAttribute attribute = (RequireAuthorizationAttribute)(attributes[0]);
                AuthorityParam authorityParam = new AuthorityParam
                {
                    authorityName = attribute.AuthorityPath,
                    authorityPath = attribute.AuthorityPath
                };
                authorityParams.Add(authorityParam);
            }

            await TaskEx.Run(() => this._authorizationContract.CreateAuthorities(infoSystemNo, applicationType, authorityParams));
        }
    }
}
