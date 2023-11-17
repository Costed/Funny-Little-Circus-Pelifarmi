using UnityEngine.Events;
using UnityEngine;

public class RecordStateOnActivation : ActionOnActivation
{
    [SerializeField] UnityEvent trueEvent;
    [SerializeField] UnityEvent falseEvent;

    [SerializeField] bool onlyTrueLoad;
    [SerializeField] bool onlyFalseLoad;
    [SerializeField] UnityEvent loadTrueEvent;
    [SerializeField] UnityEvent loadFalseEvent;

    [SerializeField] bool defaultState;
    bool state;

    StateRecorder recorder;

    void Awake()
    {
        state = defaultState;

        recorder = new StateRecorder(transform, state);
        recorder.OnStateLoad += LoadInvoke;
    }

    public override void Activated()
    {
        state = !state;
        recorder.RecordState(state);

        InvokeEvent();
    }

    void InvokeEvent()
    {
        if (state) trueEvent?.Invoke();
        else falseEvent?.Invoke();
    }

    void LoadInvoke(bool state)
    {
        this.state = state;

        if (state)
        {
            if (onlyTrueLoad) loadTrueEvent?.Invoke();
            else trueEvent?.Invoke();
        }
        else
        {
            if (onlyFalseLoad) loadFalseEvent?.Invoke();
            else falseEvent?.Invoke();
        }

        /*if (separateOnLoad)
        {
            if (state) loadTrueEvent?.Invoke();
            else loadFalseEvent?.Invoke();
        }
        else InvokeEvent();*/
    }
}
