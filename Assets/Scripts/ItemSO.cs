using UnityEngine;

[CreateAssetMenu(menuName = "Game/Item")]
public class ItemSO : ScriptableObject
{
    [field:SerializeField] public int ID {  get; set; }
}
