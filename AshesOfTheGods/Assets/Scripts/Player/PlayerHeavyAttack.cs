using System.Collections;
using UnityEngine;

public class PlayerHeavyAttack : MonoBehaviour
{
    //private Animator animator;
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
    public void Start()
    {
        //animator = GetComponent<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(AttackCooldown(attackSpeed));
    }

    public void Update()
    {
        if (!gameObject.GetComponent<PlayerStats>().isEsc)
        {
            //HandleMovement();

            if (Input.GetAxis("Fire2") == 0)
                KeyWasPressed = false;
            if (Input.GetAxis("Fire2") != 0 & !waitMode & !KeyWasPressed)
            {
                HeavyAttack();
                //animator.SetTrigger("Attack");
                KeyWasPressed = true;
            }
        }
    }

    /*private void HandleMovement()
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
    }*/

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(GetComponent<CapsuleCollider2D>().bounds.center, attackPoint.position - GetComponent<CapsuleCollider2D>().bounds.center);
    }

    public void HeavyAttack()
    {
        Vector2 direction = attackPoint.position - GetComponent<CapsuleCollider2D>().bounds.center;
        RaycastHit2D[] hits = Physics2D.RaycastAll(GetComponent<CapsuleCollider2D>().bounds.center, direction, direction.magnitude, damageableLayerMask);
        foreach (RaycastHit2D hit in hits)
        {
            hit.collider.GetComponent<Enemy>().TakeDamage(damage);
        }
        waitMode = true;

        Sounds.Sound(attackSound, audioSource, volume, minPitch, maxPitch);
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