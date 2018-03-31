using GalaSoft.MvvmLight;

namespace SOh_ParkInspect.Helper
{
    public abstract class ParkInspectViewModel : ViewModelBase
    {
        public dynamic ViewBag { get; set; }

        public virtual void OnEnter()
        {
            // Will be executed when the user is navigated to this UserControl
        }

        public virtual void OnLeave()
        {
            // Will be executed when the user is navigated to another UserControl
        }
    }
}
