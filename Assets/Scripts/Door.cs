using UnityEngine;

public class Door : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        animator.Play("Door_Open");
    }
    public void OpenDoorInstant()
    {
        animator.Play("Door_Open", -1, 1f);
    }
    public void CloseDoor()
    {
        animator.Play("Door_Close");
    }
    public void CloseDoorInstant()
    {
        animator.Play("Door_Idle");
    }
}
