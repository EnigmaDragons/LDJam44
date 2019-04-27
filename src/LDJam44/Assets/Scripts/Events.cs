using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Event
{
    private static readonly List<EventSubscription> EventSubs = new List<EventSubscription>();
    private static readonly Events TransientEvents = new Events();
    private static readonly Events PersistentEvents = new Events();

    public static int SubscriptionCount => TransientEvents.SubscriptionCount + PersistentEvents.SubscriptionCount;
    
    public static void Publish(object payload)
    {
       Debug.Log(payload);
        TransientEvents.Publish(payload);
        PersistentEvents.Publish(payload);
    }

    public static void PublishLocally(object payload)
    {
        Debug.Log(payload);
        TransientEvents.Publish(payload);
        PersistentEvents.Publish(payload);
    }
    
    public static void SubscribeForever(EventSubscription subscription)
    {
        PersistentEvents.Subscribe(subscription);
    }

    public static void Subscribe<T>(Action<T> onEvent, object owner)
    {
        Subscribe(EventSubscription.Create<T>(onEvent, owner));
    }
    
    public static void Subscribe(EventSubscription subscription)
    {
        TransientEvents.Subscribe(subscription);
        EventSubs.Add(subscription);
    }

    public static void Unsubscribe(object owner)
    {
        TransientEvents.Unsubscribe(owner);
        PersistentEvents.Unsubscribe(owner);
        EventSubs.Where(x => x.Owner.Equals(owner)).ToList().ForEach(x =>
        {
            EventSubs.Remove(x);
        });
    }
}

public sealed class Events
{
    private readonly Dictionary<Type, List<object>> _eventActions = new Dictionary<Type, List<object>>();
    private readonly Dictionary<object, List<EventSubscription>> _ownerSubscriptions = new Dictionary<object, List<EventSubscription>>();

    public int SubscriptionCount => _eventActions.Sum(e => e.Value.Count);

    public void Publish(object payload)
    {
        var eventType = payload.GetType();
        // TODO: Make this class an automaton instead of using Threading
        if (_eventActions.ContainsKey(eventType))
            foreach (var action in _eventActions[eventType].ToList())
                ((Action<object>)action)(payload);
    }

    public void Subscribe(EventSubscription subscription)
    {
        var eventType = subscription.EventType;
        if (!_eventActions.ContainsKey(eventType))
            _eventActions[eventType] = new List<object>();
        if (!_ownerSubscriptions.ContainsKey(subscription.Owner))
            _ownerSubscriptions[subscription.Owner] = new List<EventSubscription>();
        _eventActions[eventType].Add(subscription.OnEvent);
        _ownerSubscriptions[subscription.Owner].Add(subscription);
    }

    public void Unsubscribe(object owner)
    {
        if (!_ownerSubscriptions.ContainsKey(owner))
            return;
        var events = _ownerSubscriptions[owner];
        for (var i = 0; i < _eventActions.Count; i++)
            _eventActions.ElementAt(i).Value.RemoveAll(x => events.Any(y => y.OnEvent.Equals(x)));
        _ownerSubscriptions.Remove(owner);
    }
}

public sealed class EventSubscription : IDisposable
{
    public Type EventType { get; }
    public Action<object> OnEvent { get; }
    public object Owner { get; }

    internal EventSubscription(Type eventType, Action<object> onEvent, object owner)
    {
        EventType = eventType;
        OnEvent = onEvent;
        Owner = owner;
    }

    public void Dispose()
    {
        Event.Unsubscribe(Owner);
    }      

    public static EventSubscription Create<T>(Action<T> onEvent, object owner)
    {
        return new EventSubscription(typeof(T), (o) => { if (o.GetType() == typeof(T)) onEvent((T)o); }, owner);
    }
}
