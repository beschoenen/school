using System;

namespace DPA_Musicsheets.Refactor.EditorMementos
{
    public class Originator
    {
        private string _lilypondText;

        public void Set(string lilypondText)
        {
            _lilypondText = lilypondText;
        }

        public Memento StoreInMemento()
        {
            return new Memento(_lilypondText);
        }

        public string RestoreFromMemento(Memento memento)
        {
            _lilypondText = memento.GetSavedLilypondText();
            return _lilypondText;
        }
    }
}