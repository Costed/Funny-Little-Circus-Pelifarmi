using UnityEngine;

public class ReferenceSetter : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform cameraHolderTransform;
    [SerializeField] MovementController movementController;
    [SerializeField] CameraController cameraController;

    void Awake()
    {
        GameData.Player.SetPlayerTransform(playerTransform);
        GameData.Player.SetCameraTransform(cameraTransform);
        GameData.Player.SetCameraHolderTransform(cameraHolderTransform);

        GameData.Player.SetMovementController(movementController);
        GameData.Player.SetCameraController(cameraController);
    }
}
