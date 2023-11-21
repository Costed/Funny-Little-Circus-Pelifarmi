using Random = UnityEngine.Random;
using UnityEngine.Splines;
using Unity.Mathematics;
using HietakissaUtils;
using UnityEngine;

public class DuckController : MonoBehaviour
{
    [SerializeField] SplineContainer path;

    [SerializeField] float force = 50f;
    [SerializeField] float baseLaptime = 7f;
    [SerializeField] float maxLaptimeOffset;
    float currentLaptimeOffset;
    float targetOffset;

    float laptime;
    float point;
    
    Rigidbody rb;

    bool closeEnoughToPoint;

    [SerializeField] float torque = 13f;
    [SerializeField] float damping = 1.5f;


    public void SetSpline(SplineContainer path) => this.path = path;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        point = Random.Range(0f, 1f);
        rb.position = GetPointOnPath();

        baseLaptime += Random.Range(-1f, 1f);
    }

    void Update()
    {
        //if ((currentLaptimeOffset - targetOffset).Abs() < 0.1f) targetOffset = Random.Range(-maxLaptimeOffset, maxLaptimeOffset);
        //
        //currentLaptimeOffset = Mathf.Lerp(currentLaptimeOffset, targetOffset, Time.deltaTime);
        laptime = baseLaptime/* + currentLaptimeOffset*/;


        closeEnoughToPoint = Vector3.Distance(transform.position, GetPointOnPath()) < 0.2f;
        if (!closeEnoughToPoint) return;

        point += Time.deltaTime * (1f / laptime);
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
        
        Vector3 forceDir = direction;
        forceDir.y *= 6f;
        rb.AddForce(forceDir * distanceMultiplier * force * Time.deltaTime);
        //rb.AddForce(-transform.forward.SetY(0f) * force * Time.deltaTime);
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
        direction = Vector3.Lerp(direction, -transform.forward, 0.5f);
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
