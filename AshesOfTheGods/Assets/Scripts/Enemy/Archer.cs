using UnityEngine;
using System.Collections;
public class Archer:MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private GameObject player;
    [SerializeField] private Transform firstGuardedPoint;
    [SerializeField] private Transform secondGuardedPoint;
    Rigidbody2D rb;
    BoxCollider2D coll;
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
    }

    private bool shootWaitMode = false;
    [SerializeField] float distance;
    private bool guardMode = true;
    private bool shootMode = false;
    private Vector2 movement;
    private bool returnWaitMode = false;

    public LayerMask layerMask;
    private void FixedUpdate()
    {
        if (!shootMode & !returnWaitMode & !guardMode)
        {
            guardMode = true;
        }

        //print($"{guardMode}{shootMode}{returnWaitMode}");
        //Vector towards the enemy 
        movement = (player.transform.position - transform.position).normalized;
        movement.y = 0;

        //Archer vision raycast system
        RaycastHit2D enemyVisionRight = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y + coll.size.y / 2, 0), Vector2.right, distance, layerMask);
        RaycastHit2D enemyVisionLeft = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y + coll.size.y / 2, 0), Vector2.left, distance, layerMask);
        
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + coll.size.y / 2, 0), Vector2.right * distance, Color.green);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + coll.size.y / 2, 0), Vector2.left * distance, Color.green);


        Vector2 movementDirection = Vector2.zero;

        if ((enemyVisionLeft.collider != null && enemyVisionLeft.collider.gameObject.CompareTag("Player")) | (enemyVisionRight.collider != null && enemyVisionRight.collider.gameObject.CompareTag("Player")))
        {
           
            if (Vector2.Distance(player.transform.position, transform.position) <= distance) //exit from guard, entrance to angry
            {

                guardMode = false;
                shootMode = true;

            }
            else if (!shootWaitMode & shootMode)
            {
                shootMode = false;
                guardMode = false;
                returnWaitMode = true;
            }
            

        }
        else if (!shootWaitMode & shootMode)
        {
            //print("�� �����");
            shootMode = false;
            guardMode = false;
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
            animator.SetTrigger("IsAttack");
            ShootMode();
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
        if (movementDirection != Vector2.zero & (!guardMode || !guardWaitMode))
        {
            spriteRenderer.flipX = movementDirection.x < 0;
        }
    }

    private bool guardModeRightMove = false;
    private bool guardWaitMode = false;
    [SerializeField] private float speed;
    
    private void GuardMode()
    {
        
            if (guardModeRightMove & !guardWaitMode)
                rb.MovePosition(rb.position + Vector2.right * speed * Time.fixedDeltaTime);

            if (!guardModeRightMove & !guardWaitMode)
                rb.MovePosition(rb.position + Vector2.left * speed * Time.fixedDeltaTime);

            if (rb.position.x + transform.localScale.x >= secondGuardedPoint.position.x & guardModeRightMove)
            {
                guardWaitMode = true;
                guardModeRightMove = false;
            }
            else if (rb.position.x - transform.localScale.x <= firstGuardedPoint.position.x & !guardModeRightMove)
            {
                guardWaitMode = true;
                guardModeRightMove = true;
            }
        
    }
  

    [SerializeField] private GameObject arrow;
    private void ShootMode()
    {
        
        if (!shootWaitMode)
        {
            Instantiate(arrow, transform.position, Quaternion.identity);
            shootWaitMode = true;
        }

    }

    [SerializeField] private float atackCD;
    [SerializeField] float guardWaitTime;
    [SerializeField] float targetLostTime;
    //[SerializeField] private float animCD;
    private IEnumerator WaitMode()
    {
        while (true)
        {
            if (guardWaitMode)
            {
                //print("���");
                yield return new WaitForSeconds(guardWaitTime);
                guardWaitMode = false;
            }
            if (shootWaitMode)
            {
                //print("�� ��������");
                yield return new WaitForSeconds(atackCD);
                shootWaitMode = false;
            }
            if (returnWaitMode)
            {
                //print("��� ������");
                yield return new WaitForSeconds(targetLostTime);
                returnWaitMode = false;
            }
            yield return new WaitForFixedUpdate();


        }
    }
}
