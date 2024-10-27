using UnityEngine;

public class PlayerHeavyAttack : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask damageableLayerMask;
    [SerializeField] private float damage;
    [SerializeField] private float attackRange;
    [SerializeField] private float timeBtwAttack;

    private float timer;

    public void Update()
    {
        Attack();
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void Attack()
    {
        if (timer <= 0)
        {
            if (Input.GetAxis("Fire2") != 0)
            {
                print("Нажато");

                Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, damageableLayerMask);
                print(enemies.Length);
                if (enemies.Length != 0)
                {
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        enemies[i].GetComponent<Enemy>().TakeDamage(damage);
                        print("Урон нанесён");
                    }
                }
                timer = timeBtwAttack;
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
