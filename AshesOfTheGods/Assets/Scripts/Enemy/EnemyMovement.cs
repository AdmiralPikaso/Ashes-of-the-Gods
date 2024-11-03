using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private Rigidbody2D rb;
    private Vector2 movement;
    [SerializeField] private float atackDistanse;
    private bool onAtackDistanse = false;
    private bool on_ground = false;

    [SerializeField] private Transform firstGuardedPoint;
    [SerializeField] private Transform secondGuardedPoint;
    private bool guardModeRightMove = false;
    [SerializeField] private float guardDistance;
    private Vector2 returnMovement;

    private bool guardMode = true;
    private bool angryMode = false;
    private bool returnMode = false;
    
    private bool guardWaitMode = false;
    private bool returnWaitMode = false;
    private bool returnWaitModeFlag = false;
    [SerializeField] float guardWaitTime;
    [SerializeField] float targetLostTime;

    private GameObject player;
    private Transform playerTransform;
    private PlayerCollisionState player_on_platform;

    [SerializeField] float enemyDamage;
    private bool enemyCanAtack = true;
    [SerializeField] float atackCoodown;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;
        player_on_platform = player.GetComponent<PlayerCollisionState>();
        if (firstGuardedPoint.transform.position.x > secondGuardedPoint.transform.position.x)
        {
            Transform transform = firstGuardedPoint;
            firstGuardedPoint = secondGuardedPoint;
            secondGuardedPoint = transform;
        }
    }



    private void Start()
    {

        StartCoroutine(WaitMode(guardWaitTime, targetLostTime, atackCoodown));

    }

    // Update is called once per frame
    void Update()
    {
        
        

        Debug.DrawRay(new Vector3(firstGuardedPoint.position.x, -500, 0), new Vector3(0, 1000, 0));
        Debug.DrawRay(new Vector3(secondGuardedPoint.position.x, -500, 0), new Vector3(0, 1000, 0));
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
            returnMovement = (firstGuardedPoint.position - transform.position).normalized;
            returnMovement.y = 0;
        }
        else
        {
            returnMovement = (secondGuardedPoint.position - transform.position).normalized;
            returnMovement.y = 0;
        }
        //exit from return, entrance to guard
        if (rb.position.x + transform.localScale.x >= firstGuardedPoint.position.x & rb.position.x- transform.localScale.x <= secondGuardedPoint.position.x & !angryMode)
        {
            guardMode = true;
            returnMode = false;
        }

        //exit from guard, entrance to angry

        if (playerTransform.position.x > firstGuardedPoint.position.x & playerTransform.position.x < secondGuardedPoint.position.x)
        {
            angryMode = true;
            guardMode = false;
        }

        //Debug.Log(player_on_platform.GetOnPlatform());
        //exit from angry, entrance to return
        if ((playerTransform.position.y - rb.position.y>0.1f) & angryMode & player_on_platform.GetOnPlatform())
        {
            returnMode = true;
            returnWaitModeFlag = true;
            angryMode = false;
        }
    }



    private void FixedUpdate()
    {
     
        if (!onAtackDistanse & on_ground & angryMode)
            AngryMode(movement);

        else if (onAtackDistanse & angryMode & enemyCanAtack)
            AtackMode();

        if (returnMode)
            ReturnMode();

        if (guardMode)
            GuardMode();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.CompareTag("Ground"))
            on_ground = true;
  
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.CompareTag("Ground"))
            on_ground = false;
        
    }


    private void AngryMode(Vector2 movement)
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }


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

    private void AtackMode()
    {
        Debug.Log("Скелет бьёт");
        
        player.GetComponent<PlayerStats>().ReduceHp(enemyDamage);
        enemyCanAtack = false;
    }

    private IEnumerator WaitMode(float guardWaitTime, float targetLostTime, float atackCoodown)
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

