using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.UI;

public class SimarglBehaivor : MonoBehaviour
{
    GameObject player;
    Rigidbody2D rb;

    Rigidbody2D playerRB;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        playerRB = player.GetComponent<Rigidbody2D>();
        StartCoroutine(WaitAfterAttack());
        StartCoroutine(WaitAfterOneAttack());
        StartCoroutine(FireBlastCd());
    }

    //Вектор в сторону игрока
    private Vector2 movement;

    [Header("скорость передвижения")]
    [SerializeField] private float speed;

    [Space]
    [Header("дистанция атаки")]
    [SerializeField] private float attackDistance;

    private int attackCount = 0;
    
    private Vector2 attackVector;
    private bool secondFaseJump = false;
    private float blastSeries = 0;
    private void FixedUpdate() // flip x = false => повернут влево
    {
        Debug.Log($"Кд после атаки {waitAfterOneAttack}");
        Debug.Log($"Атака в кд {waitAfterAttack}");
        Debug.Log($"Кол-во атак {attackCount}");

        if (transform.position.x < ActivePillar.transform.position.x)
            attackCount = 0;

        if (gameObject.GetComponent<SimarglScript>().IsActive)
        {
            //Вектор в сторону игрока 
            movement = (player.transform.position - transform.position).normalized;
            movement.y = 0;

            if (!secondFaseJump & !waitAfterAttack)
            {
                if ((Vector2.Distance(player.transform.position, rb.position) <= attackDistance & !waitAfterOneAttack) | (attackCount>0 & !waitAfterOneAttack))
                {
                    if (attackCount == 0)
                    {
                        if (!gameObject.GetComponent<SpriteRenderer>().flipX)
                            attackVector = Vector2.left.normalized;
                        else attackVector = Vector2.right.normalized;
                    }
                    BasicAttack();

                }
                if (attackCount > 0)
                    BasicAttackMove();

                
                if (attackCount == 0 &(Vector2.Distance(rb.position, playerRB.position) > attackDistance))
                    CalmMode();

            }

            Debug.Log(secondFaseJump);

            if (!secondFaseJump & (gameObject.GetComponent<SimarglScript>().HpNow <= ((gameObject.GetComponent<SimarglScript>().HpMax / 4) * 1)))
                secondFaseJump = true;

            if (secondFaseJump)
                SecondFaseJump();

            if (FireAttack & !waitAfterBlast)
            {
                FireBlastAttack(); 
            }
              
            
            
        }

    }

    [SerializeField] private GameObject fireBlast;
    private bool waitAfterBlast;
    private void FireBlastAttack()
    {
        if (blastSeries == 0 | blastSeries == 2 || blastSeries == 3)
        {
            GameObject blast = Instantiate(fireBlast, fireTarget1.transform.position, Quaternion.identity);
        }
        else
        { 
            GameObject blast = Instantiate(fireBlast, fireTarget2.transform.position, Quaternion.identity);
        }
        blastSeries++;
        waitAfterBlast = true;
    }
    

    private void CalmMode()
    {
        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * movement);
    }

   


    [Space]
    [Header("Урон")]
    [SerializeField] private float attackDamage;
    private bool waitAfterAttack = false;
    private bool waitAfterOneAttack = false;
    private void BasicAttack()
    {
        
        
        //не сломать!
        //Урон регается только в случае, если симаргл атакует влево и гг стоит слева от него на дистанции тычки
        if (attackVector == Vector2.left.normalized & (Vector2.Distance(player.transform.position, rb.position) <= attackDistance) & playerRB.transform.position.x <= transform.position.x)
            player.GetComponent<PlayerStats>().ReduceHp(attackDamage);

        //Либо симаргл атакует вправо и гг стоит справа от него на дистанции тычки
        else if (attackVector == Vector2.right.normalized & (Vector2.Distance(player.transform.position, rb.position) <= attackDistance) & playerRB.transform.position.x >= transform.position.x)
            player.GetComponent<PlayerStats>().ReduceHp(attackDamage);

        attackCount++;
        waitAfterOneAttack = true;
        if (attackCount == 3)
        {
            waitAfterAttack = true;
            attackCount = 0;
        }
    }

    public GameObject ActivePillar;
    private void BasicAttackMove()
    {
        //он продолжает двигаться в том направлении, куда двигался в начале атаки
        if (Vector2.Distance(rb.position, ActivePillar.transform.position) >= 5f)
            rb.MovePosition(rb.position + speed/2 * Time.fixedDeltaTime * attackVector);
        
    }


    [Space]
    [Header("точка для взлёта")]
    [SerializeField] private GameObject jumpTarget;

    [Space]
    [Header("точки стрельбы")]
    [SerializeField] private GameObject fireTarget1;
    [SerializeField] private GameObject fireTarget2;

    private bool FireAttack = false;
    private void SecondFaseJump()
    {
        Vector2 jumpVector = (jumpTarget.transform.position - transform.position).normalized;
        if (Vector2.Distance(rb.position, jumpTarget.transform.position) >= 5f)
            rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * jumpVector);
        else
        {
            
            FireAttack = true;
        }
    }





    [Space]
    [Header("кд атаки")]
    [SerializeField] private float afterAtackTime;
    private IEnumerator WaitAfterAttack()
    {
        while (true)
        {
            if (waitAfterAttack)
            {
                yield return new WaitForSeconds(3);
                waitAfterAttack = false;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator WaitAfterOneAttack()
    {
        while (true)
        {
            if (waitAfterOneAttack)
            {
                yield return new WaitForSeconds(afterAtackTime);
                waitAfterOneAttack = false;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator FireBlastCd()
    {
        while (true)
        {
            if (waitAfterBlast)
            {
                yield return new WaitForSeconds(3);
                waitAfterBlast = false;
            }

            yield return new WaitForFixedUpdate();
        }
        
    }
}
