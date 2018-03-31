using System;
using DPA_Musicsheets.Refactor.EventHandler.Events;
using DPA_Musicsheets.Refactor.EventHandler.Interfaces;

namespace DPA_Musicsheets.Refactor.EventHandler
{
    internal class Subscription<TEventBase> : ISubscription where TEventBase : EventBase
    {
        private readonly Action<TEventBase> _action;

        public SubscriptionToken SubscriptionToken { get; }

        public Subscription(Action<TEventBase> action, SubscriptionToken token)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
            SubscriptionToken = token ?? throw new ArgumentNullException(nameof(token));
        }

        public void Fire(EventBase eventItem)
        {
            if (!(eventItem is TEventBase))
            {
                throw new ArgumentException("Event Item is not the correct type.");
            }

            _action.Invoke((TEventBase) eventItem);
        }
    }
}
