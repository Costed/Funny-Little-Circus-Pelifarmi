using UnityEngine;

public class AddItemOnActivation : ActionOnActivation
{
    [SerializeField] ItemSO item;

    public override void Activated()
    {
        GameManager.Singleton.ItemManager.AddItem(item);
    }
}
