using System;
using System.Collections.Generic;
using System.Linq;

namespace DPA_Musicsheets.Refactor.Models.Base
{
    public abstract class BlockElement : Element
    {
        public Enums.NoteType Staffs;

        public Enums.ClefType Clef;

        public TimeSignature TimeSignature;

        public Tempo Tempo;

        public readonly List<Element> Elements = new List<Element>();

        public void AddElement(Element element, bool isFixingBars)
        {
            if (!isFixingBars)
            {
                Elements.Add(element);
                return;
            }

            if (element?.GetType() != typeof(Bar))
            {
                AddBarIfNeeded();
                Elements.Add(element);
            }
        }

        private void AddBarIfNeeded()
        {
            var lastBar = -1;
            try
            {
                lastBar = Elements.FindLastIndex(e => e.GetType() == typeof(Bar));
            }
            catch (Exception) { }

            double totalTime = TimeSignature.Top;
            for (var i = lastBar + 1; i < Elements.Count; i++)
            {
                if (!(Elements[i] is MusicElement) || Elements[i] is Bar) break;
                var element = (MusicElement) Elements[i];

                var time = TimeSignature.Bottom / (double) element.DurationType;
                for (var j = 0; j < element.Dots; j++)
                {
                    time += time / 2;
                }

                totalTime -= time;
            }

            if (totalTime <= 0)
            {
                Elements.Add(new Bar());
            }
        }
    }
}
