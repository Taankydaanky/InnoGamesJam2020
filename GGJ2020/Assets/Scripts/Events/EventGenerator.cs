using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGenerator : MonoBehaviour
{
    public Event[] captainEvents;
    public Event[] environmentEvents;
    public Event[] groundEvents;

    public int captianEventDistance;
    public int environmentEventDistance;
    public int groundEventDistance;

    public EventTiming[] captainEventTimings;
    public EventTiming[] environmentEventTimings;
    public EventTiming[] groundEventTimings;

    private List<GeneratedEvent> generatedTimeline;

    void Start()
    {
        generatedTimeline = new List<GeneratedEvent>();
        generatedTimeline.AddRange(GenerateEventTimeline(captainEvents, captianEventDistance, captainEventTimings));
        generatedTimeline.AddRange(GenerateEventTimeline(environmentEvents, environmentEventDistance, environmentEventTimings));
        generatedTimeline.AddRange(GenerateEventTimeline(groundEvents, groundEventDistance, groundEventTimings));
    }

    List<GeneratedEvent> GenerateEventTimeline(Event[] events, int eventDistance, EventTiming[] timings)
    {
        List<GeneratedEvent> generatedEvents = new List<GeneratedEvent>();

        Queue<Event> usedEvents = new Queue<Event>();
        List<Event> availableEvents = new List<Event>(events);

        foreach(EventTiming timing in timings)
        {
            Event usedEvent;
            if(availableEvents.Count > 0)
            {
                usedEvent = availableEvents[Random.Range(0, availableEvents.Count)];
                availableEvents.Remove(usedEvent);
            }
            else
            {
                usedEvent = usedEvents.Dequeue();
            }

            float start = timing.triggerTimeInSeconds - timing.negativeVariability;
            float end = timing.triggerTimeInSeconds + timing.positiveVariability;
            float time = Random.Range(start, end);

            generatedEvents.Add(new GeneratedEvent(usedEvent, time));

            usedEvents.Enqueue(usedEvent);

            if (usedEvents.Count >= eventDistance)
            {
                Event dequeued = usedEvents.Dequeue();
                availableEvents.Add(dequeued);
            }
        }

        return generatedEvents;
    }

    // TODO Time source
    void Update()
    {
        float time = 0;
    }
}
