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
    private List<GeneratedEvent> runningEvents;

    void Start()
    {
        generatedTimeline = new List<GeneratedEvent>();
        generatedTimeline.AddRange(GenerateEventTimeline(captainEvents, captianEventDistance, captainEventTimings));
        generatedTimeline.AddRange(GenerateEventTimeline(environmentEvents, environmentEventDistance, environmentEventTimings));
        generatedTimeline.AddRange(GenerateEventTimeline(groundEvents, groundEventDistance, groundEventTimings));
        runningEvents = new List<GeneratedEvent>();
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

    public void UpdateRunningEvents(float time)
    {
        List<GeneratedEvent> activatedEvents = new List<GeneratedEvent>();

        foreach (GeneratedEvent ev in generatedTimeline)
        {
            if (ev.Activate(time))
            {
                activatedEvents.Add(ev);
            }
        }

        runningEvents.AddRange(activatedEvents);
        generatedTimeline.RemoveAll((e) => activatedEvents.Contains(e));

        List<GeneratedEvent> endedEvents = runningEvents.FindAll(e => e.execEvent.HasEnded(time - e.time));
        runningEvents.RemoveAll(e => e.execEvent.HasEnded(time - e.time));

        endedEvents.ForEach(e => e.execEvent.End(time - e.time));
    }
    
    public void Update(float time)
    {
        foreach(GeneratedEvent ev in runningEvents)
        {
            ev.execEvent.Update(time - ev.time);
        }
    }
}
