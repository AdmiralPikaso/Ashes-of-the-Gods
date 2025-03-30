using UnityEngine;
using System.Collections;

public class Archer : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private GameObject player;
    [SerializeField] private Transform firstGuardedPoint;
    [SerializeField] private Transform secondGuardedPoint;
    Rigidbody2D rb;
    BoxCollider2D coll;


    [SerializeField]private float distance;
    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (firstGuardedPoint.transform.position.x > secondGuardedPoint.transform.position.x)
        {
            Transform transform = firstGuardedPoint;
            firstGuardedPoint = secondGuardedPoint;
            secondGuardedPoint = transform;
        }

        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(WaitMode());

        //distance = Vector2.Distance(firstGuardedPoint.position, secondGuardedPoint.position)*3/4;
    }

    private bool shootWaitMode = false;
    
    private bool guardMode = true;
    private bool shootMode = false;
    private Vector2 movement;
    private bool returnWaitMode = false;
    private bool isAttacking = false;

    public LayerMask layerMask;

    private Vector2 visionVec;
    private void FixedUpdate()
    {
        //Debug.Log($"GuardMode {guardMode}");
        //Debug.Log($"ShootMode {shootMode}");

        //Debug.Log($"shootwait{shootWaitMode}");
        //Debug.Log($"guardWait {guardWaitMode}");
        //Debug.Log($"returnWait {returnWaitMode}");

        //Debug.Log($"isAttacking {isAttacking}"); 
        

        if (!shootMode & !returnWaitMode & !guardMode)
        {
            guardMode = true;
        }

        movement = (player.transform.position - transform.position).normalized;
        visionVec = movement;
        movement.y = 0;
        //RaycastHit2D enemyVisionRight = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y + coll.size.y / 2, 0), visionVec, distance, layerMask);
        //RaycastHit2D enemyVisionLeft = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y + coll.size.y / 2, 0), visionVec, distance, layerMask);
        RaycastHit2D enemyVision = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y + coll.size.y / 2, 0), visionVec, distance, layerMask); ;




        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + coll.size.y / 2, 0), visionVec * distance, Color.green);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + coll.size.y / 2, 0), visionVec * distance, Color.green);

        Vector2 movementDirection = Vector2.zero;

        // Проверка видимости игрока
        bool playerInSight = (enemyVision.collider != null && enemyVision.collider.gameObject.CompareTag("Player"));
        //Debug.Log($"Вижн {playerInSight && Vector2.Distance(player.transform.position, transform.position) <= distance}");

        if (playerInSight && Vector2.Distance(player.transform.position, transform.position) <= distance && canSomething)
        {
            if (!shootMode && !shootWaitMode)
            {
                guardMode = false;
                shootMode = true;
               
            }
        }
        else if (shootMode && !shootWaitMode && !isAttacking)
        {
            shootMode = false;
            returnWaitMode = true;
            
        }

        if (guardMode)
        {
            GuardMode();
            animator.SetBool("IsWalking", !guardWaitMode);
            movementDirection = (guardModeRightMove ? Vector2.right : Vector2.left);
        }
        else if (shootMode)
        {
            if (!isAttacking && !shootWaitMode)
            {
                animator.SetTrigger("IsAttack");
                isAttacking = true;
            }
            animator.SetBool("IsWalking", false);
            movementDirection = (player.transform.position - transform.position).normalized;
        }
        else if (returnWaitMode)
        {
            animator.SetBool("IsWalking", false);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        if (movementDirection != Vector2.zero && (!guardMode || !guardWaitMode) && canSomething)
        {
            spriteRenderer.flipX = movementDirection.x < 0;
        }
    }

    private bool guardModeRightMove = false;
    private bool guardWaitMode = false;
    [SerializeField] private float speed;

    private void GuardMode()
    {
        if (guardModeRightMove && !guardWaitMode)
            rb.MovePosition(rb.position + Vector2.right * speed * Time.fixedDeltaTime);

        if (!guardModeRightMove && !guardWaitMode)
            rb.MovePosition(rb.position + Vector2.left * speed * Time.fixedDeltaTime);

        if (rb.position.x + transform.localScale.x >= secondGuardedPoint.position.x && guardModeRightMove)
        {
            guardWaitMode = true;
            //isAttacking = false;
            guardModeRightMove = false;
        }
        else if (rb.position.x - transform.localScale.x <= firstGuardedPoint.position.x && !guardModeRightMove)
        {
            guardWaitMode = true;
            //isAttacking = false;
            guardModeRightMove = true;
        }
    }

    [SerializeField] private GameObject arrow;
    [SerializeField] private float atackCD;
    [SerializeField] float guardWaitTime;
    [SerializeField] float targetLostTime;

    private IEnumerator WaitMode()
    {
        while (true)
        {
            if (guardWaitMode)
            {
                yield return new WaitForSeconds(guardWaitTime);
                guardWaitMode = false;
            }

            if (shootWaitMode)
            {
                yield return new WaitForSeconds(atackCD);
                shootWaitMode = false;
                isAttacking = false;
            }

            if (returnWaitMode)
            {
                yield return new WaitForSeconds(targetLostTime);
                returnWaitMode = false;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    
    private IEnumerator CD()
    {

        //yield return new WaitForSeconds(atackCD);
        //shootWaitMode = false;
        yield return new WaitForSeconds(0);




        //RaycastHit2D enemyVisionRight = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y + coll.size.y / 2, 0), Vector2.right, distance, layerMask);
        //RaycastHit2D enemyVisionLeft = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y + coll.size.y / 2, 0), Vector2.left, distance, layerMask);

        /*RaycastHit2D enemyVision = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y + coll.size.y / 2, 0), visionVec, distance, layerMask);

        bool playerInSight = (enemyVision.collider != null && enemyVision.collider.gameObject.CompareTag("Player"));
                             

        if (playerInSight && Vector2.Distance(player.transform.position, transform.position) <= distance)
        {
            shootMode = true;
            guardMode = false;
        }
        /*else
        {
            guardMode = true;
        }
        */

    }
    

    private void CreatingAnArrow()
    {
        if (!shootWaitMode)
        {
            Instantiate(arrow, transform.position, Quaternion.identity);
            shootWaitMode = true;
        }
    }

    private void Destruction()
    {
        canSomething = false;
        guardModeRightMove = false;
        returnWaitMode = false;
        guardWaitMode = false;
        shootMode = false;
        guardMode = false;
        speed = 0;
        Destroy(gameObject, 5f);
    }

    private bool canSomething = true;
}