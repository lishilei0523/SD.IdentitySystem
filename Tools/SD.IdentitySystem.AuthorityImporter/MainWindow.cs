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

namespace SD.IdentitySystem.AuthorityImporter
{
    /// <summary>
    /// 主窗体
    /// </summary>
    public partial class MainWindow : Form
    {
        #region # 字段及构造器

        /// <summary>
        /// 身份认证服务契约接口
        /// </summary>
        private readonly IAuthenticationContract _authenticationContract;

        /// <summary>
        /// 权限管理服务契约接口
        /// </summary>
        private readonly IAuthorizationContract _authorizationContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
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
            AppDomain.CurrentDomain.SetData(GlobalSetting.ApplicationId, loginInfo);
        }

        #endregion

        #region # 窗体加载事件 —— void MainWindow_Load(object sender, EventArgs eventArgs)
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void MainWindow_Load(object sender, EventArgs eventArgs)
        {
            IEnumerable<InfoSystemInfo> infoSystems = this._authorizationContract.GetInfoSystems(null);
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
        #endregion

        #region # 打开文件事件 —— void Btn_OpenFile_Click(object sender, EventArgs eventArgs)
        /// <summary>
        /// 打开文件事件
        /// </summary>
        private void Btn_OpenFile_Click(object sender, EventArgs eventArgs)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = @"dll files (*.dll)|*.dll" };
            openFileDialog.ShowDialog();
            this.Txt_FilePath.Text = openFileDialog.FileName;
        }
        #endregion

        #region # 初始化事件 —— void Btn_Init_Click(object sender, EventArgs eventArgs)
        /// <summary>
        /// 初始化事件
        /// </summary>
        private async void Btn_Init_Click(object sender, EventArgs eventArgs)
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
        #endregion

        #region # 初始化权限 —— Task InitAuthorities(string assemblyPath, string infoSystemNo...
        /// <summary>
        /// 初始化权限
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
            IEnumerable<MethodInfo> methodInfos = types.SelectMany(x => x.GetMethods()).Where(x => x.IsDefined(typeof(RequireAuthorizationAttribute), false));

            //构造权限参数模型集
            IList<AuthorityParam> authorityParams = new List<AuthorityParam>();
            foreach (MethodInfo methodInfo in methodInfos)
            {
                object[] attributes = methodInfo.GetCustomAttributes(typeof(RequireAuthorizationAttribute), false);
                RequireAuthorizationAttribute attribute = (RequireAuthorizationAttribute)(attributes[0]);
                AuthorityParam authorityParam = new AuthorityParam
                {
                    authorityName = attribute.AuthorityPath,
                    authorityPath = attribute.AuthorityPath
                };
                authorityParams.Add(authorityParam);
            }

            await Task.Factory.StartNew(() => this._authorizationContract.CreateAuthorities(infoSystemNo, applicationType, authorityParams));
        }
        #endregion
    }
}
