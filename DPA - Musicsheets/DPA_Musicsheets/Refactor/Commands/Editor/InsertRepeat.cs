namespace DPA_Musicsheets.Refactor.Commands.Editor
{
    public class InsertRepeat : AbstractEditorCommand
    {
        public override void execute()
        {
            insert("\\repeat 2 {\n", null, false);
            insert("\n}\n", Offset + 10);
        }
    }
}
