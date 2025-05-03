using System.Collections;
using UnityEngine;

public class FireBlast : MonoBehaviour
{
    public Transform target;  
    public float speed = 20f;
    [SerializeField] private float damage = 30.0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            rb.linearVelocity = direction * speed;
        }
        else
        {
            Debug.LogWarning("Target not set for HomingFireball!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PerunHand"))
            other.GetComponentInParent<Enemy>().TakeDamage(damage);
        if (target != null && other.gameObject == target.gameObject)
        {
            Destroy(gameObject);
        }
    }

    
}
