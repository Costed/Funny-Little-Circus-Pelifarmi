using System.Collections.Generic;
using UnityEngine.Splines;
using Unity.Mathematics;
using HietakissaUtils;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Cinematic")]
public class CinematicSO : ScriptableObject
{
    [SerializeField] int id;
    public int ID => id;
    [SerializeField] float cameraTime;
    public float CameraTime => cameraTime;
    [SerializeField] float playerTime;
    public float PlayerTime => playerTime;

    [HorizontalGroup(2)]
    [SerializeField] float startDelay;
    public float StartDelay => startDelay;
    [SerializeField, HideInInspector] float endDelay;
    public float EndDelay => endDelay;

    [HorizontalGroup(2)]
    [SerializeField] bool controlPlayer;
    public bool ControlPlayer => controlPlayer;
    [SerializeField, HideInInspector] bool controlCamera;
    public bool ControlCamera => controlCamera;

    [HorizontalGroup(2)]
    [SerializeField] bool returnToStart;
    public bool ReturnToStart => returnToStart;
    [SerializeField, HideInInspector] bool startOfCinematic;
    public bool StartOfCinematic => startOfCinematic;

    SplineContainer spline;

    public void SetSpline(SplineContainer spline)
    {
        this.spline = spline;
    }

    public Vector3 SamplePlayerPosition(float point)
    {
        spline.Evaluate(0, point, out float3 position, out float3 tangent, out float3 upVector);
        return position;
    }

    public Vector3 SampleLookAtPos(float point)
    {
        spline.Evaluate(1, point, out float3 position, out float3 tangent, out float3 upVector);
        return position;
    }

    public Quaternion SampleRotation(float point, Vector3 origin)
    {
        spline.Evaluate(0, point, out float3 position, out float3 tangent, out float3 upVector);
        return Quaternion.LookRotation(Maf.Direction(origin, origin + new Vector3(tangent.x, tangent.y, tangent.z)));
    }
}
