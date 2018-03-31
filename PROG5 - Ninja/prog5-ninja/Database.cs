using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using database;

namespace prog5_ninja
{
    // Database singleton so that we won't get cross-context update errors

    public class Database
    {
        public Entities Context;

        private static Database _instance;
        public static Database Instance => _instance ?? (_instance = new Database());

        private Database()
        {
            Context = new Entities();
        }
    }
}
