using UnityEngine;
using UnityEngine.Audio;

public class NewStribog : Enemy
{
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

    public override void TakeDamage(float damage)
    {
        Sounds.Sound(damageSound, audioSource, volume, minPitch, maxPitch);
        HpNow -= damage;
        gameObject.GetComponent<StribogScript>().CountAttack();
        if (HpNow <= 0)
        {
            Die();
        }
        //Debug.Log("ея т№рур" + HpNow);

    }
}
