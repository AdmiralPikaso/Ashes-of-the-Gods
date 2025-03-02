using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using System.Collections;
using UnityEngine.UI;

public class StribogScript : MonoBehaviour
{

    GameObject player;
    Rigidbody2D rb;
   
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(WaitBeforeAttack());
        StartCoroutine(WaitAfterAttack());
        StartCoroutine(WaitJump());
        StartCoroutine(WaitStun());
        StartCoroutine(AirBlastCd());
    }

    [SerializeField] float attackDistance;
    private int attackCount;
    private Vector2 movement;
    private bool waitAfterAttack = false;
    private bool jump = true;
    private bool activeFlag = false;
    [SerializeField] private Transform bossFightTarget;

   
    [SerializeField] private Transform jumpPointA;
    [SerializeField] private Transform jumpPointB;
    private Vector2 jumpMove;
    private bool returnMode;
    [SerializeField] Transform returnA;
    [SerializeField] Transform returnB;
    [SerializeField] private float jumpSpeed;
    private bool dashMode = false;
    public bool Catch { get; set; } = false;

    private bool secondFase = false;
    private bool secondFaseSkill = false;
    private Vector2 SecondFaseSkillMove;
    private bool waitBeforeAttack = false;
    private bool waitwaitBeforeAttack = false;
    private void FixedUpdate()
    {
        if (!player.GetComponent<PlayerStats>().IsDead)
        {
            if (player.transform.position.x > bossFightTarget.position.x & activeFlag == false)
            {
                jump = false;
                activeFlag = true;
            }

            if (activeFlag & !stun)
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
                SecondFaseSkillMove = (player.transform.position - transform.position).normalized;


                if (!waitAfterAttack & !jump & !returnMode & !dashMode)
                    CalmMode();


                if (attackCount >= 2 & !waitwaitBeforeAttack & (Vector2.Distance(player.transform.position, rb.position) <= attackDistance))
                {

                    waitwaitBeforeAttack = true;
                    waitBeforeAttack = true;

                }

                if (attackCount >= 2 & !waitBeforeAttack)
                {
                    if (Vector2.Distance(player.transform.position, rb.position) <= attackDistance)
                        Attack();
                    else
                    {
                        waitwaitBeforeAttack = false;
                        attackCount = 0;
                    }

                }

                if (Catch)
                {
                    airBlastCount = 3;
                    inAirBlast = false;
                    dashMode = true;
                }
                if (dashMode)
                    Dash();

                if (gameObject.GetComponent<NewStribog>().HpNow <= gameObject.GetComponent<NewStribog>().HpMax / 3)
                    secondFase = true;

                if (secondFaseSkill)
                {
                    SecondFaseSkill();
                }

            }
        }
    }

    [SerializeField] private float speed;
    Vector2 returnMove;
    [SerializeField] GameObject airBlast;
    [SerializeField] private float secondFaseSkillDamage;
    private float secondFaseAttackCount = 0;
    private bool stun = false;
    private bool wasInArmor = false;
    private void SecondFaseSkill()
    {
        if (!returnMode)
            rb.MovePosition(rb.position + jumpSpeed * Time.fixedDeltaTime * SecondFaseSkillMove);
        if (Vector2.Distance(player.transform.position, rb.position) <= 5f)
        {
            if (secondFaseAttackCount == 0)
            {
                if (player.GetComponent<FirstSkill>().In_armor)
                {
                    player.GetComponent<FirstSkill>().AttacksCount += 3;
                    wasInArmor = true;
                }
                else player.GetComponent<PlayerStats>().ReduceHp(secondFaseSkillDamage);
                secondFaseAttackCount++;
            }
                returnMode = true;
      
        }
    }
    
    private void AirBlastSkill()
    {
        GameObject blast = Instantiate(airBlast, transform.position, Quaternion.identity);
        AirBlastSeries = false;
        airBlastCount++;
    }

    private void Dash()
    {
        if (Vector2.Distance(player.transform.position, rb.position) > attackDistance)
            rb.MovePosition(rb.position + speed * 10 * Time.fixedDeltaTime * movement);
        else
        {
            dashMode = false;
            Catch = false;
        }
    }
    private void ReturnMode()
    {
       
        rb.MovePosition(rb.position + jumpSpeed * Time.fixedDeltaTime * returnMove);
        if (!secondFaseSkill & rb.position.y - bossFightTarget.position.y <= 0.01f)
        {
            airBlastCount = 0;
            returnMode = false;
            inAirBlast = true;
            AirBlastSkill();
            
        }
        else if (rb.position.y - bossFightTarget.position.y <= 0.01f)
        {
            returnMode = false;
            secondFaseSkill = false;
            secondFaseAttackCount = 0;
            if (wasInArmor)
            {
                stun = true;
                wasInArmor = false;
            }
        }
        
    }

    int airBlastCount = 3;
    bool AirBlastSeries = true;
    bool inAirBlast = false;
    [SerializeField] private Image im1;
    [SerializeField] private Image im2;
    private void CalmMode()
    {

        if (AirBlastSeries & airBlastCount < 3)
        {
            AirBlastSkill();
            
        }
        if (airBlastCount == 3)
            inAirBlast = false;
        if (!inAirBlast &Vector2.Distance(player.transform.position, rb.position) > attackDistance)
        {
            
            rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * movement);
        }
        
        
    }

    public void CountAttack()
    {
        attackCount++;
    }

    [SerializeField] private float attackDamage;
    
    private void Attack()
    {
        player.GetComponent<PlayerStats>().ReduceHp(attackDamage);
        attackCount = 0;
        waitAfterAttack = true;
        waitwaitBeforeAttack = false;
    }

    
    private void Jump()
    {
        
        rb.MovePosition(rb.position + jumpSpeed * Time.fixedDeltaTime * jumpMove);
        if (transform.position.y >= jumpPointA.position.y)
        {
            jump = false;
            if (secondFase)
                secondFaseSkill = true;
            else
                returnMode = true;
        }
    }


   


    [SerializeField] private float afterAtackTime;
    [SerializeField] private float stunTime;
    [SerializeField] private float beforeAtackTime;

    private IEnumerator WaitBeforeAttack()
    {
        while (true)
        {
            if (waitBeforeAttack)
        {

            yield return new WaitForSeconds(beforeAtackTime);
            waitBeforeAttack = false;

        }
        yield return new WaitForFixedUpdate();
    }
}
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
        

        yield return new WaitForFixedUpdate();
    }
}

    [SerializeField] private float jumpCD;
    [SerializeField] private float jumpSecondCD;
    
    private IEnumerator WaitJump()
    {
        while (true)
        {
            if (!jump & activeFlag)
            {
                yield return new WaitForSeconds(jumpCD);
                jump = true;
                jumpCD = jumpSecondCD;

            }
        yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator WaitStun()
    {
        while (true)
        {
            if (stun)
            {
                yield return new WaitForSeconds(stunTime);
                stun = false;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator AirBlastCd()
    {
        while (true)
        {
            if (!AirBlastSeries)
            {
                yield return new WaitForSeconds(3);
                AirBlastSeries = true;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
