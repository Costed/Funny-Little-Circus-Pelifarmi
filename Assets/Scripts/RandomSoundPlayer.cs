using HietakissaUtils;
using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    [SerializeField] AudioclipWithVolume[] sounds;

    [SerializeField] float minDelay = 2f;
    [SerializeField] float maxDelay = 7f;

    float delay;
    float seconds;


    void Awake()
    {
        delay = Random.Range(minDelay, maxDelay);
    }

    void Update()
    {
        seconds += Time.deltaTime;

        if (seconds >= delay)
        {
            seconds -= delay;
            PlaySound();

            delay = Random.Range(minDelay, maxDelay);
        }
    }

    void PlaySound()
    {
        Debug.Log($"Playing random sound, {delay.RoundToDecimalPlaces(3)}");
        AudioclipWithVolume sound = sounds.RandomElement();
        GameManager.Singleton.SoundManager.PlaySoundEffectAtPosition(transform.position, sound.clip, sound.volume);
    }
}

[System.Serializable]
class AudioclipWithVolume
{
    [field:SerializeField] public AudioClip clip { get; private set; }
    [field:SerializeField] public float volume { get; private set; }
}
