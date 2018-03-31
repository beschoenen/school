using System.Windows.Controls;
using SOh_ParkInspect.Control.Template;
using SOh_ParkInspect.Helper;

namespace SOh_ParkInspect.ViewModel.Template
{
    public class TemplateViewModel : ParkInspectViewModel, ITemplateWrapper
    {
        public TemplateViewModel(Database.Question question)
        {
            Question = question;
        }

        public Database.Question Question { get; }

        public UserControl Control => new TemplateControl { DataContext = this };

        public bool IsEditing { get; set; }
    }
}