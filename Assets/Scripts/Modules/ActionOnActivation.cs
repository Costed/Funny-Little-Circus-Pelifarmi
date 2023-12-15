using UnityEngine;

public abstract class ActionOnActivation : MonoBehaviour
{
    IActivatable[] activatables;
    void OnEnable()
    {
        activatables = GetComponents<IActivatable>();

        if (activatables != null )
        {
            foreach (IActivatable activatable in activatables) activatable.OnActivate += Activated;
        }

        //if (activatable != null) activatable.OnActivate += Activated;
    }
    void OnDisable()
    {
        if (activatables != null)
        {
            foreach (IActivatable activatable in activatables) activatable.OnActivate -= Activated;
        }

        //if (activatable != null) activatable.OnActivate -= Activated;
    }

    public abstract void Activated();
}
