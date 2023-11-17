using UnityEngine.Events;
using UnityEngine;

public class EventOnActivation : ActionOnActivation
{
    [SerializeField] UnityEvent eventToInvoke;

    public override void Activated()
    {
        eventToInvoke?.Invoke();
    }
}
