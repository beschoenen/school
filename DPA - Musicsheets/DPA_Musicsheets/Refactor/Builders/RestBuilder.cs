using DPA_Musicsheets.Refactor.Models;

namespace DPA_Musicsheets.Refactor.Builders
{
    internal class RestBuilder : AbstractBuilder<RestBuilder, Rest>
    {
        public RestBuilder()
        {
            Element = new Rest();
        }
    }
}