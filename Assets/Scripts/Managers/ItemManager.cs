using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Manager
{
    List<int> tempInventory = new List<int>();
    List<int> savedInventory = new List<int>();

    public void AddItem(int itemID) => tempInventory.Add(itemID);
    public void RemoveItem(int itemID) => tempInventory.Remove(itemID);
    public bool HasItem(ItemSO item) => tempInventory.Contains(item.ID);

    public void SaveTempInventory()
    {
        savedInventory.Clear();

        foreach (int item in tempInventory) savedInventory.Add(item);

        //savedInventory = tempInventory;
    }

    public void LoadSavedInventory()
    {
        tempInventory.Clear();

        foreach (int item in savedInventory) tempInventory.Add(item);

        //tempInventory = savedInventory;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            string inventoryString = "Inventory: ";
            foreach (int item in tempInventory)
            {
                inventoryString += item.ToString() + ", ";
            }
            Debug.Log(inventoryString);
        }
    }
}
