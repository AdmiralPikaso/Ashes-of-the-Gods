using UnityEngine;

public class SimarglScript : Enemy
{
    private void Awake()
    {
        
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
}
