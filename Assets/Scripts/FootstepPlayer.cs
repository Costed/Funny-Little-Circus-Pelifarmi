using System.Collections.Generic;
using HietakissaUtils;
using UnityEngine;
using System;

public class FootstepPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] defaultClips;
    [SerializeField] FootstepType[] footstepTypes;

    Dictionary<string, AudioClip[]> stepsForSurface = new Dictionary<string, AudioClip[]>();

    [SerializeField] float distancePerStep;
    float distance;

    Vector3 oldPos;


    void Awake()
    {
        if (footstepTypes == null || footstepTypes.Length == 0) return;

        foreach (FootstepType step in footstepTypes)
        {
            stepsForSurface[step.surface] = step.footstepSounds;
        }
    }

    void Update()
    {
        Debug.Log(distance);

        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out RaycastHit hit, 0.3f))
        {
            distance += Vector3.Distance(transform.position, oldPos);

            AudioClip clip;
            if (hit.collider.TryGetComponent(out FootstepSurface surface))
            {
                clip = stepsForSurface[surface.surfaceType].RandomElement();
            }
            else clip = defaultClips.RandomElement();

            if (distance >= distancePerStep)
            {
                GameManager.Singleton.SoundManager.PlaySoundEffectAtPosition(hit.point, clip);
                distance -= distancePerStep;
            }
        }

        oldPos = transform.position;
    }
}

[Serializable]
class FootstepType
{
    public string surface;
    public AudioClip[] footstepSounds;
}