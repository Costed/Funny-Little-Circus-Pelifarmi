using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float sensitivity;

    [SerializeField] bool invertVertical;
    [SerializeField] bool invertHorizontal;

    [SerializeField] float maxLookAngle = 90f;

    Transform holder;

    float xRot, yRot;

    new bool enabled = true;

    void OnValidate()
    {
        GameData.Controls.SetSensitivity(sensitivity);
    }

    void Awake()
    {
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        holder = GameData.Player.CameraHolderTransform;

        SetRotation(transform.rotation);
    }

    void Update()
    {
        if (!enabled) return;

        GetInput();
        Rotate();
    }

    void LateUpdate()
    {
        if (!enabled) return;

        transform.position = holder.position;
        transform.rotation = holder.rotation;
    }

    void GetInput()
    {
        yRot += Input.GetAxisRaw("Mouse X") * (invertHorizontal ? -GameData.Controls.Sensitivity : GameData.Controls.Sensitivity);
        xRot += Input.GetAxisRaw("Mouse Y") * (invertVertical ? GameData.Controls.Sensitivity : -GameData.Controls.Sensitivity);

        ClampRotations();
    }

    void Rotate()
    {
        GameData.Player.Transform.rotation = Quaternion.Euler(0f, yRot, 0f);
        holder.rotation = Quaternion.Euler(xRot, yRot, 0f);
    }

    public void SetRotations(float x, float y)
    {
        xRot = x;
        yRot = y;

        ClampRotations();
        Rotate();

        transform.rotation = holder.rotation;
    }

    public void SetRotation(Quaternion rot)
    {
        Vector3 euler = rot.eulerAngles;

        xRot = euler.x;
        yRot = euler.y;

        Rotate();
        transform.rotation = holder.rotation;
    }

    void ClampRotations()
    {
        xRot = Mathf.Clamp(xRot, -maxLookAngle, maxLookAngle);

        if (yRot > 360f) yRot -= 360f;
        else if (yRot < -360f) yRot += 360f;
    }

    public void Enable()
    {
        enabled = true;
    }

    public void Disable()
    {
        enabled = false;
    }
}
