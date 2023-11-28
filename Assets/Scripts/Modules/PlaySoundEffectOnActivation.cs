using HietakissaUtils;
using UnityEngine;

public class PlaySoundEffectOnActivation : ActionOnActivation
{
    [SerializeField] AudioClip[] soundEffects;


    public override void Activated()
    {
        GameManager.Singleton.SoundManager.PlaySoundEffectAtPosition(transform.position, soundEffects.RandomElement());
    }
}
