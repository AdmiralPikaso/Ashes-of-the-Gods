using UnityEngine;

public class Sounds : MonoBehaviour
{
    public static void Sound(AudioClip clip, AudioSource audioSource, float volume)
    {
        audioSource.volume = volume;
        audioSource.pitch = 1f;
        audioSource.PlayOneShot(clip);
    }
    public static void Sound(AudioClip clip, AudioSource audioSource, float volume, float minPitch, float maxPitch)
    {
        audioSource.volume = volume;
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.PlayOneShot(clip);
    }
}
