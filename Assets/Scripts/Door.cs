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
        recorder.RecordState(true);

        OpenDoor();
    }

    void LoadState(bool newState)
    {
        if (newState) OpenDoor();
        else CloseDoor();
    }

    void OpenDoor()
    {
        animator.Play("Door_Open");
    }

    void CloseDoor()
    {
        animator.Play("Door_Idle");
    }
}
