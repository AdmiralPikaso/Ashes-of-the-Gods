using UnityEngine;

public class SimarglScript : Enemy
{
    public bool IsActive { get; protected set; } = false;


    private GameObject player;
    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        HpNow = hp;
        HpMax = hp;
    }

    public override void TakeDamage(float damage)
    {

        HpNow -= damage;

        if (HpNow <= 0)
        {
            healthBar.SetActive(false);
            Die();
        }


    }

    [Header("Точка, которая запускает файт")]
    [SerializeField] private GameObject bossFightPoint;


    [Space]
    [Header("Границы первой зоны огня")]
    [SerializeField] private GameObject fireA;
    [SerializeField] private GameObject fireB;

    [Space]
    [Header("Границы второй зоны огня")]
    [SerializeField] private GameObject left1;
    [SerializeField] private GameObject right1;

    [Space]
    [Header("Границы третьей зоны огня")]
    [SerializeField] private GameObject left2;
    [SerializeField] private GameObject right2;

    [Space]
    [Header("хелсбар")]
    [SerializeField] private GameObject healthBar;
    private void Update()
    {

        if (!IsActive & bossFightPoint.transform.position.x < player.transform.position.x)
        {
            IsActive = true;
            fireA.SetActive(true); 
            fireB.SetActive(true);
        }

        if (IsActive)
        {
            healthBar.SetActive(true);
            fireA.SetActive(true);
            fireB.SetActive(true);

            fireA.GetComponent<FirePillar>().IsOn = true;
            fireB.GetComponent<FirePillar>().IsOn = true;

        }
        
        if (HpNow <= (HpMax/3)*2)
            MovePillar(left1,right1);   
        
        else if (HpNow <= (HpMax/2)*1)
            MovePillar(left2, right2);
        
        else if (HpNow <= (HpMax / 3) * 1)
            MovePillar(left1, right1);

    }

    [Space]
    [Header("Скорость столбов")]
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
    
   
   
   
}
