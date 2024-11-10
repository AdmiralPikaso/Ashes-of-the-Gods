using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

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


    private void Start()
    {
        StartCoroutine(WaitMode());
    }


    private bool guardMode = true;
    private bool angryMode = false;
    private bool returnMode = false;
    void Update()
    {
        print($"{guardMode}{angryMode}{returnMode}");
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
    private void FixedUpdate()
    { 
        //Atack Distance check
        if (Vector2.Distance(playerTransform.position, transform.position) <= atackDistanse - 1)
            onAtackDistanse = true;

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

        RaycastHit2D enemyVisionRight = Physics2D.Raycast(transform.position, Vector2.right, distance,layerMask);
        RaycastHit2D enemtVisionLeft = Physics2D.Raycast(transform.position, Vector2.left, distance,layerMask);
        Debug.DrawRay(transform.position, Vector2.right * distance, Color.green);
        Debug.DrawRay(transform.position, Vector2.left * distance, Color.green);

        //exit from return, entrance to guard
        if (rb.position.x - transform.localScale.x >= firstGuardedPoint.position.x - 0.1f & rb.position.x + transform.localScale.x <= secondGuardedPoint.position.x + 0.5f & !angryMode)
        {
            returnMode = false;
            guardMode = true;
        }


        //exit from guard, entrance to angry
        //if (playerTransform.position.x > firstGuardedPoint.position.x & playerTransform.position.x < secondGuardedPoint.position.x)
        if (enemyVisionRight.collider != null | enemtVisionLeft.collider !=null)
        {
           // Debug.Log("Enemy Detected");
            guardMode = false;
            returnMode = false;
            angryMode = true;
        }

        //Debug.Log(player_on_platform.GetOnPlatform());
        //exit from angry, entrance to return
        if ((playerTransform.position.y - rb.position.y > 0.1f) & angryMode & player_on_platform.GetOnPlatform())
        {
            angryMode = false;
            returnMode = true;
            returnWaitModeFlag = true;
        }

        if (!onAtackDistanse & angryMode)
            AngryMode(movement);

        else if (onAtackDistanse & angryMode & enemyCanAtack)
            AtackMode();

        if (returnMode)
            ReturnMode();

        if (guardMode)
            GuardMode();
    }

    [Space]
    [SerializeField] private float speed;
    private void AngryMode(Vector2 movement)
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }


    private bool guardModeRightMove = false;
    private bool guardWaitMode = false;
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

    
    private bool returnWaitMode = false;
    private void ReturnMode()
    {
        if (returnWaitModeFlag)
        {
            returnWaitMode = true;
            returnWaitModeFlag = false;
        }
        if (!returnWaitMode)
            rb.MovePosition(rb.position + returnMovement * speed * Time.fixedDeltaTime);

    }

    
    [SerializeField] float enemyDamage;
    private bool enemyCanAtack = true;
    private void AtackMode()
    {
        Debug.Log("Скелет бьёт");

        player.GetComponent<PlayerStats>().ReduceHp(enemyDamage);
        enemyCanAtack = false;
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
                print("Ждёт");
                yield return new WaitForSeconds(guardWaitTime);
                guardWaitMode = false;
            }
            if (returnWaitMode)
            {
                print("Ждёт ретёрна");
                yield return new WaitForSeconds(targetLostTime);
                returnWaitMode = false;
            }
            if (!enemyCanAtack)
            {
                print("Кд атаки");
                yield return new WaitForSeconds(atackCoodown);
                enemyCanAtack = true;
            }
            yield return new WaitForFixedUpdate();


        }
    }
}

