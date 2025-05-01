using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.UI;

public class SimarglBehaivor : MonoBehaviour
{
    private Animator animator;
    GameObject player;
    Rigidbody2D rb;

    Rigidbody2D playerRB;
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        playerRB = player.GetComponent<Rigidbody2D>();
        StartCoroutine(WaitAfterAttack());
        StartCoroutine(WaitAfterOneAttack());
        StartCoroutine(FireBlastCd());
    }

    //������ � ������� ������
    private Vector2 movement;

    [Header("�������� ������������")]
    [SerializeField] private float speed;

    [Space]
    [Header("��������� �����")]
    [SerializeField] private float attackDistance;

    private int attackCount = 0;
    
    private Vector2 attackVector;
    private bool secondFaseJump = false;
    private float blastSeries = 0;
    private Vector3 playerPos;
    private void FixedUpdate() // flip x = false => �������� �����
    {
        playerPos = player.GetComponent<CapsuleCollider2D>().bounds.center;
        


        if (gameObject.GetComponent<SimarglScript>().IsActive)
        {
            

            //Debug.Log($"���� ����� {attackVector == Vector2.left.normalized}");
            //Debug.Log($"���� ������ {attackVector == Vector2.right.normalized}");
            //Debug.Log($"��� � ������ {attackCount == 0 & (Vector2.Distance(rb.position, playerPos) > attackDistance)}");
            //Debug.Log($"���-�� ���� {attackCount}");

            if (transform.position.x < LeftPillar.transform.position.x || transform.position.x > RightPillar.transform.position.x)
                attackCount = 0;

            //������ � ������� ������ 
            movement = (playerPos - transform.position).normalized;
            movement.y = 0;

            if (!secondFaseJump & !waitAfterAttack)
            {
                if ((Vector2.Distance(playerPos, rb.position) <= attackDistance & !waitAfterOneAttack) | (attackCount>0 & !waitAfterOneAttack))
                {
                    if (attackCount == 0)
                    {

                        if (!gameObject.GetComponent<SpriteRenderer>().flipX)
                            attackVector = Vector2.left.normalized;
                        else attackVector = Vector2.right.normalized;
                    }
                    animator.SetTrigger("IsAttack");

                }
                
                if (attackCount > 0)
                    BasicAttackMove();

                
                if (attackCount == 0 & 
                    (Vector2.Distance(rb.position, playerPos) > attackDistance | 
                    (gameObject.GetComponent<SpriteRenderer>().flipX == false & playerPos.x > transform.position.x) 
                    | ((gameObject.GetComponent<SpriteRenderer>().flipX == true & playerPos.x < transform.position.x))))
                    CalmMode();
                
                animator.SetBool("IsWalk", walk);
            }

            

            if (!secondFaseJump & (gameObject.GetComponent<SimarglScript>().HpNow <= ((gameObject.GetComponent<SimarglScript>().HpMax / 3) * 1)))
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
        walk = false;
        if (blastSeries == 0 | blastSeries == 3)
        {
            GameObject blast = Instantiate(fireBlast, fireTarget1.transform.position, Quaternion.identity);
        }
        else if (blastSeries == 2 | blastSeries == 8)
        {
            GameObject blast = Instantiate(fireBlast, fireTarget2.transform.position, Quaternion.identity);
        }
        else if (blastSeries == 1 | blastSeries == 7)
        {
            GameObject blast = Instantiate(fireBlast, fireTarget3.transform.position, Quaternion.identity);
        }
        else if (blastSeries == 6 | blastSeries == 10)
        {
            GameObject blast = Instantiate(fireBlast, fireTarget4.transform.position, Quaternion.identity);
        }
        else if (blastSeries == 5 | blastSeries == 11)
        {
            GameObject blast = Instantiate(fireBlast, fireTarget5.transform.position, Quaternion.identity);
        }
        else if (blastSeries == 4 | blastSeries == 9)
        {
            GameObject blast = Instantiate(fireBlast, fireTarget6.transform.position, Quaternion.identity);
        }
        blastSeries++;
        waitAfterBlast = true;
    }
    
    private bool walk = false;
    private void CalmMode()
    {
        walk = true;
        if (transform.position.x < playerPos.x)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        
        else gameObject.GetComponent<SpriteRenderer>().flipX = false;
        
        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * movement);
    }

   


    [Space]
    [Header("����")]
    [SerializeField] private float attackDamage;
    private bool waitAfterAttack = false;
    private bool waitAfterOneAttack = false;
    private void BasicAttack()
    {

        walk = false;
        //�� �������!
        //���� �������� ������ � ������, ���� ������� ������� ����� � �� ����� ����� �� ���� �� ��������� �����
        if (attackVector == Vector2.left.normalized & (Vector2.Distance(playerPos, rb.position) <= attackDistance) & playerRB.transform.position.x <= transform.position.x)
            player.GetComponent<PlayerStats>().ReduceHp(attackDamage);

        //���� ������� ������� ������ � �� ����� ������ �� ���� �� ��������� �����
        else if (attackVector == Vector2.right.normalized & (Vector2.Distance(playerPos, rb.position) <= attackDistance) & playerRB.transform.position.x >= transform.position.x)
            player.GetComponent<PlayerStats>().ReduceHp(attackDamage);

        attackCount++;
        waitAfterOneAttack = true;
        if (attackCount == 3)
        {
            waitAfterAttack = true;
            attackCount = 0;
        }
    }

    public GameObject LeftPillar;
    public GameObject RightPillar;

    [Space]
    [Header ("�������� ��� �����")]
    [SerializeField] private float speedInAttack;
    private void BasicAttackMove()
    {
        walk = false;
        Debug.Log("BasicAttackMove");
        //�� ���������� ��������� � ��� �����������, ���� �������� � ������ �����
        if ((attackVector == Vector2.left.normalized & rb.position.x - LeftPillar.transform.position.x >= 5f) | (attackVector == Vector2.right.normalized & RightPillar.transform.position.x - rb.position.x >= 5f))
        {
            Debug.Log("���� � �������");
            rb.MovePosition(rb.position + speedInAttack * Time.fixedDeltaTime * attackVector);
        }
    }


    [Space]
    [Header("����� ��� �����")]
    [SerializeField] private GameObject jumpTarget;

    [Space]
    [Header("����� ��������")]
    [SerializeField] private GameObject fireTarget1;
    [SerializeField] private GameObject fireTarget2;
    [SerializeField] private GameObject fireTarget3;
    [SerializeField] private GameObject fireTarget4;
    [SerializeField] private GameObject fireTarget5;
    [SerializeField] private GameObject fireTarget6;

    private bool FireAttack = false;
    private void SecondFaseJump()
    {
        walk = false;
        LeftPillar.SetActive(false);
        RightPillar.SetActive(false);

        Vector2 jumpVector = (jumpTarget.transform.position - transform.position).normalized;
        if (Vector2.Distance(rb.position, jumpTarget.transform.position) >= 5f)
            rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * jumpVector);
        else
        {
            
            FireAttack = true;
        }
    }

    [Space]
    [Header("�� �����")]
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

    private IEnumerator WaitAfterOneAttack()
    {
        while (true)
        {
            if (waitAfterOneAttack)
            {
                yield return new WaitForSeconds(5);
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
                yield return new WaitForSeconds(2);
                waitAfterBlast = false;
            }

            yield return new WaitForFixedUpdate();
        }
        
    }
}
