using UnityEngine.Splines;
using Unity.Mathematics;
using HietakissaUtils;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Cinematic")]
public class CinematicSO : ScriptableObject
{
    [SerializeField] float cinematicLength;
    [SerializeField] int id;
    public float Length => cinematicLength;
    public int ID => id;

    [HorizontalGroup(2)]
    [SerializeField] float startDelay;
    [SerializeField, HideInInspector] float endDelay;
    public float StartDelay => startDelay;
    public float EndDelay => endDelay;

    [SerializeField] bool cameraOnly;
    public bool CameraOnly => cameraOnly;

    SplineContainer movementSpline;

    public void SetSpline(SplineContainer spline)
    {
        movementSpline = spline;
    }

    public Vector3 SamplePosition(float point)
    {
        movementSpline.Evaluate(point, out float3 position, out float3 tangent, out float3 upVector);
        return position;
    }

    public Quaternion SampleRotation(float point, Vector3 origin)
    {
        movementSpline.Evaluate(point, out float3 position, out float3 tangent, out float3 upVector);
        return Quaternion.LookRotation(Maf.Direction(origin, origin + new Vector3(tangent.x, tangent.y, tangent.z)));
    }
}
