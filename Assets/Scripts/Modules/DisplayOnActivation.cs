using UnityEngine;

public class DisplayOnActivation : ActionOnActivation
{
    [SerializeField] int ID;

    public override void Activated()
    {
        GameManager.Singleton.UIManager.DisplayItem(ID);
    }
}
