using UnityEngine;

public class PlayerRegularAttack : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask damageableLayerMask;
    [SerializeField] private float damage;
    [SerializeField] private float attackRange;
    [SerializeField] private float timeBtwAttack;

    private float timer = 0;

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
            if (Input.GetAxis("Fire1") != 0)
            {
                // Проверяем ближайшего врага
                Collider2D closestEnemy = Physics2D.OverlapCircle(attackPoint.position, attackRange, damageableLayerMask);

                if (closestEnemy != null)
                    closestEnemy.GetComponent<Enemy>().TakeDamage(damage);
                timer = timeBtwAttack;
                print("Атакован");
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}