using System.Collections;
using UnityEngine;
using System;

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

        while (playerPoint < 1f || cameraPoint < 1f)
        {
            if (cinematic.ControlPlayer) playerTransform.position = cinematic.SamplePlayerPosition(playerPoint);

            if (cinematic.ControlCamera)
            {
                Vector3 targetLookAt = cinematic.SampleLookAtPos(cameraPoint);
                lookAt = Vector3.Lerp(lookAt, targetLookAt, 2f * Time.deltaTime);
                cameraTransform.LookAt(lookAt);
                cameraTransform.position = GameData.Player.CameraHolderTransform.position;

                cameraPoint += cameraSpeed * Time.deltaTime;
            }
            else cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, cinematic.SampleRotation(playerPoint, cameraTransform.position), 2f * Time.deltaTime);
            playerPoint += playerSpeed * Time.deltaTime;

            yield return null;
        }

        OnCinematicCompleted?.Invoke(cinematic.ID);

        yield return new WaitForSeconds(cinematic.EndDelay);

        GameData.Player.Camera.SetRotation(cameraRotation);
        GameData.Player.Camera.Enable();
        if (cinematic.ReturnToStart)
        {
            if (cinematic.StartOfCinematic) playerTransform.position = cinematicStartPos;
            else playerTransform.position = playerStartPos;
        }
        GameData.Player.Movement.Enable();
    }
}
