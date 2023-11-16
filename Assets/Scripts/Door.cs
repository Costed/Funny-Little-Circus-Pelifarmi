using UnityEngine;
using System;

public class Door : MonoBehaviour
{
    Animator animator;

    IActivatable activatable;
    StateRecorder recorder;

    void Awake()
    {
        activatable = GetComponent<IActivatable>();
        activatable.OnActivate += Activated;

        animator = GetComponent<Animator>();

        recorder = new StateRecorder(transform);
        recorder.OnStateLoad += LoadState;
    }

    void Activated()
    {
        OpenDoor();
    }

    void LoadState(bool newState)
    {
        if (newState) OpenDoor();
        else CloseDoor();
    }

    public void OpenDoor()
    {
        animator.Play("Door_Open");
        recorder.RecordState(true);
    }

    public void CloseDoor()
    {
        animator.Play("Door_Idle");
        recorder.RecordState(false);
    }
}
