using UnityEngine;
using System.Collections;

public class Simargl : MonoBehaviour
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
    }

    //Vector towards the player
    private Vector2 movement;
    [Header("скорость передвижения в покое")]
    [SerializeField] private float calmSpeed;

    [Space]
    [Header("дистанция до агрессии")]
    [SerializeField] private float agressiveDistance;

    [Space]
    [Header("скорость в агрессии")]
    [SerializeField] private float agressiveSpeed;

    [Space]
    [Header("дистанция атаки")]
    [SerializeField] private float attackDistance;
    
    private int attackCount = 0;

    private void FixedUpdate()
    {
        //Vector towards the player 
        movement = (player.transform.position - transform.position).normalized;
        movement.y = 0;

        if (!waitAfterAttack)
        {
            if (Vector2.Distance(rb.position, playerRB.position) <= agressiveDistance)
                AgressiveMode();

            else CalmMode();
        }

       

        
            if (Vector2.Distance(player.transform.position, rb.position) <= attackDistance & !waitAfterAttack)
                Attack();
            

       

    }

    private void CalmMode()
    {
        if (Vector2.Distance(rb.position, playerRB.position) > attackDistance)
            rb.MovePosition(rb.position + calmSpeed * Time.fixedDeltaTime * movement);
    }

    private void AgressiveMode()
    {
        if (Vector2.Distance(rb.position, playerRB.position) > attackDistance)
            rb.MovePosition(rb.position + agressiveSpeed * Time.fixedDeltaTime * movement);
    }


    [Space]
    [Header("Урон")]
    [SerializeField] private float attackDamage;
    private bool waitAfterAttack = false;
    private void Attack()
    {
        if (Vector2.Distance(player.transform.position, rb.position) <= attackDistance)
            player.GetComponent<PlayerStats>().ReduceHp(attackDamage);

        attackCount++;

        if (attackCount == 3)
        {
            waitAfterAttack = true;
            attackCount = 0;
            
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
                yield return new WaitForSeconds(afterAtackTime);
                waitAfterAttack = false;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
