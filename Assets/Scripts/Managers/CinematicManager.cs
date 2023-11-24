using System.Collections;
using HietakissaUtils;
using UnityEngine;
using System;

public class CinematicManager : Manager
{
    [SerializeField] CinematicSO[] cinematics;
    [SerializeField] Transform lookAt;
    CinematicSO cinematic;

    public event Action<int> OnCinematicCompleted;

    Transform cameraTransform;
    Animator anim;


    void Awake() => anim = GetComponent<Animator>();


    public void PlayCinematic(int cinematicID)
    {
        cinematic = cinematics[cinematicID];
        StartCoroutine(RunCinematicCor());
    }

    IEnumerator RunCinematicCor()
    {
        GameData.Player.Movement.Disable();
        GameData.Player.Camera.Disable();


        const float warmupTime = 1.5f;
        float cinematicLength = cinematic.clip.length;

        cameraTransform = GameData.Player.CameraTransform;
        Quaternion startRot = cameraTransform.rotation;

        Vector3 startForward = cameraTransform.forward;
        Vector3 startPos = cameraTransform.position;


        anim.Play(cinematic.clip.name);

        yield return LerpForSecondsCor(cinematicLength);
        yield return LerpForSecondsCor(warmupTime, startPos + startForward);


        OnCinematicCompleted?.Invoke(cinematic.ID);


        GameData.Player.Camera.SetRotation(startRot);

        GameData.Player.Camera.Enable();
        GameData.Player.Movement.Enable();
    }

    IEnumerator LerpForSecondsCor(float seconds, Vector3 overridePos = new Vector3())
    {
        float cinematicTime = 0f;
        while (cinematicTime < seconds)
        {
            cinematicTime += Time.deltaTime;
            if (overridePos == Vector3.zero) LerpCameraRot(lookAt.position);
            else LerpCameraRot(overridePos);

            yield return null;
        }
    }

    void LerpCameraRot(Vector3 targetPos)
    {
        Vector3 targetLookAt = targetPos;
        Quaternion rotToLookAt = Quaternion.LookRotation(Maf.Direction(cameraTransform.position, targetLookAt));
        cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, rotToLookAt, 3f * Time.deltaTime);
    }
}
