using UnityEngine;

[SelectionBase]
public class MovementController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float runMultiplier;

    [Header("Physics")]
    [SerializeField] float gravity;
    [SerializeField] float fallMultiplier;
    [SerializeField] float drag;

    CharacterController cc;

    float horizontal, vertical;

    Vector3 moveDir;
    Vector3 velocity;

    const float Gravity = 9.81f;

    [SerializeField] float sphereCastRadius;
    [SerializeField] float sphereCastOffset = 0.05f;
    [SerializeField] float rayCastOffset = 0.05f;

    RaycastHit sphereCast;
    RaycastHit groundRay;

    bool isGrounded;
    bool lastGrounded;

    void Awake()
    {
        cc = GetComponent<CharacterController>();

        GameData.Controls.SetSensitivity(1f);
    }

    void Update()
    {
        if (!cc.enabled) return;

        //Grounded check
        isGrounded = Physics.SphereCast(transform.position + Vector3.up * (sphereCastRadius + sphereCastOffset), sphereCastRadius, Vector3.down, out sphereCast, sphereCastOffset * 2);
        Physics.Raycast(transform.position + Vector3.up * rayCastOffset, Vector3.down, out groundRay, rayCastOffset * 2);

        if (isGrounded && !lastGrounded) StartGround();
        else if (!isGrounded && lastGrounded) StartAir();

        HandlePhysics();
        GetInput();
        HandleMovement();

        lastGrounded = isGrounded;

        void HandlePhysics()
        {
            HandleGravity();
            HandleDrag();

            void HandleDrag()
            {
                //terrible implementation, technically works, will leave it at that for now
                //the implementation isn't the greatest, so it slows down at an odd rate, should probably multiply it with 
                //Mathf.Max(1f, velocity.Magnitude()) or something similar
                Vector3 tempVelocity = velocity;
                tempVelocity.y = 0f;
                if (tempVelocity.magnitude < 0.1f && velocity.y == 0f) velocity = Vector3.zero;
                else velocity -= tempVelocity.normalized * drag * Time.deltaTime;
            }

            void HandleGravity()
            {
                velocity += Vector3.down * CalculateGravityMagnitude();
                if (isGrounded) velocity.y = Mathf.Max(-5f, velocity.y);
            }
        }

        void GetInput()
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }

        void HandleMovement()
        {
            MoveDir();
            cc.Move((moveDir + velocity) * Time.deltaTime);

            void MoveDir()
            {
                //TODO somehow orient vector away from the slope
                //joo ruma ja pitk� rivi koodia, en jaksa teh� hienompaa rn
                if (OnSteepGround()) moveDir = Vector3.ProjectOnPlane(Vector3.down, groundRay.normal);
                else moveDir = (GameData.Player.Transform.forward * vertical + GameData.Player.Transform.right * horizontal).normalized; 
                moveDir *= speed * GetSpeedMultiplier();



                float GetSpeedMultiplier()
                {
                    if (Input.GetKey(KeyCode.LeftShift)) return runMultiplier;
                    else return 1;
                }
            }
        }

        void StartAir()
        {
            if (lastGrounded && !isGrounded && velocity.y < 0) velocity.y = 0f;
        }

        void StartGround()
        {

        }
    }

    bool OnSteepGround()
    {
        return GetGroundAngle() > cc.slopeLimit;
    }

    float GetGroundAngle()
    {
        float raycastAngle = Vector3.Angle(groundRay.normal, Vector3.up);
        float spherecastAngle = Vector3.Angle(sphereCast.normal, Vector3.up);
        if (raycastAngle < spherecastAngle) return raycastAngle;
        else return spherecastAngle;
    }

    float CalculateGravityMagnitude() => Gravity * gravity * (velocity.y < 0 ? fallMultiplier : 1) * Time.deltaTime;

    public void AddKnockback(Vector3 knockbackForce)
    {
        velocity += knockbackForce;
    }

    public void Teleport(Vector3 position)
    {
        cc.enabled = false;
        transform.position = position;
        cc.enabled = true;
    }


    public void Enable()
    {
        cc.enabled = true;
    }

    public void Disable()
    {
        cc.enabled = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawRay(transform.position + Vector3.up * 0, moveDir);

        if (Physics.SphereCast(transform.position + Vector3.up * (sphereCastRadius + sphereCastOffset), sphereCastRadius, Vector3.down, out sphereCast, sphereCastOffset * 2))
        {
            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(new Vector3(transform.position.x, sphereCast.point.y, transform.position.z) + Vector3.up * sphereCastRadius, sphereCastRadius);
        }
        else
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position + Vector3.down * (sphereCastOffset - sphereCastRadius), sphereCastRadius);
        }
    }
}
