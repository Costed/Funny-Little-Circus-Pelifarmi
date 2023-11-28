using UnityEngine;

public class SoundManager : Manager
{
    [SerializeField] float pitchDeviation = 0.1f;

    const int POOLSIZE = 10;
    Transform[] soundTransforms = new Transform[POOLSIZE];
    AudioSource[] soundSources = new AudioSource[POOLSIZE];

    int audioIndex;


    public override void Init()
    {
        for (int i = 0; i < soundTransforms.Length; i++)
        {
            soundTransforms[i] = new GameObject($"Sound Holder Object {i + 1}").transform;
            soundTransforms[i].parent = transform;

            soundSources[i] = soundTransforms[i].gameObject.AddComponent<AudioSource>();

            soundSources[i].playOnAwake = false;
            soundSources[i].spatialBlend = 1f;
        }
    }

    public void PlaySoundEffectAtPosition(Vector3 position, AudioClip soundEffect)
    {
        soundTransforms[audioIndex].position = position;
        AudioSource source = soundSources[audioIndex];
        source.clip = soundEffect;
        source.pitch = GetPitch();
        source.Play();

        audioIndex++;
        audioIndex %= POOLSIZE;
    }


    float GetPitch()
    {
        return 1 + Random.Range(-pitchDeviation * 0.5f, pitchDeviation * 0.5f);
    }
}
