using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class NewStribog : Enemy
{
    [Header("Hit Sounds")]
    [SerializeField] private AudioClip[] hitSounds;
    [SerializeField] private float hitSoundVolume;
    public AudioSource audioS;
    SpriteRenderer spriteRenderer;
    private bool getDamage = false;
    private void Awake()
    {
        audioS = gameObject.AddComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        HpNow = hp;
        HpMax = hp;
    }

     private int lastSoundIndex = -1;
    private void PlayRandomHitSound()
    {
        int randomIndex = Random.Range(0, hitSounds.Length);
        if (randomIndex == lastSoundIndex)
            randomIndex = Random.Range(0, hitSounds.Length);
        else
            Sounds.StaticSound(hitSounds[randomIndex], audioS, hitSoundVolume);
        lastSoundIndex = randomIndex;
    }
    
    private void Start()
    {
        StartCoroutine(damageble());
    }
    
    public override void TakeDamage(float damage)
    {
        PlayRandomHitSound();
        getDamage = true;
        HpNow -= damage;
        gameObject.GetComponent<StribogScript>().CountAttack();
        if (HpNow <= 0)
        {
            Die();
        }
        //Debug.Log("�� �����" + HpNow);
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
}
