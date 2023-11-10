using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    [SerializeField] CheckpointSO checkpointData;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == GameData.Player.Transform) GameManager.Singleton.SetCheckpoint(checkpointData);
    }
}
