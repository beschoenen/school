using Database;
using GalaSoft.MvvmLight;

namespace SOh_ParkInspect.ViewModel.Control
{
    public class TemplateChecklistViewModel : ViewModelBase
    {
        public Checklist Checklist { get; set; }
        
        public int VersionNr
        {
            get
            {
                var previousChecklist = Checklist.Parent;
                var count = 0;

                while (true)
                {
                    if (previousChecklist == null) return count;

                    previousChecklist = previousChecklist.Parent;
                    count++;
                }
            }
        }

        public TemplateChecklistViewModel(Checklist checklist)
        {
            Checklist = checklist;
        }
    }
}
