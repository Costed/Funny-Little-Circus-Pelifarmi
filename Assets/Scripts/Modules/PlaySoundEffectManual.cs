using HietakissaUtils;
using UnityEngine;
using UnityEngine.Audio;

public class PlaySoundEffectManual : MonoBehaviour
{
    [SerializeField] AudioMixerGroup mixerGroup;
    [SerializeField] AudioClip[] soundEffects;
    [SerializeField, Range(0f, 1f)] float blend2D3D = 1f;
    [SerializeField, Range(0f, 1f)] float volume = 1f;


    public void PlaySoundRandomEffect()
    {
        GameManager.Singleton.SoundManager.PlaySoundEffectAtPosition(transform.position, soundEffects.RandomElement(), volume, blend2D3D, mixerGroup);
    }
}
