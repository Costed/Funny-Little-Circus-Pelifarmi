using UnityEngine.Splines;
using Unity.Mathematics;
using HietakissaUtils;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Cinematic")]
public class CinematicSO : ScriptableObject
{
    [field:SerializeField] public int ID { get; private set; }
    [field:SerializeField] public AnimationClip clip { get; private set; }
}
