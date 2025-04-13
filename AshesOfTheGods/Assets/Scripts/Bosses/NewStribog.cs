using UnityEngine;
using UnityEngine.Audio;

public class NewStribog : Enemy
{
    private void Awake()
    {
        animator = GetComponent<Animator>();
        HpNow = hp;
        HpMax = hp;
    }

    public override void TakeDamage(float damage)
    {
        Sounds.Sound(damageSound, GetComponent<StribogScript>().audioSource, volume, minPitch, maxPitch);
        HpNow -= damage;
        gameObject.GetComponent<StribogScript>().CountAttack();
        if (HpNow <= 0)
        {
            Die();
        }
        //Debug.Log("�� �����" + HpNow);
    }
}
