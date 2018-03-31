using System.ComponentModel;
using System.Runtime.CompilerServices;
using database;
using prog5_ninja.Annotations;

namespace prog5_ninja.Model
{
    public class NinjaCrudModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ninja OriginalNinja { get; }

        public string Name { get; set; }

        public NinjaCrudModel(ninja ninja = null)
        {
            OriginalNinja = ninja ?? new ninja();

            Name = OriginalNinja.name;
        }

        public ninja GetModel()
        {
            OriginalNinja.name = Name;

            return OriginalNinja;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
