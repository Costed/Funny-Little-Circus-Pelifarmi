using UnityEngine;

public class UIManager : Manager
{
    [SerializeField] Animator anim;

    void Awake()
    {
        ExitTransition();
    }


    public void EnterTransition()
    {
        anim.Play("EnterTransition");
    }

    public void ExitTransition()
    {
        anim.Play("ExitTransition");
    }
}
