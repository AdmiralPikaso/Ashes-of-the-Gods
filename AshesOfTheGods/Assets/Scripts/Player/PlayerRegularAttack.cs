using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRegularAttack : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask damageableLayerMask;
    private float damage;
    private float attackColldown;
    private bool waitMode = false;
    private bool KeyWasPressed = false;

    public void Start()
    {
        damage = 10;
        attackColldown = 0.3f;
        StartCoroutine(AttackCooldown(attackColldown));
    }

    public void Update()
    {
        HandleMovement();

        if (Input.GetAxis("Fire1") == 0)
            KeyWasPressed = false;
        if (Input.GetAxis("Fire1") != 0 && !waitMode && !KeyWasPressed)
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, direction.magnitude, damageableLayerMask);
        if (hit.collider != null)
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