namespace DPA_Musicsheets.Refactor.EditorMementos
{
    public class Memento
    {
        private readonly string _lilypondText;

        public Memento(string lilypondText)
        {
            _lilypondText = lilypondText;
        }

        public string GetSavedLilypondText()
        {
            return _lilypondText;
        }
    }
}