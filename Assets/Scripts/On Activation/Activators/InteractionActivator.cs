using UnityEngine;
using System;

public class InteractionActivator : MonoBehaviour, IInteractable
{
    public event Action OnActivate;

    [HorizontalGroup(3)]
    [SerializeField] bool requireItem;
    [SerializeField, ConditionalField(nameof(requireItem)), HideInInspector] bool consume;
    [SerializeField, ConditionalField(nameof(requireItem)), HideInInspector] ItemSO item;

    public bool CanInteract()
    {
        if (requireItem)
        {
            if (GameManager.Singleton.ItemManager.HasItem(item)) return true;
            else return false;
        }
        else return true;
    }

    public void Interact()
    {
        Activate();
    }

    public void Activate()
    {
        if (requireItem && consume) GameManager.Singleton.ItemManager.RemoveItem(item.ID);
        OnActivate?.Invoke();
    }
}
