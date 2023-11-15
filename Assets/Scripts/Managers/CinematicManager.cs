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

        float point = 0f;
        float speed = 1f / cinematic.Length;

        Transform cameraTransform = GameData.Player.CameraTransform;
        Transform playerTransform = GameData.Player.Transform;

        Vector3 playerStartPos = playerTransform.position;

        while (point < 1f)
        {
            playerTransform.position = cinematic.SamplePosition(point);
            cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, cinematic.SampleRotation(point, cameraTransform.position), 2f * Time.deltaTime);
            cameraTransform.position = GameData.Player.CameraHolderTransform.position;

            point += Time.deltaTime * speed;
            yield return null;
        }

        yield return new WaitForSeconds(cinematic.EndDelay);

        Quaternion cameraRotation = cameraTransform.rotation;

        GameData.Player.Camera.SetRotation(cameraRotation);
        GameData.Player.Camera.Enable();
        if (cinematic.CameraOnly) playerTransform.position = playerStartPos;
        GameData.Player.Movement.Enable();

        OnCinematicCompleted?.Invoke(cinematic.ID);
    }
}
