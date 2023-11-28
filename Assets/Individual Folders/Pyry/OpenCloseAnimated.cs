using UnityEngine;

public class OpenCloseAnimated : MonoBehaviour
{
    Animator anim;

    [SerializeField] string animName;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    public void Open()
    {
        anim.CrossFade($"{animName}Open", 0.2f);
    }

    public void Close()
    {
        anim.CrossFade($"{animName}Close", 0.2f);
    }
}
