using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] KeyType requiredKey;

    Animator animator;

    bool doorOpen;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (!doorOpen && GameManager.Singleton.KeyManager.HasKeyOfType(requiredKey)) OpenDoor();
    }

    void OpenDoor()
    {
        animator.Play("Door_Open");

        doorOpen = true;
        //animator.SetBool("DoorOpen", doorOpen);
    }

    void CloseDoor()
    {
        animator.Play("Door_Idle");
        
        doorOpen = false;
        //animator.SetBool("DoorOpen", doorOpen);
    }

    /*void KeyCollected(KeyType type)
    {
        if (type == requiredKey) animator.Play("Door_Open");
    }*/

    void LoadCheckpoint()
    {
        if (!GameManager.Singleton.KeyManager.HasKeyOfType(requiredKey)) CloseDoor();
    }

    void OnEnable()
    {
        GameManager.Singleton.OnLoadCheckpoint += LoadCheckpoint;
    }

    void OnDisable()
    {
        GameManager.Singleton.OnLoadCheckpoint -= LoadCheckpoint;
    }
}
