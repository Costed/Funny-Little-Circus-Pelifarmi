using UnityEngine;
using System;

public static class GameData
{
    public static event Action<bool> OnPauseStateChanged;
    public static bool Paused;
    public static bool DisplayingItem;

    public static PlayerData Player = new PlayerData();
    public static ControlsData Controls = new ControlsData();
    public static SettingsData Settings = new SettingsData();

    public static void ChangePauseState(bool state)
    {
        Paused = state;
        OnPauseStateChanged?.Invoke(Paused);
    }
}

public class PlayerData
{
    public Transform Transform { get; private set; }
    public Transform CameraTransform { get; private set; }
    public Transform CameraHolderTransform { get; private set; }

    public MovementController Movement { get; private set; }
    public CameraController Camera { get; private set; }

    public void SetPlayerTransform(Transform playerTransform) => Transform = playerTransform;
    public void SetCameraTransform(Transform cameraTransform) => CameraTransform = cameraTransform;
    public void SetCameraHolderTransform(Transform cameraHolderTransform) => CameraHolderTransform = cameraHolderTransform;

    public void SetMovementController(MovementController controller) => Movement = controller;
    public void SetCameraController(CameraController controller) => Camera = controller;
}

public class SettingsData
{
    public event Action OnSettingsChanged;

    public int Volume { get; private set; }

    public void SetVolume(int volume)
    {
        Volume = volume;
        OnSettingsChanged?.Invoke();
    }
}

public class ControlsData
{
    public float Sensitivity { get; private set; }

    public void SetSensitivity(float sensitivity) => Sensitivity = sensitivity;
}
