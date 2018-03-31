using System.Data.Entity;

namespace Database
{
    public class OnlineDatabase : Context
    {
        public OnlineDatabase() : base("name=OnlineDatabase")
        {
            //
        }
    }
}
