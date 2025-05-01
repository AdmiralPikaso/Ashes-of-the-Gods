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

    private void Destruction()
    {
        gameObject.GetComponent<SimarglScript>().IsActive = false;
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
    [SerializeField] private GameObject fireC;
    [SerializeField] private GameObject fireD;

    [Space]
    [Header("������� ������� ���� ����")]
    [SerializeField] private GameObject fireE;
    [SerializeField] private GameObject fireF;

    [Space]
    [Header("�������")]
    [SerializeField] private GameObject healthBar;
    private void Update()
    {
        
        if (!IsActive & bossFightPoint.transform.position.x < player.transform.position.x)
            IsActive = true;

        if (IsActive)
        {
            healthBar.SetActive(true);
            fireA.SetActive(true);
            fireB.SetActive(true);

            gameObject.GetComponent<SimarglBehaivor>().LeftPillar = fireA;
            gameObject.GetComponent<SimarglBehaivor>().RightPillar = fireB;

            fireA.GetComponent<FirePillar>().IsOn = true;
            fireB.GetComponent<FirePillar>().IsOn = true;

        }
        
        if (HpNow <= (HpMax/3)*2 )
        {
            fireA.SetActive(false);
            fireB.SetActive(false);

            gameObject.GetComponent<SimarglBehaivor>().LeftPillar = fireC;
            gameObject.GetComponent<SimarglBehaivor>().RightPillar = fireD;

            fireC.SetActive(true);
            fireD.SetActive(true);
            
            fireC.GetComponent<FirePillar>().IsOn = true;
            fireD.GetComponent<FirePillar>().IsOn = true;
        }

        if (HpNow <= (HpMax/2)*1)
        {
            fireC.SetActive(false);
            fireD.SetActive(false);

            gameObject.GetComponent<SimarglBehaivor>().LeftPillar = fireE;
            gameObject.GetComponent<SimarglBehaivor>().RightPillar = fireB;

            fireE.SetActive(true);
            fireF.SetActive(true);

            fireE.GetComponent<FirePillar>().IsOn = true;
            fireF.GetComponent<FirePillar>().IsOn = true;

        }
            
          
    }

    

    
   
   
   
}
