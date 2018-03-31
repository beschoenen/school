using DPA_Musicsheets.ViewModels;

namespace DPA_Musicsheets.Refactor.EditorStates
{
    public class EditorDisabledState : BaseEditorState
    {
        public EditorDisabledState(LilypondViewModel viewModel) : base(viewModel)
        {
            ReadOnly = true;
        }

        public override void SetLilypondText(string text)
        {
            // Do nothing cause we're disabled
        }
    }
}
