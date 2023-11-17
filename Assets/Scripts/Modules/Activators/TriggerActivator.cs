using UnityEngine;
using System;

public class TriggerActivator : MonoBehaviour, IActivatable
{
    public event Action OnActivate;

    public void Activate()
    {
        OnActivate?.Invoke();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == GameData.Player.Transform)
        {
            Activate();
        }
    }
}
