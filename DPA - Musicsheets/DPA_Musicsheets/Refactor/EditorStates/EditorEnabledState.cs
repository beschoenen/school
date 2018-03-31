using DPA_Musicsheets.ViewModels;

namespace DPA_Musicsheets.Refactor.EditorStates
{
    public class EditorEnabledState : BaseEditorState
    {
        public EditorEnabledState(LilypondViewModel viewModel) : base(viewModel)
        {
            ReadOnly = false;
        }

        public override void SetLilypondText(string text)
        {
            ViewModel.LilypondText = text;
        }
    }
}
