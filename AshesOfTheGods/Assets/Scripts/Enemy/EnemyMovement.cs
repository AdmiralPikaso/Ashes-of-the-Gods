using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private GameObject player;
    private Transform playerTransform;
    [Header("Точки режима патрулирования")]
    [SerializeField] private Transform firstGuardedPoint;
    [SerializeField] private Transform secondGuardedPoint;
    
    private PlayerMovement player_on_platform;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;
        player_on_platform = player.GetComponent<PlayerMovement>();
        if (firstGuardedPoint.transform.position.x > secondGuardedPoint.transform.position.x)
        {
            Transform transform = firstGuardedPoint;
            firstGuardedPoint = secondGuardedPoint;
            secondGuardedPoint = transform;
        }
    }

    BoxCollider2D coll;
    CapsuleCollider2D playerColl;
    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        playerColl = player.GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (animator == null) Debug.LogError("Animator component not found!");
        if (spriteRenderer == null) Debug.LogError("SpriteRenderer component not found!");
        StartCoroutine(WaitMode());
    }


    private bool guardMode = true;
    private bool angryMode = false;
    private bool returnMode = false;
    void Update()
    {
        //print($"{guardMode}{angryMode}{returnMode}");
        Debug.DrawRay(new Vector3(firstGuardedPoint.position.x, -500, 0), new Vector3(0, 1000, 0));
        Debug.DrawRay(new Vector3(secondGuardedPoint.position.x, -500, 0), new Vector3(0, 1000, 0));
    }

    [Space]
    [Header("дальность атаки и дальность зрения")]
    [SerializeField] float distance;
    [SerializeField] private float atackDistanse;
    
    public LayerMask layerMask;
    private Vector2 movement;
    private bool onAtackDistanse = false;
    private Vector2 returnMovement;
    private bool returnWaitModeFlag = false;
    private bool isWalking = false;
    private void FixedUpdate()
    {
        //Debug.Log(enemyCanAtack);
        //Debug.Log("атак" + isAttack);
        //Atack Distance check
        if (Vector2.Distance(playerTransform.position, transform.position) <= atackDistanse - 1)
        {
            onAtackDistanse = true;
        }
            

        else
            onAtackDistanse = false;

        //Vector towards the enemy 
        movement = (playerTransform.position - transform.position).normalized;
        movement.y = 0;

        //Vector towards the guarded point
        if (Vector2.Distance(rb.position, firstGuardedPoint.position) < Vector2.Distance(rb.position, secondGuardedPoint.position))
        {
            returnMovement = (firstGuardedPoint.position - transform.position + new Vector3(1, 0, 0)).normalized;
            returnMovement.y = 0;
        }
        else
        {
            returnMovement = (secondGuardedPoint.position - transform.position - new Vector3(1, 0, 0)).normalized;
            returnMovement.y = 0;
        }

        RaycastHit2D enemyVisionRight = Physics2D.Raycast(new Vector3 (transform.position.x, transform.position.y + coll.size.y/2, 0), Vector2.right, distance);
        RaycastHit2D enemyVisionLeft = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y + coll.size.y / 2, 0), Vector2.left, distance);
        Debug.DrawRay(new Vector3(transform.position.x,  transform.position.y + coll.size.y / 2, 0), Vector2.right * distance, Color.green);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + coll.size.y / 2, 0), Vector2.left * distance, Color.green);

        //exit from return, entrance to guard
        if (rb.position.x - transform.localScale.x >= firstGuardedPoint.position.x - 0.1f & rb.position.x + transform.localScale.x <= secondGuardedPoint.position.x + 0.5f & !angryMode)
        {
            returnMode = false;
            guardMode = true;
        }


        //exit from guard, entrance to angry
        //if (playerTransform.position.x > firstGuardedPoint.position.x & playerTransform.position.x < secondGuardedPoint.position.x)
        if ((enemyVisionRight.collider != null && enemyVisionRight.collider.gameObject.CompareTag("Player") 
        && !enemyVisionRight.collider.gameObject.CompareTag("Ground")  && !enemyVisionRight.collider.gameObject.CompareTag("Wall"))
        || (enemyVisionLeft.collider != null && enemyVisionLeft.collider.gameObject.CompareTag("Player") 
        && !enemyVisionLeft.collider.gameObject.CompareTag("Ground")  && !enemyVisionLeft.collider.gameObject.CompareTag("Wall")))
        {
           // Debug.Log("Enemy Detected");
            guardMode = false;
            returnMode = false;
            angryMode = true;
        }

        //Debug.Log(player_on_platform.GetOnPlatform());
        //exit from angry, entrance to return
        if (((playerTransform.position.y - playerColl.size.y/2 > transform.position.y + coll.size.y/2) | (playerTransform.position.y + playerColl.size.y / 2 < transform.position.y - coll.size.y / 2)) & angryMode & !player_on_platform.InAir())
        {
            angryMode = false;
            returnMode = true;
            returnWaitModeFlag = true;
        }

        Vector2 movementDirection = Vector2.zero;
        
        if (!onAtackDistanse & angryMode)
        {
            AngryMode();
            if (!isAttack)
                isWalking = true;
            movementDirection = movement;
        }
        else if (onAtackDistanse & angryMode & enemyCanAtack)
        {
            isWalking = false;
            AtackMode();
        }
        else if (returnMode)
        {
            isWalking = false;
            ReturnMode();
            movementDirection = returnMovement;
        }
        else if (guardMode)
        {
            GuardMode();
            isWalking = !guardWaitMode;
            movementDirection = (guardModeRightMove ? Vector2.right : Vector2.left);
        }
        else
        {
            isWalking = false;
        }
            
        animator.SetBool("IsWalking", isWalking);

        if (isWalking & !gameObject.GetComponent<Enemy>().isDead)
        {
            spriteRenderer.flipX = movementDirection.x < 0;
        }
    }

    [Space]
    [SerializeField] private float speed;
    
    private void AngryMode()
    {
        if (!isAttack)
        {
            rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * movement);
        }
    }


    private bool guardModeRightMove = false;
    private bool guardWaitMode = false;
    public void GuardMode()
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

    
    private bool returnWaitMode = false;
    private void ReturnMode()
    {
        if (returnWaitModeFlag)
        {
            returnWaitMode = true;
            isWalking = false;
            returnWaitModeFlag = false;
        }
        if (!returnWaitMode)
        {
            rb.MovePosition(rb.position + returnMovement * speed * Time.fixedDeltaTime);
            isWalking = true;
        }
    }

    
    [SerializeField] float enemyDamage;
    private bool enemyCanAtack = true;
    private bool isAttack = false;
    private void AtackMode()
    {
        isAttack = true;
        Debug.Log("Скелет бьёт");
        animator.SetTrigger("Attack");
        enemyCanAtack = false;
    }
    
    private void EnemyAtack()
    {
        if (onAtackDistanse)
        {
            player.GetComponent<PlayerStats>().ReduceHp(enemyDamage);
        }
    }

    [Space]
    [Header("Время ожидания на точке, ожидания после потери и кд атаки")]
    [SerializeField] float guardWaitTime;
    [SerializeField] float targetLostTime;
    [SerializeField] float atackCoodown;
    
    private IEnumerator WaitMode()
    {
        while (true)
        {
            if (guardWaitMode)
            {
                //print("Ждёт");
                yield return new WaitForSeconds(guardWaitTime);
                guardWaitMode = false;
            }
            if (returnWaitMode)
            {
                //print("Ждёт ретёрна");
                yield return new WaitForSeconds(targetLostTime);
                returnWaitMode = false;
            }
            
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator CD()
    {
        if (!enemyCanAtack)
            {
                //print("Кд атаки");
                isAttack = false;
                yield return new WaitForSeconds(atackCoodown);
                enemyCanAtack = true;
            }
    }

    void Destruction()
    {
        isWalking = false;
        //enemyCanAtack = false;
        returnWaitMode = false;
        guardWaitMode = false;
        speed = 0;
        Destroy(gameObject, 5f);   
    }
}

