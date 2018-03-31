using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SOh_ParkInspect.Helper
{
    public class ListBoxExt : ListBox
    {
        new DependencyProperty SelectedItemsProperty = DependencyProperty.Register("SelectedItems", typeof(ObservableCollection<string>), typeof(ListBoxExt));

        new public ObservableCollection<string> SelectedItems
        {
            get { return (ObservableCollection<string>) GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty,value);}
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            if (this.SelectedItems != null)
            {
                this.SelectedItems.Clear();
                ;
                base.SelectedItems.Cast<string>().ToList().ForEach(this.SelectedItems.Add);
            }
        }
    }
}
