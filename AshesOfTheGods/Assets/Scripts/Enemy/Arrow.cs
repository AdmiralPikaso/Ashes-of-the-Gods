using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
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

        Vector3 direction = player.GetComponent<CapsuleCollider2D>().bounds.center - transform.position;
        rb.AddForce((Vector2)direction.normalized * force, ForceMode2D.Impulse);

        float rot = Mathf.Atan2(-direction.y, -direction.x)* Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy")) 
        {
            if (collision.gameObject.CompareTag("Player"))
                player.GetComponent<PlayerStats>().ReduceHp(damage);
            Destroy(gameObject);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
