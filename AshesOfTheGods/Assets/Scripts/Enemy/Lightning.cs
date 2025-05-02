using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Lightning : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    [SerializeField] private float force;
    [SerializeField] private float damage;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb.AddForce(Vector2.down * force, ForceMode2D.Impulse);

        
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy") & !collision.gameObject.CompareTag("PerunHand") & !collision.gameObject.CompareTag("Perun"))
        {
            if (collision.gameObject.CompareTag("Player"))
                player.GetComponent<PlayerStats>().ReduceHp(damage);
            Destroy(gameObject);
        }

    }
}
