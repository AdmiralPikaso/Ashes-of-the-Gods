using UnityEngine;

public class PerunMelleHandScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [Header("����")]
    [SerializeField] private float damage;

    public bool Active { get; set; } = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {  
    
        if (collision.gameObject.CompareTag("Player") & Active)
        {
            player.GetComponent<PlayerStats>().ReduceHp(damage);
            
        }
    }
}
