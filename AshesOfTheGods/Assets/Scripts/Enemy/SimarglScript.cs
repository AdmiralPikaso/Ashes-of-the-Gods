using System.Collections;
using UnityEngine;

public class SimarglScript : Enemy
{
    public bool IsActive { get; protected set; } = false;

    private GameObject player;

    private SpriteRenderer spriteRenderer;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();

        HpNow = hp;
        HpMax = hp;
    }

    private void Start()
    {
        StartCoroutine(damageble());
    }
    private bool getDamage = false;
    public override void TakeDamage(float damage)
    {

        HpNow -= damage;
        getDamage = true;
        if (HpNow <= 0)
        {
            healthBar.SetActive(false);
            Die();
        }
    }

    private void Destruction()
    {
        if (player.transform.position.x > transform.position.x)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        else gameObject.GetComponent<SpriteRenderer>().flipX = false;
        fireA.SetActive(false);
        fireB.SetActive(false);
        IsActive = false;
        Destroy(gameObject, 5f);
    }

    [Header("�����, ������� ��������� ����")]
    [SerializeField] private GameObject bossFightPoint;


    [Space]
    [Header("������� ������ ���� ����")]
    [SerializeField] private GameObject fireA;
    [SerializeField] private GameObject fireB;

    [Space]
    [Header("������� ������ ���� ����")]
    [SerializeField] private GameObject left1;
    [SerializeField] private GameObject right1;

    [Space]
    [Header("������� ������� ���� ����")]
    [SerializeField] private GameObject left2;
    [SerializeField] private GameObject right2;

    [Space]
    [Header("�������")]
    [SerializeField] private GameObject healthBar;
    private void Update()
    {

        if (!IsActive & bossFightPoint.transform.position.x < player.transform.position.x & !(HpNow <= 0))
        {
            IsActive = true;
            fireA.SetActive(true); 
            fireB.SetActive(true);
        }

        if (IsActive & !(HpNow <= 0))
        {
            healthBar.SetActive(true);
            fireA.SetActive(true);
            fireB.SetActive(true);

            fireA.GetComponent<FirePillar>().IsOn = true;
            fireB.GetComponent<FirePillar>().IsOn = true;



            if (HpNow <= (HpMax / 3) * 2)
                MovePillar(left1, right1);

            else if (HpNow <= (HpMax / 2) * 1)
                MovePillar(left2, right2);

            else if (HpNow <= (HpMax / 3) * 1)
                MovePillar(left1, right1);

            
        }
        
        
    }

    [Space]
    [Header("�������� �������")]
    [SerializeField] float pillarSpeed;

    private void MovePillar(GameObject leftPoint, GameObject rightPoint)
    {
        if (Vector2.Distance(fireA.transform.position, leftPoint.transform.position) >= 5f & Vector2.Distance(fireB.transform.position, rightPoint.transform.position) >= 5f)
        {
            Vector2 move1 = (leftPoint.transform.position - fireA.transform.position).normalized;
            fireA.GetComponent<Rigidbody2D>().MovePosition(fireA.GetComponent<Rigidbody2D>().position + Time.fixedDeltaTime * move1 * pillarSpeed);

            Vector2 move2 = (rightPoint.transform.position - fireB.transform.position).normalized;
            fireB.GetComponent<Rigidbody2D>().MovePosition(fireB.GetComponent<Rigidbody2D>().position + Time.fixedDeltaTime * move2 * pillarSpeed);
        }
    }


    private IEnumerator damageble()
    {
        while (true)
        {
            if (getDamage)
            {
                spriteRenderer.color = new Color(1, 0.47f, 0.47f, 1);
                yield return new WaitForSeconds(0.2f);
                spriteRenderer.color = Color.white;
                getDamage = false;
            }

            yield return new WaitForFixedUpdate();
        }
    }

}
