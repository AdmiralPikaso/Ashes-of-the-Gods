using UnityEngine; 

public class PlayerRegularAttack : Player 
{ 
    [SerializeField] public Transform attackPoint; 
    [SerializeField] public LayerMask damageableLayerMask; 
    [SerializeField] public float damage; 
    [SerializeField] public float attackRange; 
    [SerializeField] public float timeBtwAttack; 

    private float timer; 

    private void Update() 
    { 
        Attack(); 
    } 

    private void OnDrawGizmosSelected() 
    { 
        Gizmos.color = Color.red; 
        Gizmos.DrawWireSphere(attackPoint.position, attackRange); 
    } 

    private void Attack() 
    { 
        if (timer <= 0) 
        { 
            if (Input.GetAxis("Fire1") != 0) 
            { 
                print("Нажато"); 

                // Проверяем ближайшего врага
                Collider2D closestEnemy = Physics2D.OverlapCircle(attackPoint.position, attackRange, damageableLayerMask);
                
                if (closestEnemy != null) 
                { 
                    closestEnemy.GetComponent<Enemy>().TakeDamage(damage); 
                    print("Урон нанесён"); 
                } 

                timer = timeBtwAttack; 
            } 
        } 
        else 
        { 
            timer -= Time.deltaTime; 
        } 
    } 
}