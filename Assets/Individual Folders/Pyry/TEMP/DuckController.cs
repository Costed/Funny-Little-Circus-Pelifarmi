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

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        point = UnityEngine.Random.Range(0f, 1f);
        rb.position = GetPointOnPath();

        lapTime += UnityEngine.Random.Range(-2f, 2f);
    }

    void Update()
    {
        point += Time.deltaTime * (1f / lapTime);
        if (point > 1f) point--;
    }

    void FixedUpdate()
    {
        Vector3 point = GetPointOnPath();
        Vector3 direction = Maf.Direction(rb.position, point);

        float distanceMultiplier = Mathf.Max(1f, Vector3.Distance(rb.position, point));
        distanceMultiplier *= distanceMultiplier;

        Vector3 forceDir = direction * Time.deltaTime;
        rb.AddForce(forceDir * distanceMultiplier * force);
    }

    Vector3 GetPointOnPath()
    {
        path.Evaluate(point, out float3 position, out float3 tangent, out float3 up);
        return position;
    }
}
