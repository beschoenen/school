using DPA_Musicsheets.Refactor.Models.Base;

namespace DPA_Musicsheets.Refactor.Builders
{
    public class AbstractBuilder<TBuilder, TElement> where TBuilder : AbstractBuilder<TBuilder, TElement> where TElement : MusicElement
    {
        protected TElement Element;

        public TElement Build()
        {
            return Element;
        }

        public TBuilder IsDotted(int dots = 1)
        {
            Element.Dots = dots;

            return (TBuilder) this;
        }

        public TBuilder AddDots(int dots = 1)
        {
            Element.Dots += dots;

            return (TBuilder) this;
        }

        public TBuilder Duration(Enums.DurationType duration)
        {
            Element.DurationType = duration;

            return (TBuilder) this;
        }
    }
}