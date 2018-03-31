/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:prog5_ninja"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using prog5_ninja.Repositories;

namespace prog5_ninja.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<ShopViewModel>();
            SimpleIoc.Default.Register<NinjaViewModel>();

            SimpleIoc.Default.Register<NinjaModificationViewModel>();
            SimpleIoc.Default.Register<NinjaManagementViewModel>();

            SimpleIoc.Default.Register<EquipmentModificationViewModel>();
            SimpleIoc.Default.Register<EquipmentManagementViewModel>();

            SimpleIoc.Default.Register(() => EquipmentRepository.Instance);
            SimpleIoc.Default.Register(() => NinjaRepository.Instance);
            SimpleIoc.Default.Register(() => CategoryRepository.Instance);
        }

        public ShopViewModel ShopViewModel => ServiceLocator.Current.GetInstance<ShopViewModel>();
        public NinjaViewModel NinjaViewModel => ServiceLocator.Current.GetInstance<NinjaViewModel>();

        public NinjaModificationViewModel NinjaModificationViewModel => ServiceLocator.Current.GetInstance<NinjaModificationViewModel>();
        public NinjaManagementViewModel NinjaManagementViewModel => ServiceLocator.Current.GetInstance<NinjaManagementViewModel>();

        public EquipmentModificationViewModel EquipmentModificationViewModel => ServiceLocator.Current.GetInstance<EquipmentModificationViewModel>();
        public EquipmentManagementViewModel EquipmentManagementViewModel => ServiceLocator.Current.GetInstance<EquipmentManagementViewModel>();

        public EquipmentRepository EquipmentRepository => ServiceLocator.Current.GetInstance<EquipmentRepository>();
        public NinjaRepository NinjaRepository => ServiceLocator.Current.GetInstance<NinjaRepository>();
        public CategoryRepository CategoryRepository => ServiceLocator.Current.GetInstance<CategoryRepository>();
    }
}