using UnityEngine;
using System;

public class CheckpointActivator : MonoBehaviour, IActivatable
{
    [SerializeField] CheckpointSO checkpointData;

    public event Action OnActivate;

    public void Activate()
    {
        OnActivate?.Invoke();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == GameData.Player.Transform)
        {
            if (GameManager.Singleton.SetCheckpoint(checkpointData)) Activate();
        }
    }
}
