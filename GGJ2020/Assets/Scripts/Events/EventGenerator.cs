using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventGenerator : MonoBehaviour
{
    public DurationEvent[] captainEvents;
    public DurationEvent[] environmentEvents;
    public DurationEvent[] groundEvents;

    public int captianEventDistance;
    public int environmentEventDistance;
    public int groundEventDistance;

    public EventTiming[] captainEventTimings;
    public EventTiming[] environmentEventTimings;
    public EventTiming[] groundEventTimings;

    private List<GeneratedEvent> generatedTimeline;
    private List<GeneratedEvent> runningEvents;
    private List<GeneratedEvent> environmentEventList;
    private Dictionary<GeneratedEvent,Slider> environmentEventSliders;
    public List<GeneratedEvent> environmentGeneratedEvents { get => environmentEventList; }

    [SerializeField] private GameTimer timer;
    [SerializeField] private GameObject desasterSliderPrefab;
    [SerializeField] private Transform desasterTimeline;

    void Start()
    {
        generatedTimeline = new List<GeneratedEvent>();
        generatedTimeline.AddRange(GenerateEventTimeline(captainEvents, captianEventDistance, captainEventTimings));
        environmentEventList = GenerateEventTimeline(environmentEvents, environmentEventDistance, environmentEventTimings);
        generatedTimeline.AddRange(environmentEventList);
        generatedTimeline.AddRange(GenerateEventTimeline(groundEvents, groundEventDistance, groundEventTimings));
        runningEvents = new List<GeneratedEvent>();
        ShowEnvironmentEvents();
    }

    List<GeneratedEvent> GenerateEventTimeline(DurationEvent[] events, int eventDistance, EventTiming[] timings)
    {
        List<GeneratedEvent> generatedEvents = new List<GeneratedEvent>();

        Queue<DurationEvent> usedEvents = new Queue<DurationEvent>();
        List<DurationEvent> availableEvents = new List<DurationEvent>(events);

        foreach(EventTiming timing in timings)
        {
            DurationEvent usedEvent;
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
                DurationEvent dequeued = usedEvents.Dequeue();
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
    
    public void UpdateEvents(float time)
    {
        foreach(GeneratedEvent ev in runningEvents)
        {
            ev.execEvent.UpdateEvent(time - ev.time);
        }
        foreach (GeneratedEvent ev in environmentEventList)
        {
            environmentEventSliders[ev].value = (ev.time - time) / timer.maxSeconds;
        }
    }

    private void ShowEnvironmentEvents()
    {
        environmentEventSliders = new Dictionary<GeneratedEvent, Slider>();
        foreach(GeneratedEvent ev in environmentEventList)
        {
            Slider eventSlider = Instantiate(desasterSliderPrefab, desasterTimeline).GetComponentInChildren<Slider>();
            eventSlider.value =  ev.time / timer.maxSeconds;
            eventSlider.targetGraphic.GetComponent<Image>().sprite = ev.execEvent.eventIcon;
            environmentEventSliders.Add(ev,eventSlider);
        }
    }
}
