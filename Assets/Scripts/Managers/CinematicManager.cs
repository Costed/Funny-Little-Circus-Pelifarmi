using System.Collections;
using UnityEngine;
using System;
using HietakissaUtils;

public class CinematicManager : Manager
{
    public CinematicSO[] Cinematics => cinematics;
    [SerializeField] CinematicSO[] cinematics;
    CinematicSO cinematic;

    public event Action<int> OnCinematicCompleted;

    public void PlayCinematic(int cinematicID)
    {
        cinematic = cinematics[cinematicID];
        StartCoroutine(RunCinematic());
    }

    IEnumerator RunCinematic()
    {
        yield return new WaitForSeconds(cinematic.StartDelay);

        GameData.Player.Movement.Disable();
        GameData.Player.Camera.Disable();

        float playerPoint = 0f;
        float cameraPoint = 0f;

        float playerSpeed = 1f / cinematic.PlayerTime;
        float cameraSpeed = 1f / cinematic.CameraTime;

        Transform cameraTransform = GameData.Player.CameraTransform;
        Transform playerTransform = GameData.Player.Transform;

        Quaternion cameraRotation = cameraTransform.rotation;

        Vector3 playerStartPos = playerTransform.position;
        Vector3 lookAt = cinematic.SampleLookAtPos(0f);
        Vector3 cinematicStartPos = cinematic.SamplePlayerPosition(0f);

        float startLerp = 0f;

        while (startLerp < 1f)
        {
            startLerp += Time.deltaTime;

            playerTransform.position = Vector3.Lerp(playerStartPos, cinematicStartPos, startLerp);
            cameraTransform.position = GameData.Player.CameraHolderTransform.position;

            LerpCameraRot();

            yield return null;
        }


        while (playerPoint < 1f || cameraPoint < 1f)
        {
            if (cinematic.ControlPlayer) playerTransform.position = cinematic.SamplePlayerPosition(playerPoint);

            if (cinematic.ControlCamera)
            {
                LerpCameraRot();

                cameraTransform.position = GameData.Player.CameraHolderTransform.position;
                cameraPoint += cameraSpeed * Time.deltaTime;
            }
            else cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, cinematic.SampleRotation(playerPoint, cameraTransform.position), 2f * Time.deltaTime);
            playerPoint += playerSpeed * Time.deltaTime;

            yield return null;
        }

        OnCinematicCompleted?.Invoke(cinematic.ID);

        yield return new WaitForSeconds(cinematic.EndDelay);

        
        if (cinematic.ReturnPlayerToStart)
        {
            if (cinematic.StartOfCinematic)
            {
                playerTransform.position = cinematicStartPos;
                cameraTransform.rotation = cinematic.SampleRotation(0f, cinematicStartPos);
            }
            else
            {
                playerTransform.position = playerStartPos;
                GameData.Player.Camera.SetRotation(cameraRotation);
            }
        }
        else GameData.Player.Camera.SetRotation(cameraTransform.rotation);

        GameData.Player.Camera.Enable();
        GameData.Player.Movement.Enable();


        void LerpCameraRot()
        {
            Vector3 targetLookAt = cinematic.SampleLookAtPos(cameraPoint);
            Quaternion rotToLookAt = Quaternion.LookRotation(Maf.Direction(cameraTransform.position, targetLookAt));
            cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, rotToLookAt, 2f * Time.deltaTime);
        }
    }
}
