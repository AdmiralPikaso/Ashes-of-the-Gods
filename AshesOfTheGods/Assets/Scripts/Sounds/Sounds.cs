using UnityEngine;

public class Sounds : MonoBehaviour
{
    public static void StaticSound(AudioClip clip, AudioSource audioSource, float volume)
    {
        ConfigureAudioSource(audioSource, volume, 1f);
        audioSource.PlayOneShot(clip);
    }

    public static void Sound(AudioClip clip, AudioSource audioSource, float volume, float minPitch, float maxPitch)
    {
        float pitch = Random.Range(minPitch, maxPitch);
        ConfigureAudioSource(audioSource, volume, pitch);
        audioSource.PlayOneShot(clip);
    }

    private static void ConfigureAudioSource(AudioSource audioSource, float volume, float pitch)
    {
        audioSource.volume = volume;
        audioSource.pitch = pitch;

        audioSource.spatialBlend = 1f;
        audioSource.minDistance = 1f;
        audioSource.maxDistance = 50f;
        audioSource.spread = 180f;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
    }
}
