using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float sensitivity;

    [SerializeField] bool invertVertical;
    [SerializeField] bool invertHorizontal;

    [SerializeField] float maxLookAngle = 90f;

    Transform holder;

    float xRot, yRot;

    void OnValidate()
    {
        GameData.Controls.SetSensitivity(sensitivity);
    }

    void Awake()
    {
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;

        Cursor.lockState = CursorLockMode.Locked;

        yRot = transform.rotation.y;
    }

    void Start()
    {
        holder = GameData.Player.CameraHolderTransform;
    }

    void Update()
    {
        GetInput();
        Rotate();
    }

    void LateUpdate()
    {
        transform.position = holder.position;
        transform.rotation = holder.rotation;
    }

    void GetInput()
    {
        yRot += Input.GetAxisRaw("Mouse X") * (invertHorizontal ? -GameData.Controls.Sensitivity : GameData.Controls.Sensitivity);
        xRot += Input.GetAxisRaw("Mouse Y") * (invertVertical ? GameData.Controls.Sensitivity : -GameData.Controls.Sensitivity);

        xRot = Mathf.Clamp(xRot, -maxLookAngle, maxLookAngle);

        if (yRot > 360f) yRot -= 360f;
        else if (yRot < -360f) yRot += 360f;
    }

    void Rotate()
    {
        holder.rotation = Quaternion.Euler(xRot, yRot, 0f);
        GameData.Player.Transform.rotation = Quaternion.Euler(0f, yRot, 0f);
    }

    public void SetRotations(float x, float y)
    {
        xRot = x;
        yRot = y;
    }
}
