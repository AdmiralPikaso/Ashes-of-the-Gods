using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.UI;

public class SimarglBehaivor : MonoBehaviour
{
    [SerializeField] private AudioClip attackSimarglSound;
    private AudioSource attackSimarglAudioSource;
    [SerializeField] private float attackSimarglVolume;
    [SerializeField] private float minP;
    [SerializeField] private float maxP;
    [SerializeField] private AudioClip stepSound;
    [SerializeField] private float stepVolume;
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private float fireVolume;
    private Animator animator;
    GameObject player;
    Rigidbody2D rb;

    Rigidbody2D playerRB;
    void Start()
    {
        attackSimarglAudioSource = gameObject.AddComponent<AudioSource>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        playerRB = player.GetComponent<Rigidbody2D>();
        StartCoroutine(WaitAfterAttack());
        StartCoroutine(WaitAfterOneAttack());
        StartCoroutine(FireBlastCd());
    }

    private void PlayStepSound()
    {
        Sounds.Sound(stepSound, attackSimarglAudioSource, stepVolume, minP, maxP);
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
    private bool secondFase = false;
    
    private Vector3 playerPos;
    
    private bool walk = false;
    bool chooseMove = false;
    private GameObject chooseMovePillar;
    private void FixedUpdate() // flip x = false => �������� �����
    {
        if (waitAfterAttack)
            animator.SetBool("IsWalk", walk);

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

            if (!secondFase & !waitAfterAttack & !InAttack)
            {
                if ((Vector2.Distance(playerPos, rb.position) <= attackDistance & !waitAfterOneAttack) | (attackCount>0 & !waitAfterOneAttack))
                {
                    walk = false;
                    if (attackCount == 0)
                    {
                        if (!gameObject.GetComponent<SpriteRenderer>().flipX)
                            attackVector = Vector2.left.normalized;
                        else attackVector = Vector2.right.normalized;
                    }
                    animator.SetTrigger("IsAttack");
                }
                
                if (attackCount > 0 & !waitAfterAttack)
                    BasicAttackMove();

                if (attackCount == 0 & 
                    (Vector2.Distance(rb.position, playerPos) > attackDistance | 
                    (gameObject.GetComponent<SpriteRenderer>().flipX == false & playerPos.x > transform.position.x) 
                    | (gameObject.GetComponent<SpriteRenderer>().flipX == true & playerPos.x < transform.position.x)))
                    CalmMode();
                else if (waitAfterAttack)
                    walk = false;
                    
                animator.SetBool("IsWalk", walk);
            }

            if (!secondFase & (gameObject.GetComponent<SimarglScript>().HpNow <= ((gameObject.GetComponent<SimarglScript>().HpMax / 3) * 1)))
                secondFase = true;

            
            if (secondFase & !chooseMove & !FireAttack)
            {
                
                if (Vector2.Distance(LeftPillar.transform.position, transform.position) > Vector2.Distance(RightPillar.transform.position, transform.position))
                {
                    chooseMove = true;
                    chooseMovePillar = LeftPillar;
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    Debug.Log("��������� � ����� �������");
                    animator.SetBool("IsWalk", true);
                }
                else
                {
                    chooseMove = true;
                    chooseMovePillar = RightPillar;
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    Debug.Log("��������� � ������ �������");
                    animator.SetBool("IsWalk", true);
                }
            }

            if (chooseMove)
                SecondFaseMove(chooseMovePillar);

            if (FireAttack & !waitAfterBlast)
            {
                animator.SetBool("IsWalk", false);
                FireBlastAttack(); 
            }

        }
    }

    private bool InAttack = false;
    private void CanNotAttack()
    {
        InAttack = true;
    }
     private void CanAttack()
    {
        InAttack = false;
    }

    [SerializeField] private GameObject fireBlast;
    [Space]
    [Header("����� ��������")]
    [SerializeField] private GameObject fireTarget1;
    [SerializeField] private GameObject fireTarget2;
    private bool waitAfterBlast;
    private void FireBlastAttack()
    {
        Sounds.StaticSound(fireSound, attackSimarglAudioSource, fireVolume);
        walk = false;
        if (Vector2.Distance(transform.position, fireTarget1.transform.position) < Vector2.Distance(transform.position, fireTarget2.transform.position))
        {
            GameObject blast = Instantiate(fireBlast, fireTarget1.transform.position, Quaternion.identity);
        }
        else
        {
            GameObject blast = Instantiate(fireBlast, fireTarget2.transform.position, Quaternion.identity);
        }
        waitAfterBlast = true;
        FireAttack = false;
    }
    

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
        Sounds.Sound(attackSimarglSound, attackSimarglAudioSource, attackSimarglVolume, minP, maxP);
        walk = false;
        //�� �������!
        //���� �������� ������ � ������, ���� ������� ������� ����� � �� ����� ����� �� ���� �� ��������� �����
        if (attackVector == Vector2.left.normalized & (Vector2.Distance(playerPos, rb.position) <= attackDistance) & playerRB.transform.position.x <= transform.position.x)
        {
            player.GetComponent<PlayerStats>().ReduceHp(attackDamage);
            //player.GetComponent<FirstSkill>().AttacksCount += 5;
        }
        //���� ������� ������� ������ � �� ����� ������ �� ���� �� ��������� �����
        else if (attackVector == Vector2.right.normalized & (Vector2.Distance(playerPos, rb.position) <= attackDistance) & playerRB.transform.position.x >= transform.position.x)
        {
            player.GetComponent<PlayerStats>().ReduceHp(attackDamage);
            //player.GetComponent<FirstSkill>().AttacksCount += 5;
        }

        attackCount++;
        waitAfterOneAttack = true;
        if (attackCount == 3)
        {
            walk = false;
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

        //�� ���������� ��������� � ��� �����������, ���� �������� � ������ �����
        if ((attackVector == Vector2.left.normalized & rb.position.x - LeftPillar.transform.position.x >= 10f) | (attackVector == Vector2.right.normalized & RightPillar.transform.position.x - rb.position.x >= 10f))
        {
            walk = true;
            Debug.Log("���� � �������");
            rb.MovePosition(rb.position + speedInAttack * Time.fixedDeltaTime * attackVector);
        }
    }



    

    private bool FireAttack = false;
    
   
    
    private void SecondFaseMove(GameObject pillar)
    {
        walk = true;
        speed = 20;

        if (Vector2.Distance(rb.position, pillar.transform.position) >= 6f)
        {
            Debug.Log("Бежит");
            Debug.Log(Vector2.Distance(rb.position, pillar.transform.position));
            Vector2 moveVector = (pillar.transform.position - transform.position).normalized;
            moveVector.y = 0;
            rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * moveVector);
        }
        else
        {
            walk = false;
            FireAttack = true;
            chooseMove = false;
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
                yield return new WaitForSeconds(1);
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
