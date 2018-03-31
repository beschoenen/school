using System;
using System.Collections.Generic;
using System.Linq;
using DPA_Musicsheets.Refactor.EventHandler.Events;
using DPA_Musicsheets.Refactor.EventHandler.Interfaces;

namespace DPA_Musicsheets.Refactor.EventHandler
{
    public class EventBus
    {
        private static readonly Dictionary<Type, List<ISubscription>> Subscriptions = new Dictionary<Type, List<ISubscription>>();

        public static SubscriptionToken Subscribe<TEventBase>(Action<TEventBase> action) where TEventBase : EventBase
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            if (!Subscriptions.ContainsKey(typeof(TEventBase)))
            {
                Subscriptions.Add(typeof(TEventBase), new List<ISubscription>());
            }

            var token = new SubscriptionToken(typeof(TEventBase));
            Subscriptions[typeof(TEventBase)].Add(new Subscription<TEventBase>(action, token));

            return token;
        }

        public static void Unsubscribe(SubscriptionToken token)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));

            if (!Subscriptions.ContainsKey(token.EventItemType)) return;

            var allSubscriptions = Subscriptions[token.EventItemType];
            var subscriptionToRemove = allSubscriptions.FirstOrDefault(x => x.SubscriptionToken.Token == token.Token);

            if (subscriptionToRemove != null)
            {
                Subscriptions[token.EventItemType].Remove(subscriptionToRemove);
            }
        }

        public static void Fire<TEventBase>(TEventBase eventItem) where TEventBase : EventBase
        {
            if (eventItem == null) throw new ArgumentNullException(nameof(eventItem));

            var allSubscriptions = new List<ISubscription>();

            if (Subscriptions.ContainsKey(typeof(TEventBase)))
            {
                allSubscriptions = Subscriptions[typeof(TEventBase)];
            }

            foreach (var subscription in allSubscriptions)
            {
                try
                {
                    subscription.Fire(eventItem);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }
    }
}
