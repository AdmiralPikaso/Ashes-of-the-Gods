using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float hp;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip deathSound;
    private AudioSource audioSource;
    [SerializeField] private float minPitch;
    [SerializeField] private float maxPitch;
    [SerializeField] private float volume;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;

        Sounds.Sound(damageSound, audioSource, volume, minPitch, maxPitch);

        if (hp <= 0)
        {
            Die();
        }
        
        Debug.Log(hp);
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        Destroy(gameObject);
    }
}