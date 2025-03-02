using System.Xml.Serialization;
using NUnit.Framework;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    [SerializeField] protected float hp;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip deathSound;
    private AudioSource audioSource;
    [SerializeField] private float minPitch;
    [SerializeField] private float maxPitch;
    [SerializeField] private float volume;
    public float HpNow { get; private set; }
    public float HpMax { get; private set; }
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
    public bool isDead = false;
    void Die()
    {   isDead = true;
        print("Смерть");
        animator.SetBool("IsDie", true);
    }
}