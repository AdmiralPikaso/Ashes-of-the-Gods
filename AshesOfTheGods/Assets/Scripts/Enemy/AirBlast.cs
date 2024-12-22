using Unity.VisualScripting;
using UnityEngine;

public class AirBlast : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    [SerializeField] private float force;
    [SerializeField] private float damage;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.AddForce((Vector2)direction.normalized * force, ForceMode2D.Impulse);

        float rot = Mathf.Atan2(-direction.y, -direction.x)* Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }


    public bool Catch { get; private set; } = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!collision.gameObject.CompareTag("Enemy")) 
        {

            if (player.GetComponent<FirstSkill>().In_armor)
            {
                player.GetComponent<FirstSkill>().AttacksCount += 3;
            }
            else
            {
                Catch = true;
                player.GetComponent<PlayerStats>().ReduceHp(damage);
            }
            Destroy(gameObject);
        }
    }
 
}
