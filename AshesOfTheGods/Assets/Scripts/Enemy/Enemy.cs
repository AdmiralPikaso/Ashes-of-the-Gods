using System.Collections;
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

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer melleSprite;
    private SpriteRenderer rangeSprite;

    private bool getDamage = false;
    public float HpNow { get; protected set; }
    public float HpMax { get; protected set; }
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        if (this.gameObject.name != "Perun")
            StartCoroutine(damageble());
        else
            StartCoroutine(Perundamageble());
    }
    private void Awake()
    {
        if (this.gameObject.name != "Perun" )
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        else
        {
            melleSprite = gameObject.transform.Find("MelleHand").GetComponent<SpriteRenderer>();
            rangeSprite = gameObject.transform.Find("RangeHand").GetComponent<SpriteRenderer>();
        }
        animator = GetComponent<Animator>();
        HpNow = hp;
        HpMax = hp;
    }

    public virtual void TakeDamage(float damage)
    {
        Sounds.Sound(damageSound, audioSource, volume, minPitch, maxPitch);
        getDamage = true;
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
        if(deathSound != null)
            Sounds.Sound(deathSound, audioSource, volume, minPitch, maxPitch);
        if (!gameObject.CompareTag("Perun"))
        {
            GetComponent<BoxCollider2D>().enabled = false;
            animator.SetBool("IsDie", true);
        }
        isDead = true;
        print("Смерть");

    }


    private IEnumerator damageble()
    {
        while (true)
        {
            if (getDamage)
            {
                spriteRenderer.color = new Color(1, 0.47f, 0.47f, 1);
                yield return new WaitForSeconds(0.2f);
                spriteRenderer.color = Color.white;
                getDamage = false;
            }

            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator Perundamageble()
    {
        while (true)
        {
            if (getDamage)
            {
                melleSprite.color = new Color(1, 0.47f, 0.47f, 1);
                rangeSprite.color = new Color(1, 0.47f, 0.47f, 1);
                
                yield return new WaitForSeconds(0.2f);
                melleSprite.color = Color.white;
                rangeSprite.color = Color.white;
                getDamage = false;
            }

            yield return new WaitForFixedUpdate();
        }
    }
}