using System.Collections.Generic;
using UnityEngine;

public class FortuneTeller : MonoBehaviour
{
    [SerializeField] SimpleObjectAnimator[] repairPartAnimators;
    [SerializeField] SimpleObjectAnimator crystalBallAnimator;

    [SerializeField] ItemSO[] partItems;
    [SerializeField] ItemSO crystalBallItem;

    Animator anim;

    Dictionary<int, int> animations = new Dictionary<int, int>();


    void Awake()
    {
        anim = GetComponent<Animator>();
        animations[0] = Animator.StringToHash("Base Layer.SpitOutKey");
        //anim.Play(animations[0]);
    }


    public void InsertItem()
    {
        if (GameManager.Singleton.ItemManager.HasItem(partItems[0])) RepairPart(0);
        else if (GameManager.Singleton.ItemManager.HasItem(partItems[1])) RepairPart(1);
        else if (GameManager.Singleton.ItemManager.HasItem(partItems[2])) RepairPart(2);
        else if (GameManager.Singleton.ItemManager.HasItem(partItems[3])) RepairPart(3);
    }

    public void InsertCrystalBall()
    {
        RepairPart(-1);
    }

    void RepairPart(int index)
    {
        if (index == -1)
        {
            crystalBallAnimator.OnAnimationComplete = () => anim.Play(animations[0]);
            crystalBallAnimator.Play();

            GameManager.Singleton.ItemManager.RemoveItem(crystalBallItem);
        }
        else
        {
            repairPartAnimators[index].Play();
            GameManager.Singleton.ItemManager.RemoveItem(partItems[index]);
        }
    }
}
