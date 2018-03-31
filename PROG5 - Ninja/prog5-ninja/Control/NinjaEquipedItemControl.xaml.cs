using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using database;
using Microsoft.Practices.ServiceLocation;
using prog5_ninja.Annotations;
using prog5_ninja.Repositories;

namespace prog5_ninja.Control
{
    public partial class NinjaEquipedItemControl : INotifyPropertyChanged
    {
        public static readonly DependencyProperty CategoryProperty = DependencyProperty.Register("Category", typeof(string), typeof(NinjaEquipedItemControl));
        
        public event PropertyChangedEventHandler PropertyChanged;

        public string Category
        {
            get { return GetValue(CategoryProperty) as string; }
            set { SetValue(CategoryProperty, value); }
        }

        public equipment Item => NinjaRepository.Instance.CurrentNinja?.equipment.FirstOrDefault(e => e.category.name == Category);
        public string Image => Item?.image ?? string.Empty;

        public NinjaEquipedItemControl()
        {
            InitializeComponent();

            NinjaRepository.Instance.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "CurrentNinja")
                {
                    OnPropertyChanged(nameof(Item));
                    OnPropertyChanged(nameof(Image));
                }
            };
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
