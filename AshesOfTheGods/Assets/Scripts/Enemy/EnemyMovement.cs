using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] protected float speed;
    private Transform player;
    private Rigidbody2D rb;
    private Vector2 movement;
    [SerializeField] protected float atackDistanse;
    private bool onAtackDistanse = false;
    private bool on_ground = false;

    [SerializeField] protected Transform guardedPoint;
    private bool guardModeRightMove;
    [SerializeField] protected float guardDistance;
    private Vector2 returnMovement;

    bool guardMode = true;
    bool angryMode = false;
    bool returnMode = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Проверка дистанции для атаки
        if (Vector2.Distance(player.position, transform.position) <= atackDistanse-1)
            onAtackDistanse = true;
        
        else
            onAtackDistanse = false;

        //вектор в сторону игрока
        movement = (player.position - transform.position).normalized;
        movement.y = 0;

        //вектор в сторону зоны патрулирования
        returnMovement = (guardedPoint.position - transform.position).normalized;
        returnMovement.y = 0;

        //находится ли скелет в зоне патрулирования
        if (Vector2.Distance(rb.position, guardedPoint.position) < guardDistance + 0.1f && !angryMode)
        {
            guardMode = true;
            returnMode = false;
        }

        //находится ли игрок в зоне патрулирования
        if (Vector2.Distance(player.position,guardedPoint.position) < guardDistance)
        {
            angryMode = true;
            guardMode = false;
        }

        if (player.position.y >= -4f && angryMode)
        {
            returnMode = true;
            angryMode = false;
        }
    }



    private void FixedUpdate()
    {
        if (!onAtackDistanse & on_ground & angryMode)
          AngryMode(movement);

        if (returnMode)
            ReturnMode();

        if (guardMode)
            GuardMode(); 
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.CompareTag("Ground"))
            on_ground = true;
    }

    protected void OnCollisionExit2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.CompareTag("Ground"))
            on_ground = false;
    }

    //Движение за игроком
    private void AngryMode(Vector2 movement)
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    //Зона патрулирования
    private void GuardMode()
    {
        if (rb.position.x > guardedPoint.position.x + guardDistance)
            guardModeRightMove = false;
        
        else if (rb.position.x < guardedPoint.position.x - guardDistance)
            guardModeRightMove = true;
        
        if (guardModeRightMove)
            rb.MovePosition(rb.position + Vector2.right * speed * Time.fixedDeltaTime);
        
        else
            rb.MovePosition(rb.position + Vector2.left*speed * Time.fixedDeltaTime);
    }

    //Возвращение в зону патрулирования
    private void ReturnMode()
    {
            rb.MovePosition(rb.position + returnMovement * speed * Time.fixedDeltaTime);   
    }

}
