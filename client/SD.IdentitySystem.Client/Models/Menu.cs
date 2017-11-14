using Caliburn.Micro;
using SD.IdentitySystem.Client.Annotations;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SD.IdentitySystem.Client.Models
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class Menu : INotifyPropertyChanged
    {
        public Menu()
        {
            this.MenuItems = new BindableCollection<Menu>();
        }

        /// <summary>
        /// 
        /// </summary>
        public Menu(string name, string url, int sort)
            : this()
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Url = url;
            this.Sort = sort;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int Sort { get; set; }
        public BindableCollection<Menu> MenuItems { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
