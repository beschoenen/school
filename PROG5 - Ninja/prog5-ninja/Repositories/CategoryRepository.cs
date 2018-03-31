using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using database;

namespace prog5_ninja.Repositories
{
    public class CategoryRepository
    {
        private static Entities Context => Database.Instance.Context;

        private static CategoryRepository _instance;
        public static CategoryRepository Instance => _instance ?? (_instance = new CategoryRepository());

        public ObservableCollection<category> All => new ObservableCollection<category>(Context.categories.Local.ToList());

        private CategoryRepository()
        {
            Context.categories.Load();
        }
    }
}
