using UnityEngine;

public class SetCheckpointOnActivation : ActionOnActivation
{
    [SerializeField] CheckpointSO checkpoint;

    public override void Activated()
    {
        GameManager.Singleton.SetCheckpoint(checkpoint);
    }
}
