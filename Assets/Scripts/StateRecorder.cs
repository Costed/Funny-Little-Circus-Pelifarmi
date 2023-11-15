using UnityEngine;
using System;

public class StateRecorder
{
    public event Action<bool> OnStateLoad;

    Transform transform;
    bool stateValue;

    public StateRecorder(Transform transform, bool defaultValue = false)
    {
        GameManager.Singleton.OnLoadCheckpoint += LoadState;
        this.transform = transform;

        RecordState(defaultValue);
    }

    public void RecordState(bool newState)
    {
        stateValue = newState;
        GameManager.Singleton.tempStates[transform] = stateValue;
    }
    public void LoadState()
    {
        if (GameManager.Singleton.tempStates.TryGetValue(transform, out bool loadValue))
        {
            stateValue = loadValue;
            OnStateLoad(stateValue);
        }
    }
}
