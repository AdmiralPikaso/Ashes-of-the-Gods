using UnityEngine;
using System.Collections;

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
    }

    //Вектор в сторону игрока
    private Vector2 movement;

    [Header("скорость передвижения")]
    [SerializeField] private float calmSpeed;

    [Space]
    [Header("дистанция атаки")]
    [SerializeField] private float attackDistance;

    private int attackCount = 0;

    private void FixedUpdate()
    {
        if (gameObject.GetComponent<SimarglScript>().IsActive)
        {


            //Вектор в сторону игрока 
            movement = (player.transform.position - transform.position).normalized;
            movement.y = 0;

            if (!waitAfterAttack)
            {
                if (Vector2.Distance(player.transform.position, rb.position) <= attackDistance & !waitAfterAttack)
                    Attack();
                else
                    CalmMode();

            }
        }

    }

    private void CalmMode()
    {
        if (Vector2.Distance(rb.position, playerRB.position) > attackDistance)
            rb.MovePosition(rb.position + calmSpeed * Time.fixedDeltaTime * movement);
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
