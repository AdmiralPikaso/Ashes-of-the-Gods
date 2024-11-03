using UnityEngine;

public class EnemyTop : MonoBehaviour
{
    [SerializeField] private float pushForce = 10f;
    [SerializeField] private float upwardPushForce = 5f;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            print("Контакт!");
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Vector2 pushDirection = Vector2.up * upwardPushForce; 
                if (other.transform.position.x < transform.position.x)
                {
                    pushDirection += Vector2.left * pushForce; // толкаем влево
                }
                else
                {
                    pushDirection += Vector2.right * pushForce; // толкаем вправо
                }
                playerRb.AddForce(pushDirection, ForceMode2D.Impulse);
            }
        }

    }
}
