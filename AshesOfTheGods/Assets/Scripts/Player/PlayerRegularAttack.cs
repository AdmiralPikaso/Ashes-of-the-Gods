using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerRegularAttack : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask damageableLayerMask;
    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private AudioClip attackSound;
    private AudioSource audioSource;
    [SerializeField] private float minPitch;
    [SerializeField] private float maxPitch;
    [SerializeField] private float volume;
    private bool waitMode = false;
    private bool KeyWasPressed = false;
    //public bool canMove = true;
    private PlayerMovement playerMovement;
    public void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        StartCoroutine(AttackCooldown(attackSpeed));
    }

    public void Update()
    {
        if (!gameObject.GetComponent<PlayerStats>().isEsc)
        {
            //HandleMovement();

            if (Input.GetAxis("Fire1") == 0)
                KeyWasPressed = false;
            if (Input.GetAxis("Fire1") != 0 && !waitMode & !KeyWasPressed)
            {
                animator.SetTrigger("Attack");
                KeyWasPressed = true;
            }
        }
    }

    /*private void HandleMovement()
    {
        if (canMove)
        {
            float moveDir = playerMovement.GetMoveDirection();

            if (moveDir > 0)
            {
                attackPoint.localPosition = new Vector2(-Mathf.Abs(attackPoint.localPosition.x), attackPoint.localPosition.y);
            }
            if (moveDir < 0)
            {
                attackPoint.localPosition = new Vector2(Mathf.Abs(attackPoint.localPosition.x), attackPoint.localPosition.y);
            }
        }
    }*/

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(GetComponent<CapsuleCollider2D>().bounds.center, attackPoint.position - GetComponent<CapsuleCollider2D>().bounds.center);
    }

    public void RegularAttack()
    {
        Vector2 direction = attackPoint.position - GetComponent<CapsuleCollider2D>().bounds.center;
        RaycastHit2D hit = Physics2D.Raycast(GetComponent<CapsuleCollider2D>().bounds.center, direction, direction.magnitude, damageableLayerMask);
        if (hit.collider != null)
        {
            hit.collider.GetComponent<Enemy>().TakeDamage(damage);
        }
        waitMode = true;
        
        Sounds.Sound(attackSound, audioSource, volume, minPitch, maxPitch);
    }

    /*private void CanNotMove()
    {
        canMove = false;
    }
    private void CanMove()
    {
        canMove = true;
    }*/

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