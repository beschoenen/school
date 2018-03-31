using DPA_Musicsheets.ViewModels;

namespace DPA_Musicsheets.Refactor.EditorStates
{
    public abstract class BaseEditorState
    {
        protected readonly LilypondViewModel ViewModel;
        
        public bool ReadOnly { get; protected set; }

        protected BaseEditorState(LilypondViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public abstract void SetLilypondText(string text);
    }
}
