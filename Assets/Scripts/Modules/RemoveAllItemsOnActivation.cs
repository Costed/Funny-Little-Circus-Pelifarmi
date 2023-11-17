using UnityEngine;

public class RemoveAllItemsOnActivation : ActionOnActivation
{
    [HorizontalGroup(2)]
    [SerializeField] bool ofType;
    [SerializeField, HideInInspector, ConditionalField(nameof(ofType))] ItemSO item;

    public override void Activated()
    {
        if (ofType) GameManager.Singleton.ItemManager.RemoveAllItemsOfType(item);
        else GameManager.Singleton.ItemManager.RemoveAllItems();
    }
}
