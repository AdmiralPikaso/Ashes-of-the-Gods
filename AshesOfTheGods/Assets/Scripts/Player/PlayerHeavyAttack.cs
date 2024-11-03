using System.Collections;
using UnityEngine;

public class PlayerHeavyAttack : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask damageableLayerMask;
    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed;
    private bool waitMode = false;
    
    public void Start()
    {
        StartCoroutine(AttackCooldown(attackSpeed));
    }

    public void Update()
    {
        HandleMovement();

        if (Input.GetAxis("Fire2") != 0 && !waitMode)
        {
            Attack();
        }
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput == 1 && attackPoint.localPosition.x < 0)
        {
            attackPoint.localPosition = new Vector3(-attackPoint.localPosition.x, attackPoint.localPosition.y, attackPoint.localPosition.z);
        }
        else if (horizontalInput == -1 && attackPoint.localPosition.x > 0)
        {
            attackPoint.localPosition = new Vector3(-attackPoint.localPosition.x, attackPoint.localPosition.y, attackPoint.localPosition.z);
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, attackPoint.position - transform.position);
    }

    public void Attack()
    {
        Vector2 direction = attackPoint.position - transform.position;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, direction.magnitude, damageableLayerMask);
        foreach (RaycastHit2D hit in hits)
        {
            hit.collider.GetComponent<Enemy>().TakeDamage(damage);
        }
        waitMode = true;
    }

    private IEnumerator AttackCooldown(float attackSpeed)
    {
        while (true)
        {
            if (waitMode)
            {
                yield return new WaitForSeconds(attackSpeed);
                waitMode = false;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}