using Caliburn.Micro;
using SD.IdentitySystem.Client.Models;
using SD.IOC.Core.Mediator;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SD.IdentitySystem.Client.ViewModels
{
    /// <summary>
    /// 主窗体
    /// </summary>
    public class MainViewModel : PropertyChangedBase, IDocumentManager
    {
        #region # 依赖注入构造器
        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public MainViewModel()
        {
            this.Menus = new BindableCollection<Menu>();


            //初始化菜单
            this.InitMenus();
        }


        public DocumentBase this[string url]
        {
            get
            {
                if (this.Documents == null)
                {
                    this.Documents = new ObservableCollection<DocumentBase>();
                }

                DocumentBase document = this.Documents.SingleOrDefault(item => item.Url == url);

                if (document == null)
                {
                    Type type = Type.GetType(url);

                    if (type == null)
                    {
                        return null;
                    }

                    document = (DocumentBase)ResolveMediator.Resolve(type);

                    this.Documents.Add(document);
                }

                return document;
            }
        }

        #endregion

        #region # 属性

        #region 菜单列表 —— BindableCollection<Menu> Menus
        /// <summary>
        /// 菜单列表
        /// </summary>
        public BindableCollection<Menu> Menus { get; set; }
        #endregion

        #region 文档列表 —— BindableCollection<DocumentBase> Documents
        /// <summary>
        /// 文档列表
        /// </summary>
        private ObservableCollection<DocumentBase> _documents;

        /// <summary>
        /// 文档列表
        /// </summary>
        public ObservableCollection<DocumentBase> Documents
        {
            get { return this._documents; }
            set { this.Set(ref this._documents, value); }
        }

        #endregion

        #endregion

        #region # 方法

        #region 初始化菜单 —— void InitMenus()
        /// <summary>
        /// 初始化菜单
        /// </summary>
        public void InitMenus()
        {
            Menu menu1 = new Menu("文件", "File", 1);
            Menu menu11 = new Menu("新建", "SD.IdentitySystem.Client.ViewModels.TestViewModel", 1);
            Menu menu12 = new Menu("打开", "Open", 2);
            menu1.MenuItems.Add(menu11);
            menu1.MenuItems.Add(menu12);

            Menu menu2 = new Menu("编辑", "Eidt", 2);
            Menu menu21 = new Menu("撤销", "U", 1);
            Menu menu22 = new Menu("重做", "F", 2);
            menu2.MenuItems.Add(menu21);
            menu2.MenuItems.Add(menu22);


            this.Menus.Add(menu1);
            this.Menus.Add(menu2);
        }
        #endregion


        public void Navigate(Menu menu)
        {
            DocumentBase document = this[menu.Url];

            if (document != null)
            {
                document.Open();
            }
        }

        #endregion
    }
}
