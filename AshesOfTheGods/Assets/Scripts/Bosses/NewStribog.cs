using UnityEngine;
using UnityEngine.Audio;

public class NewStribog : Enemy
{
    [Header("Hit Sounds")]
    [SerializeField] private AudioClip[] hitSounds;
    [SerializeField] private float hitSoundVolume;
    public AudioSource audioS;
    private void Awake()
    {
        audioS = gameObject.AddComponent<AudioSource>();
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
    public override void TakeDamage(float damage)
    {
        PlayRandomHitSound();
        HpNow -= damage;
        gameObject.GetComponent<StribogScript>().CountAttack();
        if (HpNow <= 0)
        {
            Die();
        }
        //Debug.Log("�� �����" + HpNow);
    }
}
