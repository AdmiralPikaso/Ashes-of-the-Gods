using UnityEngine;

public class Sounds : MonoBehaviour
{
    public static float hearingRadius = 1000f;
    private static Transform listenerTransform;

    /*private static bool IsWithinHearingRange(Transform sourceTransform)
    {
        if (listenerTransform == null)
        {
            //listenerTransform = player.GetComponent<CapsuleCollider2D>().bounds.center;
        }

        return Vector2.Distance(listenerTransform.position, sourceTransform.position) <= hearingRadius;
    }*/

    public static void StaticSound(AudioClip clip, AudioSource audioSource, float volume)
    {
        //if (!IsWithinHearingRange(audioSource.transform)) return;

        audioSource.volume = volume;
        audioSource.pitch = 1f;
        audioSource.PlayOneShot(clip);
    }

    public static void Sound(AudioClip clip, AudioSource audioSource, float volume, float minPitch, float maxPitch)
    {
        //if (!IsWithinHearingRange(audioSource.transform)) return;

        audioSource.volume = volume;
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.PlayOneShot(clip);
    }
}
