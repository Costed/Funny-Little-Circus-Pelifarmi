using UnityEngine;
using System;

public class ManualActivator : MonoBehaviour, IActivatable
{
    public event Action OnActivate;

    public void Activate()
    {
        OnActivate?.Invoke();
    }
}
