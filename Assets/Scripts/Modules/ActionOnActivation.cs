using UnityEngine;

public abstract class ActionOnActivation : MonoBehaviour
{
    IActivatable activatable;
    void OnEnable()
    {
        activatable = GetComponent<IActivatable>();
        if (activatable != null) activatable.OnActivate += Activated;
    }
    void OnDisable()
    {
        if (activatable != null) activatable.OnActivate -= Activated;
    }

    public abstract void Activated();
}
