using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class NewStribog : Enemy
{
    SpriteRenderer spriteRenderer;
    private bool getDamage = false;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();
        HpNow = hp;
        HpMax = hp;
    }

    private void Start()
    {
        StartCoroutine(damageble());
    }
    public override void TakeDamage(float damage)
    {
        Sounds.Sound(damageSound, GetComponent<StribogScript>().audioSource, volume, minPitch, maxPitch);
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
