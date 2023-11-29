using UnityEngine;
using System;

public class InteractionActivator : MonoBehaviour, IInteractable
{
    public event Action OnActivate;

    public bool Interactable = true;
    [field:SerializeField] public float interactionTime {  get; private set; }

    [SerializeField] bool requireItem;
    [SerializeField, ConditionalField(nameof(requireItem))] bool consume;
    [SerializeField, ConditionalField(nameof(requireItem))] ItemSO[] items;

    [HorizontalGroup(2)]
    [SerializeField] bool doesntHaveItem;
    [SerializeField, ConditionalField(nameof(doesntHaveItem)), HideInInspector] ItemSO blacklistItem;

    [SerializeField] bool overrideInteract;
    [HideInInspector] public bool overrideValue;


    public void SetInteractable(bool interactable) => Interactable = interactable;

    public bool CanInteract()
    {
        if (!Interactable) return false;
        if (overrideInteract) return overrideValue;

        if (doesntHaveItem && GameManager.Singleton.ItemManager.HasItem(blacklistItem)) return false;

        if (requireItem)
        {
            bool hasAnyItem = false;

            foreach (ItemSO item in items)
            {
                if (GameManager.Singleton.ItemManager.HasItem(item))
                {
                    hasAnyItem = true;
                    break;
                }
            }

            if (hasAnyItem) return true;
            else return false;

            //if (GameManager.Singleton.ItemManager.HasItem(item)) return true;
            //else return false;
        }
        else return true;
    }

    public void Interact()
    {
        Activate();
    }

    public void Activate()
    {
        if (requireItem)
        {
            ItemSO firstItemInInventory = null;

            foreach (ItemSO item in items)
            {
                if (GameManager.Singleton.ItemManager.HasItem(item))
                {
                    firstItemInInventory = item;
                    break;
                }
            }

            if (consume) GameManager.Singleton.ItemManager.RemoveItem(firstItemInInventory);
        }
        
        OnActivate?.Invoke();

        //if (requireItem && consume) GameManager.Singleton.ItemManager.RemoveItem(item);
        //OnActivate?.Invoke();
    }
}
