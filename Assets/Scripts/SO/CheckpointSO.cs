using UnityEngine;

[CreateAssetMenu(menuName = "Game/Checkpoint")]
public class CheckpointSO : ScriptableObject
{
    [SerializeField] Vector3 position;
    [SerializeField] Vector3 rotation;

    public Vector3 GetPlayerSpawnPos() => position;
    public Vector3 GetPlayerRotation() => rotation;
}
