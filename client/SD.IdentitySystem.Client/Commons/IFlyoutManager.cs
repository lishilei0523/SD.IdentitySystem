using Caliburn.Micro;

namespace SD.IdentitySystem.Client.Commons
{
    /// <summary>
    /// 飞窗管理器
    /// </summary>
    public interface IFlyoutManager
    {
        BindableCollection<FlyoutBase> Flyouts { get; set; }
    }
}
