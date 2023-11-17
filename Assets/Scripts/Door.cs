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

    public void CloseDoor()
    {
        animator.Play("Door_Idle");
    }
}
