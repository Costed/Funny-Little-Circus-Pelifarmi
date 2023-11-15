using UnityEngine;
using System;

public class Key : MonoBehaviour
{
    IActivatable activatable;

    StateRecorder recorder;

    void Awake()
    {
        activatable = GetComponent<IActivatable>();
        activatable.OnActivate += Activated;

        recorder = new StateRecorder(transform, true);
        recorder.OnStateLoad += LoadState;
    }

    void Activated()
    {
        recorder.RecordState(false);
    }

    void LoadState(bool state)
    {
        gameObject.SetActive(state);
    }
}
