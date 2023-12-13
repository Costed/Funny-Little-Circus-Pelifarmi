using System.Collections.Generic;
using HietakissaUtils.Commands;
using UnityEditor;
using UnityEngine;
using System;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class GameManager : MonoBehaviour
{
    public static GameManager Singleton;

    [SerializeField] Manager[] managers;
    public ItemManager ItemManager => (ItemManager)managers[0];
    public CinematicManager CinematicManager => (CinematicManager)managers[1];
    public SoundManager SoundManager => (SoundManager)managers[2];
    public SceneManager SceneManager => (SceneManager)managers[3];
    public UIManager UIManager => (UIManager)managers[4];
    public ObjectiveManager ObjectiveManager => (ObjectiveManager)managers[5];

    CheckpointSO currentCheckpoint;

    public event Action OnLoadCheckpoint;

    public Dictionary<Transform, bool> tempStates = new Dictionary<Transform, bool>();
    public Dictionary<Transform, bool> savedStates = new Dictionary<Transform, bool>();

    void Awake()
    {
        Singleton = this;

        CommandSystem.AddCommand(new DebugCommand<int>("AddItem", (itemID) => GameManager.Singleton.ItemManager.AddItem(itemID)));
        CommandSystem.AddCommand(new DebugCommand<bool>("Pause", (state) => {
            if (state) Pause();
            else UnPause();
        }));

        foreach (Manager manager in managers) manager.Init();
        foreach (Manager manager in managers) manager.LateInit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) LoadCheckpoint();
    }

    public bool SetCheckpoint(CheckpointSO checkpoint)
    {
        if (currentCheckpoint != null && currentCheckpoint == checkpoint) return false;

        currentCheckpoint = checkpoint;

        foreach (KeyValuePair<Transform, bool> kvp in tempStates)
        {
            savedStates[kvp.Key] = kvp.Value;
        }

        ItemManager.SaveTempInventory();

        return true;
    }

    public void LoadCheckpoint()
    {
        if (currentCheckpoint != null)
        {
            GameData.Player.Movement.Teleport(currentCheckpoint.GetPlayerSpawnPos());

            Vector3 rot = currentCheckpoint.GetPlayerRotation();
            GameData.Player.Camera.SetRotations(rot.x, rot.y);

            foreach (KeyValuePair<Transform, bool> kvp in savedStates)
            {
                tempStates[kvp.Key] = kvp.Value;
            }

            ItemManager.LoadSavedInventory();

            OnLoadCheckpoint?.Invoke();
        }
    }

    public void Pause()
    {
        Time.timeScale = float.Epsilon;
        GameData.ChangePauseState(true);
    }

    public void UnPause()
    {
        Time.timeScale = 1f;
        GameData.ChangePauseState(false);
    }

#if UNITY_EDITOR
    static GameManager()
    {
        EditorApplication.playModeStateChanged += (context) =>
        {
            if (context == PlayModeStateChange.EnteredEditMode)
            {
                GameData.ChangePauseState(false);
            }
        };
    }
#endif
}

public abstract class Manager : MonoBehaviour
{
    public virtual void Init()
    {

    }
    public virtual void LateInit()
    {

    }
}
