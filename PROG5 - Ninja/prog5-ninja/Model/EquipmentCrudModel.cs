using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using database;
using prog5_ninja.Annotations;
using prog5_ninja.Repositories;

namespace prog5_ninja.Model
{
    public class EquipmentCrudModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public equipment OriginalEquipment { get; }

        public string Name { get; set; }

        private string _image;
        public string Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }

        public category Category { get; set; }
        public int Value { get; set; }
        public int Strength { get; set; }
        public int Intelligence { get; set; }
        public int Agility { get; set; }

        public EquipmentCrudModel(equipment equipment = null)
        {
            OriginalEquipment = equipment ?? new equipment();

            Name = OriginalEquipment.name;
            Image = OriginalEquipment.image;
            Category = CategoryRepository.Instance.All.FirstOrDefault(e => e.name == OriginalEquipment.category?.name);
            Value = OriginalEquipment.value;
            Strength = OriginalEquipment.strength;
            Intelligence = OriginalEquipment.intelligence;
            Agility = OriginalEquipment.agility;
        }

        public equipment GetModel()
        {
            OriginalEquipment.name = Name;
            OriginalEquipment.image = Image;
            OriginalEquipment.category = Category;
            OriginalEquipment.value = Value;
            OriginalEquipment.strength = Strength;
            OriginalEquipment.intelligence = Intelligence;
            OriginalEquipment.agility = Agility;

            return OriginalEquipment;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool CategoryHasChanged()
        {
            if (OriginalEquipment.category == null) return false;

            return OriginalEquipment.category.name != Category.name;
        }

        public void ResetCategory()
        {
            Category = OriginalEquipment.category;
        }
    }
}
