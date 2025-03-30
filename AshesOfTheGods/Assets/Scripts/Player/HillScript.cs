using UnityEngine;

public class HillScript : MonoBehaviour
{
    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [SerializeField] private float heal;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
            if (collision.gameObject.CompareTag("Player"))
            {
                player.GetComponent<PlayerStats>().Heal(heal);
                Destroy(gameObject);
            }

    }
}
