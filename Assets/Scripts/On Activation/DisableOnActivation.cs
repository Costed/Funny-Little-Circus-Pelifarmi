using UnityEngine;

public class DisableOnActivation : ActionOnActivation
{
    public override void Activated()
    {
        gameObject.SetActive(false);
    }
}
