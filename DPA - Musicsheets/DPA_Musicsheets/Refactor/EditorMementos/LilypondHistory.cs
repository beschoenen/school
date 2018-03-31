using System.ComponentModel;
using System.Runtime.CompilerServices;
using DPA_Musicsheets.Annotations;

namespace DPA_Musicsheets.Refactor.EditorMementos
{
    public sealed class LilypondHistory : INotifyPropertyChanged
    {
        private readonly Originator _originator = new Originator();

        private string _currentLilypondText = string.Empty;
        private Caretaker _caretaker = new Caretaker();

        private int _saveFiles;
        private int _currentLilypondTextIndex = -1;

        public bool CanUndo => _currentLilypondTextIndex >= 1;
        public bool CanRedo => _saveFiles - 1 > _currentLilypondTextIndex;

        public void Add(string text)
        {
            if (_currentLilypondText.Equals(text)) return;

            if (CanRedo)
            {
                var count = _caretaker.RemoveMementos(_currentLilypondTextIndex + 1);
                _saveFiles -= count;
            }

            _originator.Set(text);
            _caretaker.AddMemento(_originator.StoreInMemento());
            _saveFiles++;
            _currentLilypondTextIndex++;
            _currentLilypondText = text;

            OnPropertyChanged(nameof(CanRedo));
            OnPropertyChanged(nameof(CanUndo));
        }

        public string Undo()
        {
            if (!CanUndo) return null;

            _currentLilypondTextIndex--;
            var lilypondText = _originator.RestoreFromMemento(_caretaker.GetMemento(_currentLilypondTextIndex));
            _currentLilypondText = lilypondText;

            return lilypondText;
        }

        public string Redo()
        {
            if (!CanRedo) return null;

            _currentLilypondTextIndex++;
            var lilypondText = _originator.RestoreFromMemento(_caretaker.GetMemento(_currentLilypondTextIndex));
            _currentLilypondText = lilypondText;

            return lilypondText;
        }

        public void Clear()
        {
            _saveFiles = 0;
            _currentLilypondTextIndex = -1;
            _caretaker = new Caretaker();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}