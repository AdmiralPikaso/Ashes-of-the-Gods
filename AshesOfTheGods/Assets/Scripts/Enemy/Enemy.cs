using NUnit.Framework;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator animator;
    [SerializeField] protected float hp;
    [SerializeField] protected AudioClip damageSound;
    [SerializeField] protected AudioClip deathSound;
    protected AudioSource audioSource;
    [SerializeField] protected float minPitch;
    [SerializeField] protected float maxPitch;
    [SerializeField] protected float volume;
    public float HpNow { get; protected set; }
    public float HpMax { get; protected set; }
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

    public virtual void TakeDamage(float damage)
    {
        Sounds.Sound(damageSound, audioSource, volume, minPitch, maxPitch);
        HpNow -= damage;
        if (HpNow <= 0)
        {
            Die();
        }
        Debug.Log("Хп врага" + HpNow);

    }

    protected void Die()
    {
        print("Смерть");
        animator.SetBool("IsDie", true);
        Destroy(gameObject, 1.5f);
    }
}