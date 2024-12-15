using NUnit.Framework;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float hp;
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
        animator = GetComponent<Animator>();
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
        Debug.Log("Хп врага" + HpNow);

    }

    void Die()
    {
        print("Смерть");
        animator.SetBool("IsDie", true);
        Destroy(gameObject, 1.5f);
    }
}