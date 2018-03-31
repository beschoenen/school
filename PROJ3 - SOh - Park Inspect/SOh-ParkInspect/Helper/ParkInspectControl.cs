using System.Windows.Controls;

namespace SOh_ParkInspect.Helper
{
    public class ParkInspectControl : UserControl
    {
        public dynamic ViewBag
        {
            get { return ((ParkInspectViewModel) DataContext).ViewBag; }
            set { ((ParkInspectViewModel) DataContext).ViewBag = value; }
        }
    }
}
