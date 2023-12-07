using UnityEngine;

public class ProgressTaskOnActivation : ActionOnActivation
{
    public override void Activated()
    {
        GameManager.Singleton.ObjectiveManager.ProgressTask();
    }
}
