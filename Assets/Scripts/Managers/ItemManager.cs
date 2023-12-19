using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemManager : Manager
{
    public event Action<int> OnItemAdded;
    public event Action<int> OnItemRemoved;

    List<int> tempInventory = new List<int>();
    List<int> savedInventory = new List<int>();

    public ItemSO lastRemovedItem { get; private set; }

    public void AddItem(ItemSO item)
    {
        tempInventory.Add(item.ID);
        OnItemAdded?.Invoke(item.ID);
    }
    public void AddItem(int itemID)
    {
        tempInventory.Add(itemID);
        OnItemAdded?.Invoke(itemID);
    }

    public void RemoveItem(ItemSO item)
    {
        tempInventory.Remove(item.ID);
        OnItemRemoved?.Invoke(item.ID);

        lastRemovedItem = item;
    }

    public void RemoveAllItems()
    {
        foreach (int itemID in tempInventory) OnItemRemoved?.Invoke(itemID);

        tempInventory.Clear();
    }

    public void RemoveAllItemsOfType(ItemSO item)
    {
        while (tempInventory.Contains(item.ID)) tempInventory.Remove(item.ID);
        OnItemRemoved?.Invoke(item.ID);

        lastRemovedItem = item;
    }

    public bool HasItem(ItemSO item) => tempInventory.Contains(item.ID);

    public void SaveTempInventory()
    {
        savedInventory.Clear();
        foreach (int item in tempInventory) savedInventory.Add(item);
    }
    public void LoadSavedInventory()
    {
        tempInventory.Clear();
        foreach (int item in savedInventory) tempInventory.Add(item);
    }


    //TEMP DEBUG
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.I))
    //    {
    //        string inventoryString = "Inventory: ";
    //        foreach (int item in tempInventory)
    //        {
    //            inventoryString += item.ToString() + ", ";
    //        }
    //        Debug.Log(inventoryString);
    //    }
    //}
}
