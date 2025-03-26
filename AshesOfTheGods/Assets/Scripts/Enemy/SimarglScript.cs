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
    [SerializeField] private GameObject fireC;
    [SerializeField] private GameObject fireD;

    [Space]
    [Header("Границы третьей зоны огня")]
    [SerializeField] private GameObject fireE;
    [SerializeField] private GameObject fireF;


    private void Update()
    {

        if (!IsActive & bossFightPoint.transform.position.x < player.transform.position.x)
            IsActive = true;

        if (IsActive)
        {
            fireA.SetActive(true);
            fireB.SetActive(true);

            gameObject.GetComponent<SimarglBehaivor>().ActivePillar = fireA;
            
            fireA.GetComponent<FirePillar>().IsOn = true;
            fireB.GetComponent<FirePillar>().IsOn = true;

        }
        
        if (HpNow <= (HpMax/3)*2 )
        {
            fireA.SetActive(false);
            fireB.SetActive(false);

            gameObject.GetComponent<SimarglBehaivor>().ActivePillar = fireC;

            fireC.SetActive(true);
            fireD.SetActive(true);
            
            fireC.GetComponent<FirePillar>().IsOn = true;
            fireD.GetComponent<FirePillar>().IsOn = true;
        }

        if (HpNow <= (HpMax/3)*1)
        {
            fireC.SetActive(false);
            fireD.SetActive(false);

            gameObject.GetComponent<SimarglBehaivor>().ActivePillar = fireE;
            
            fireE.SetActive(true);
            fireF.SetActive(true);

            fireE.GetComponent<FirePillar>().IsOn = true;
            fireF.GetComponent<FirePillar>().IsOn = true;

        }
            
          
    }

    

    
   
   
   
}
