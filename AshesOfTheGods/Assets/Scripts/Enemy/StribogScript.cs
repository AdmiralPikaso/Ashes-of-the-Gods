using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using System.Collections;
using UnityEngine.UI;

public class StribogScript : MonoBehaviour
{

    GameObject player;
    Rigidbody2D rb;
    BoxCollider2D coll;
    CapsuleCollider2D playerColl;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(WaitMode());
        coll = GetComponent<BoxCollider2D>();
        playerColl = player.GetComponent<CapsuleCollider2D>();
    }



    void Update()
    {
        
    }

    [SerializeField] float attackDistance;
    public LayerMask layerMask;
    private bool calmMode = true;
    private int attackCount;
    private Vector2 movement;
    private bool waitAfterAttack = false;
    private bool jump = true;
    private bool activeFlag = false;
    [SerializeField] private Transform bossFightTarget;

    public Image im;
    [SerializeField] private Transform jumpPointA;
    [SerializeField] private Transform jumpPointB;
    private Vector2 jumpMove;
    private bool returnMode;
    [SerializeField] Transform returnA;
    [SerializeField] Transform returnB;
    [SerializeField] private float jumpSpeed;
    private void FixedUpdate()
    {
        if (jump == false)
            im.color = Color.red;
        else im.color = Color.green;
        if (player.transform.position.x > bossFightTarget.position.x & activeFlag == false)
        {
            jump = false;
            activeFlag = true;
        }

        

        if (activeFlag)
        {
            
            if (!jump & Vector2.Distance(gameObject.transform.position, jumpPointA.position) >= Vector2.Distance(gameObject.transform.position, jumpPointB.position))
            {
                jumpMove = (jumpPointA.position - transform.position).normalized;
            }
            else if (!jump)
            {
                jumpMove = (jumpPointB.position - transform.position).normalized;
            }

            

            if (jump)
                Jump();

            
            if (returnMode)
                ReturnMode();
            else returnMove = (new Vector2(rb.position.x, bossFightTarget.position.y) - rb.position).normalized;

            //Vector towards the enemy 
            movement = (player.transform.position - transform.position).normalized;
            movement.y = 0;
           
           
            if (calmMode & !waitAfterAttack & !jump & !returnMode)
                CalmMode();

            if (!jump & !returnMode & attackCount >= 2 & (Vector2.Distance(player.transform.position, rb.position) <= attackDistance))
            {
                Attack();
                attackCount = 0;
            }


        }
    }

    [SerializeField] private float speed;
    private bool afkAggression;
    Vector2 returnMove;

    [SerializeField] GameObject airBlast;
    private void SecondSkill()
    {
        GameObject blast = Instantiate(airBlast, transform.position, Quaternion.identity);
       // if (blast.GetComponent<AirBlast>().Catch)
         //   Dash();
        //else
        waitAfterAttack = true;

    }

    private void Dash()
    { 
        while (Vector2.Distance(player.transform.position, rb.position) > attackDistance)
            rb.MovePosition(rb.position + speed * 5 * Time.fixedDeltaTime * movement);
    }
    private void ReturnMode()
    {
       
        rb.MovePosition(rb.position + jumpSpeed * Time.fixedDeltaTime * returnMove);
        if (rb.position.y - bossFightTarget.position.y <= 0.001f)
        {
            returnMode = false;
            SecondSkill();
        }
    }
    private void CalmMode()
    {
        if (Vector2.Distance(player.transform.position, rb.position) > attackDistance)
        {
            rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * movement);
        }
        else afkAggression = true;
    }

    public void CountAttack()
    {
        attackCount++;
    }

    [SerializeField] private float attackDamage;
    private void Attack()
    {
            player.GetComponent<PlayerStats>().ReduceHp(attackDamage);
            waitAfterAttack = true;
    }

    
    private void Jump()
    {
        
        rb.MovePosition(rb.position + jumpSpeed * Time.fixedDeltaTime * jumpMove);
        if (transform.position.y >= jumpPointA.position.y)
        {
            jump = false;
            returnMode = true;
        }
    }


   


    [SerializeField] private float afkTime;
    [SerializeField] private float afterAtackTime;

    private IEnumerator WaitMode()
    {
        while (true)
        {
           

            if (afkAggression)
            {
                yield return new WaitForSeconds(afkTime);
                afkAggression = false;
                if (Vector2.Distance(player.transform.position, rb.position) <= attackDistance)
                    Attack();
            }

            if (waitAfterAttack)
            {
                yield return new WaitForSeconds(afterAtackTime);
                waitAfterAttack = false;
            }

            if (!jump & activeFlag)
            {
                yield return new WaitForSeconds(15);
                jump = true;

            }
            
            yield return new WaitForFixedUpdate();
        }

    }
}
