using UnityEngine;

public class FortuneTeller : MonoBehaviour
{
    [SerializeField] GameObject[] repairParts;
    [SerializeField] GameObject crystalBall;

    [SerializeField] ItemSO[] partItems;
    [SerializeField] ItemSO crystalBallItem;

    [SerializeField] ItemSO stageKey;
    int fixedParts;

    public void InsertItem()
    {
        if (GameManager.Singleton.ItemManager.HasItem(partItems[0])) RepairPart(0);
        else if (GameManager.Singleton.ItemManager.HasItem(partItems[1])) RepairPart(1);
        else if (GameManager.Singleton.ItemManager.HasItem(partItems[2])) RepairPart(2);
        else if (GameManager.Singleton.ItemManager.HasItem(partItems[3])) RepairPart(3);
        else if (GameManager.Singleton.ItemManager.HasItem(crystalBallItem)) RepairPart(-1);
    }

    public void InsertCrystalBall()
    {
        if (GameManager.Singleton.ItemManager.HasItem(crystalBallItem)) RepairPart(-1);
    }

    void RepairPart(int index)
    {
        if (index == -1)
        {
            crystalBall.SetActive(true);
            GameManager.Singleton.ItemManager.RemoveItem(crystalBallItem);
        }
        else
        {
            repairParts[index].SetActive(true);
            GameManager.Singleton.ItemManager.RemoveItem(partItems[index]);
        }
        fixedParts++;

        if (fixedParts == 5) GameManager.Singleton.ItemManager.AddItem(stageKey);
    }
}
