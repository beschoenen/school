using System.Collections.Generic;
using System.Linq;

namespace DPA_Musicsheets.Refactor.EditorMementos
{
    public class Caretaker
    {
        private readonly List<Memento> _savedMementos;

        public Caretaker()
        {
            _savedMementos = new List<Memento>();
        }

        public void AddMemento(Memento momento)
        {
            _savedMementos.Add(momento);
        }

        public Memento GetMemento(int index)
        {
            return _savedMementos.ElementAt(index);
        }

        public int RemoveMementos(int startIndex)
        {
            var count = _savedMementos.Count - startIndex;
            _savedMementos.RemoveRange(startIndex, count);

            return count;
        }
    }
}