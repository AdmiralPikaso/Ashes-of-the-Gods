using System.Xml.Serialization;
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
    
    public bool isDead = false;
    protected void Die()
    {   
        Sounds.Sound(deathSound, audioSource, volume, minPitch, maxPitch);
        GetComponent<BoxCollider2D>().enabled = false;
        isDead = true;
        print("Смерть");
        animator.SetBool("IsDie", true);
    }
}