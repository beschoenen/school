using System;

namespace DPA_Musicsheets.Refactor.EventHandler
{
    public class SubscriptionToken
    {
        public Guid Token { get; }
        public Type EventItemType { get; }

        internal SubscriptionToken(Type eventItemType)
        {
            Token = Guid.NewGuid();
            EventItemType = eventItemType;
        }
    }
}
