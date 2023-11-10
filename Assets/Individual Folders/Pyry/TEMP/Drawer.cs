using System.Collections;
using UnityEngine;

public class Drawer : MonoBehaviour, IInteractable
{
    Animator animator;

    bool open;

    bool animPlaying = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    [ContextMenu("Toggle")]
    public void ToggleState()
    {
        if (animator == null) animator = GetComponent<Animator>();

        open = !open;

        if (open) animator.Play("Drawer_Open");
        else animator.Play("Drawer_Close");
        
        StartCoroutine(AnimPlayingFlag());

        //if (open) animator.CrossFade("Drawer_Open", 0.6f);
        //else animator.CrossFade("Drawer_Close", 0.6f);
    }

    WaitForSeconds wait = new WaitForSeconds(1);
    IEnumerator AnimPlayingFlag()
    {
        animPlaying = true;
        yield return wait;
        animPlaying = false;
    }

    public void Interact()
    {
        if (!animPlaying) ToggleState();
    }

    void LoadCheckpoint()
    {
        open = false;
        animator.Play("Drawer_Idle");
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
