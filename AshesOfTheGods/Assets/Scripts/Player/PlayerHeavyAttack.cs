using UnityEngine;

public class PlayerAttack : Player
{
    [SerializeField] public Transform attackPoint;
    [SerializeField] public LayerMask damageableLayerMask;
    [SerializeField] public float damage;
    [SerializeField] public float attackRange;
    [SerializeField] public float timeBtwAttack;

    private float timer;

    private void Update()
    {
        Attack();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void Attack()
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
