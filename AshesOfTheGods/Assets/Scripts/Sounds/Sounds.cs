using UnityEngine;
using UnityEngine.UIElements;

public class Sounds : MonoBehaviour
{
    public static void StaticSound(AudioClip clip, AudioSource audioSource, float volume)
    {
        ConfigureAudioSource(audioSource, volume, 1f);
        audioSource.PlayOneShot(clip);
    }

    public static void Sound(AudioClip clip, AudioSource audioSource, float volume, float minPitch, float maxPitch)
    {
        GameObject slider = GameObject.Find("VolumeSlider");
        float sliderValue;
        if (slider != null)
            sliderValue = slider.GetComponent<Slider>().value;
        else sliderValue = 1f;
        float newVolume = volume * sliderValue; 
        float pitch = Random.Range(minPitch, maxPitch);
        ConfigureAudioSource(audioSource, newVolume, pitch);
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
    public static void StopPlay(AudioClip clip, AudioSource audioSource, float volume)
    {
        audioSource.Stop();
    }
}
