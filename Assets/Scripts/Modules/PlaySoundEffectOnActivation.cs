using UnityEngine.Audio;
using HietakissaUtils;
using UnityEngine;

public class PlaySoundEffectOnActivation : ActionOnActivation
{
    [SerializeField] AudioMixerGroup mixerGroup;
    [SerializeField] AudioClip[] soundEffects;
    [SerializeField, Range(0f, 1f)] float blend2D3D = 1f;
    [SerializeField, Range(0f, 1f)] float volume = 1f;


    public override void Activated()
    {
        GameManager.Singleton.SoundManager.PlaySoundEffectAtPosition(transform.position, soundEffects.RandomElement(), volume, blend2D3D, mixerGroup);
    }
}
