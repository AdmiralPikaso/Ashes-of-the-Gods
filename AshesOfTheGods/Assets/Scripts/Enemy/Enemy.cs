using NUnit.Framework;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip deathSound;
    private AudioSource audioSource;
    [SerializeField] private float minPitch;
    [SerializeField] private float maxPitch;
    [SerializeField] private float volume;
    public float HpNow { get; private set; }
    public float HpMax { get; private set; }
    [SerializeField] protected float hp;
    void Start()
    {
    audioSource = gameObject.AddComponent<AudioSource>();  
    }
    private void Awake()
    {
        HpNow = hp;
        HpMax = hp;
    }

    public void TakeDamage(float damage)
    {
        Sounds.Sound(damageSound, audioSource, volume, minPitch, maxPitch);
        HpNow -= damage;
        if (HpNow <= 0)
        {
            Die();
        }      
        Debug.Log(hp);
    }

    void Die()
    {
       // AudioSource.PlayClipAtPoint(deathSound, transform.position);
        Destroy(gameObject);
    }
}