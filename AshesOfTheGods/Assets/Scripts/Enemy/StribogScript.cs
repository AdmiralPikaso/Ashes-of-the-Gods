using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using NUnit.Framework.Constraints;

public class StribogScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    GameObject player;
    Rigidbody2D rb;
    [SerializeField] protected AudioClip wingFlap;
    public AudioSource audioSource;
    [SerializeField] protected float minPitch;
    [SerializeField] protected float maxPitch;
    [SerializeField] protected float volume;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();  
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        //StartCoroutine(WaitBeforeAttack());
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

    [SerializeField] private float jumpSpeed;
    private bool dashMode = false;
    public bool Catch { get; set; } = false;

    private bool secondFase = false;
    private bool secondFaseSkill = false;
    private Vector2 SecondFaseSkillMove;
    //private bool waitBeforeAttack = false;
    //private bool waitwaitBeforeAttack = false;
    private bool deathFlag = false;

    [SerializeField] GameObject healthBar;
    private void FixedUpdate()
    {
        if (activeFlag)
        {
            healthBar.SetActive(true);
        }
        if (movement.x > 0 && !spriteRenderer.flipX)
            spriteRenderer.flipX = true;
        else if (movement.x < 0 && spriteRenderer.flipX)
            spriteRenderer.flipX = false;
        if (player.GetComponent<PlayerStats>().HpNow <= 0)
            deathFlag = true;
        if (player.GetComponent<CapsuleCollider2D>().bounds.center.x > bossFightTarget.position.x & activeFlag == false & !deathFlag)
        {
            jump = false;
            activeFlag = true;
        }

        if (activeFlag & !stun & !deathFlag)
        {

            if (!jump & Vector2.Distance(gameObject.transform.position, jumpPointA.position) >= Vector2.Distance(gameObject.transform.position, jumpPointB.position))
            {
                jumpMove = (jumpPointA.position - transform.position).normalized;
            }
            else if (!jump)
            {
                jumpMove = (jumpPointB.position - transform.position).normalized;
            }

            if (jump & !inAttackAnim)
                Jump();


            if (returnMode)
                ReturnMode();
            else returnMove = (new Vector2(rb.position.x, bossFightTarget.position.y) - rb.position).normalized;



            //Vector towards the enemy 
            movement = (player.GetComponent<CapsuleCollider2D>().bounds.center - transform.position).normalized;
            movement.y = 0;
            SecondFaseSkillMove = (player.GetComponent<CapsuleCollider2D>().bounds.center - transform.position).normalized;




            /*if (attackCount >= 2 & !waitwaitBeforeAttack & (Vector2.Distance(player.GetComponent<CapsuleCollider2D>().bounds.center, rb.position) <= attackDistance))
            {
                
                waitwaitBeforeAttack = true;
                waitBeforeAttack = true;
                
            }
            */
            if (attackCount >= 2 /*& !waitBeforeAttack*/)
            {
                if (Vector2.Distance(player.GetComponent<CapsuleCollider2D>().bounds.center, rb.position) <= attackDistance)
                {
                    attackCount = 0;
                    animator.SetTrigger("Attack");
                }
                else
                {
                    //waitwaitBeforeAttack = false;
                    attackCount = 0;
                }

            }
            else if (!inAttackAnim & !waitAfterAttack & !jump & !returnMode & !dashMode)
                CalmMode();


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

            if (secondFaseSkill & !returnMode)
            {
                animator.SetBool("Fly", true);
                rb.MovePosition(rb.position + jumpSpeed * Time.fixedDeltaTime * SecondFaseSkillMove);
                //SecondFaseSkill();
            }
            /*if (!gameObject.GetComponent<Enemy>().isDead)
            {
            }*/
        }
    }

    [SerializeField] private float speed;
    Vector2 returnMove;
    [SerializeField] GameObject airBlast;
    [SerializeField] private float secondFaseSkillDamage;
    private float secondFaseAttackCount = 0;
    private bool stun = false;
    private bool wasInArmor = false;

    /*private void SecondFaseSkill()
    {
    animator.SetBool("Fly", true);
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
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (secondFaseSkill && collision.gameObject.CompareTag("Player") & !returnMode)
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
        if (Vector2.Distance(player.GetComponent<CapsuleCollider2D>().bounds.center, rb.position) > attackDistance)
            rb.MovePosition(rb.position + speed * 10 * Time.fixedDeltaTime * movement);
        else
        {
            dashMode = false;
            Catch = false;
        }
    }
    private void ReturnMode()
    {
        animator.SetBool("Fly", false);
        rb.MovePosition(rb.position + jumpSpeed * Time.fixedDeltaTime * returnMove);
        if (!secondFaseSkill & rb.position.y - bossFightTarget.position.y <= 0.1f & !inRangeAttackAnim)
        {
            airBlastCount = 0;
            returnMode = false;
            inAirBlast = true;
            animator.SetTrigger("RangeAttack");
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
    private void CalmMode()
    {

        if (AirBlastSeries & airBlastCount < 3 & !inRangeAttackAnim)
        {
            animator.SetTrigger("RangeAttack");

        }
        if (airBlastCount == 3)
            inAirBlast = false;
        if (!inAirBlast & Vector2.Distance(player.GetComponent<CapsuleCollider2D>().bounds.center, rb.position) > attackDistance)
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
        waitAfterAttack = true;
        //waitwaitBeforeAttack = false;
        if (Vector2.Distance(player.GetComponent<CapsuleCollider2D>().bounds.center, rb.position) <= attackDistance)
        {
            player.GetComponent<PlayerStats>().ReduceHp(attackDamage);
            player.GetComponent<FirstSkill>().AttacksCount += 3;
        }
    }

    private bool inAttackAnim = false;
    private bool inRangeAttackAnim = false;
    private void InAnim()
    {
        inAttackAnim = true;
    }

    private void NotInAnim()
    {
        inAttackAnim = false;
    }

    private void InRangeAttackAnim()
    {
        inRangeAttackAnim = true;
    }

    private void NotInRangeAttackAnim()
    {
        inRangeAttackAnim = false;
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


    private void PlayWingFlapSound()
    {
        Sounds.Sound(wingFlap, audioSource, volume, minPitch, maxPitch);
    }


    [SerializeField] private float afterAtackTime;
    [SerializeField] private float stunTime;
    [SerializeField] private float beforeAtackTime;

    /*private IEnumerator WaitBeforeAttack()
    {
        while (true)
        {
            if (waitBeforeAttack)
        {

            yield return new WaitForSeconds(beforeAtackTime);
            waitBeforeAttack = false;

        }
        yield return new WaitForFixedUpdate();
    }*/

    private IEnumerator WaitAfterAttack()
    {
        while (true)
        {
            if (waitAfterAttack)
            {
                yield return new WaitForSeconds(afterAtackTime + 1);
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
        private void Death()
        {
            activeFlag = false;
            deathFlag = true;
            //gameObject.SetActive(false);
            healthBar.SetActive(false);
            Destroy(gameObject, 5f);
        }
}

