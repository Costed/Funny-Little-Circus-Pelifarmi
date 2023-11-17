using UnityEngine.Splines;
using Unity.Mathematics;
using HietakissaUtils;
using UnityEngine;

public class DuckController : MonoBehaviour
{
    [SerializeField] SplineContainer path;

    [SerializeField] float force;
    [SerializeField] float lapTime;
    float point;

    Rigidbody rb;

    bool closeEnoughToPoint;

    [SerializeField] float torque;
    [SerializeField] float damping;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        point = UnityEngine.Random.Range(0f, 1f);
        rb.position = GetPointOnPath();

        lapTime += UnityEngine.Random.Range(-2f, 2f);
    }

    void Update()
    {
        closeEnoughToPoint = Vector3.Distance(transform.position, GetPointOnPath()) < 0.2f;

        if (!closeEnoughToPoint) return;

        point += Time.deltaTime * (1f / lapTime);
        if (point > 1f) point--;
    }

    void FixedUpdate()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        Vector3 point = GetPointOnPath();
        Vector3 direction = Maf.Direction(rb.position, point);

        float distanceMultiplier = Mathf.Max(1f, Vector3.Distance(rb.position, point));
        distanceMultiplier *= distanceMultiplier;

        Vector3 forceDir = direction * Time.deltaTime;
        forceDir.y *= 3f;
        rb.AddForce(forceDir * distanceMultiplier * force);
    }

    void Rotate()
    {
        float angle;
        Vector3 axis;
        Quaternion difference = Quaternion.FromToRotation(transform.up, Vector3.up);
        difference.ToAngleAxis(out angle, out axis);

        rb.AddTorque(-rb.angularVelocity * damping, ForceMode.Acceleration);
        rb.AddTorque(axis.normalized * angle * torque * Time.deltaTime, ForceMode.Acceleration);

        Vector3 direction = GetDirOnPath();
        difference = Quaternion.FromToRotation(-transform.forward, direction);
        difference.ToAngleAxis(out angle, out axis);

        rb.AddTorque(axis.normalized * angle * torque * Time.deltaTime, ForceMode.Acceleration);
    }

    Vector3 GetPointOnPath()
    {
        path.Evaluate(point, out float3 position, out float3 tangent, out float3 up);
        return position;
    }

    Vector3 GetDirOnPath()
    {
        path.Evaluate(point, out float3 position, out float3 tangent, out float3 up);
        return tangent;
    }
}
