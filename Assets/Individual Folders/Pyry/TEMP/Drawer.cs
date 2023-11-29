using System.Collections;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    Animator animator;

    bool open;

    //bool animPlaying = false;

    InteractionActivator activatable;
    //StateRecorder recorder;

    void Awake()
    {
        activatable = GetComponent<InteractionActivator>();
        activatable.overrideValue = true;
        //activatable.OnActivate += Activated;

        animator = GetComponent<Animator>();

        //recorder = new StateRecorder(transform);
        //recorder.OnStateLoad += LoadState;
    }

    void Activated()
    {
        //if (animPlaying) return;

        ToggleState();
        //recorder.RecordState(open);
    }

    void LoadState(bool newState)
    {
        open = newState;

        if (open) OpenDrawer();
        //else CloseDrawer(true);
    }

    [ContextMenu("Toggle")]
    public void ToggleState()
    {
        open = !open;

        if (open) OpenDrawer();
        else CloseDrawer();
        
        StartCoroutine(AnimPlayingFlag());
    }

    public void OpenDrawer()
    {
        animator.Play("Drawer_Open");
        StartCoroutine(AnimPlayingFlag());
        //open = true;
    }

    public void CloseDrawer()
    {
        animator.Play("Drawer_Close");
        StartCoroutine(AnimPlayingFlag());
        //open = false;
    }

    public void InstaCloseDrawer()
    {
        animator.Play("Drawer_Idle");
    }

    WaitForSeconds openWait = new WaitForSeconds(0.5f);
    WaitForSeconds closeWait = new WaitForSeconds(0.25f);
    IEnumerator AnimPlayingFlag()
    {
        //animPlaying = true;
        activatable.overrideValue = false;

        if (open) yield return openWait;
        else yield return closeWait;

        activatable.overrideValue = true;

        //animPlaying = false;
    }
}
