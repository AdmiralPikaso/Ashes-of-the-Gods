using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRegularAttack : MonoBehaviour
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

        if (Input.GetAxis("Fire1") != 0 && !waitMode)
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, direction.magnitude, damageableLayerMask);
        if (hit.collider != null)
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