using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton;

    public KeyManager KeyManager;

    CheckpointSO currentCheckpoint;

    public event Action OnLoadCheckpoint;

    void Awake()
    {
        Singleton = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadCheckpoint();
        }
    }

    public void SetCheckpoint(CheckpointSO checkpoint)
    {
        Debug.Log("Got checkpoint");

        currentCheckpoint = checkpoint;
        KeyManager.CommitKeyPickup();
    }

    public void LoadCheckpoint()
    {
        if (currentCheckpoint != null)
        {
            GameData.Player.Movement.Teleport(currentCheckpoint.GetPlayerSpawnPos());

            Vector3 rot = currentCheckpoint.GetPlayerRotation();
            GameData.Player.Camera.SetRotations(rot.x, rot.y);
        }

        KeyManager.ResetKeyToCheckpoint();
        OnLoadCheckpoint?.Invoke();
    }
}
