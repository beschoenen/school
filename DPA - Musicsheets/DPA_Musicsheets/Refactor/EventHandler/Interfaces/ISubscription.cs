using DPA_Musicsheets.Refactor.EventHandler.Events;

namespace DPA_Musicsheets.Refactor.EventHandler.Interfaces
{
    public interface ISubscription
    {
        SubscriptionToken SubscriptionToken { get; }

        void Fire(EventBase eventBase);
    }
}
