using System.Collections;
using UnityEngine;

public class PlayerHeavyAttack : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask damageableLayerMask;
    private float damage;
    private float attackColldown;
    private bool waitMode = false;
    private bool KeyWasPressed = false;

    public void Start()
    {
        damage = 30;
        attackColldown = 1.0f;
        StartCoroutine(AttackCooldown(attackColldown));
    }

    public void Update()
    {
        HandleMovement();

        if (Input.GetAxis("Fire2") == 0)
            KeyWasPressed = false;
        if (Input.GetAxis("Fire2") != 0 && !waitMode && !KeyWasPressed)
        {
            Attack();
            KeyWasPressed = true;
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

    private IEnumerator AttackCooldown(float attackColldown)
    {
        while (true)
        {
            if (waitMode)
            {
                yield return new WaitForSeconds(attackColldown);
                waitMode = false;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}