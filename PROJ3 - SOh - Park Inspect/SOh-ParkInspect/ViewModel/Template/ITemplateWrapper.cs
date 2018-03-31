using System.Windows.Controls;

namespace SOh_ParkInspect.ViewModel.Template
{
    public interface ITemplateWrapper
    {
        Database.Question Question { get; }

        UserControl Control { get; }

        bool IsEditing { get; set; }
    }
}