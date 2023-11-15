using System.Collections;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    Animator animator;

    bool open;

    bool animPlaying = false;

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
        if (animPlaying) return;

        ToggleState();
        recorder.RecordState(open);
    }

    void LoadState(bool newState)
    {
        open = newState;

        if (open) OpenDrawer();
        else CloseDrawer(true);
    }

    [ContextMenu("Toggle")]
    public void ToggleState()
    {
        open = !open;

        if (open) OpenDrawer();
        else CloseDrawer();
        
        StartCoroutine(AnimPlayingFlag());
    }

    void OpenDrawer()
    {
        animator.Play("Drawer_Open");
    }

    void CloseDrawer(bool instaClose = false)
    {
        if (instaClose) animator.Play("Drawer_Idle");
        else animator.Play("Drawer_Close");
    }

    WaitForSeconds openWait = new WaitForSeconds(0.5f);
    WaitForSeconds closeWait = new WaitForSeconds(0.25f);
    IEnumerator AnimPlayingFlag()
    {
        animPlaying = true;

        if (open) yield return openWait;
        else yield return closeWait;
        
        animPlaying = false;
    }
}
