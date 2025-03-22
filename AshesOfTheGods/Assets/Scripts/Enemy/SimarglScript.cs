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
    private void Update()
    {
        if (!IsActive & bossFightPoint.transform.position.x < player.transform.position.x)
            IsActive = true;

        if (IsActive)
            FirePhases(); 
          
    }

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

    [Space]
    [Header("Время появления столба")]
    [SerializeField] private float appearTime;
    private float t = 0;
    private bool flag = false;
    private void FirePhases()
    {
        if (!flag)
        {


            if (HpNow > HpMax * 2 / 3)
            {
                flag = false;
                fireA.SetActive(true);
                fireB.SetActive(true);

                fireA.GetComponent<SpriteRenderer>().color = new Color(fireA.GetComponent<SpriteRenderer>().color.r,
                                                                       fireA.GetComponent<SpriteRenderer>().color.g,
                                                                       fireA.GetComponent<SpriteRenderer>().color.b,
                                                                       0);

                fireB.GetComponent<SpriteRenderer>().color = new Color(fireB.GetComponent<SpriteRenderer>().color.r,
                                                                       fireB.GetComponent<SpriteRenderer>().color.g,
                                                                       fireB.GetComponent<SpriteRenderer>().color.b,
                                                                       0);

                while (t != appearTime)
                {
                    var need = Time.deltaTime / appearTime;
                    t += need;
                    fireA.GetComponent<SpriteRenderer>().color = new Color(fireA.GetComponent<SpriteRenderer>().color.r,
                                                                       fireA.GetComponent<SpriteRenderer>().color.g,
                                                                       fireA.GetComponent<SpriteRenderer>().color.b,
                                                                       t / appearTime);

                    fireB.GetComponent<SpriteRenderer>().color = new Color(fireB.GetComponent<SpriteRenderer>().color.r,
                                                                       fireB.GetComponent<SpriteRenderer>().color.g,
                                                                       fireB.GetComponent<SpriteRenderer>().color.b,
                                                                       t / appearTime);
                    flag = true;
                }
            }

            else if ((HpNow < HpMax * 2 / 3) & (HpNow > HpMax * 1 / 3))
            {
                flag = false;

                fireA.SetActive(false);
                fireB.SetActive(false);

                fireC.SetActive(true);
                fireD.SetActive(true);
            }

            else if (HpNow < HpMax * 1 / 3)
            {
                flag = false;

                fireC.SetActive(false);
                fireD.SetActive(false);

                fireE.SetActive(true);
                fireF.SetActive(true);
            }
        }
         
    }
}
