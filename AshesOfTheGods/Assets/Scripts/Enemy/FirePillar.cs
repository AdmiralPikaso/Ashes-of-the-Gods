using UnityEngine;
using System.Collections;

public class FirePillar : MonoBehaviour
{
    private GameObject player;

    [Header("Урон")]
    [SerializeField] private float damage;


    private SpriteRenderer renderer;
    void Start()
    {

        renderer = GetComponent<SpriteRenderer>();
        
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(WaitAfterAttack());
    }

    public bool IsOn = false;
    [SerializeField] private float appearTime;
    private bool fire = false;
    private float currentAlpha = 0f;
    private float fadeTimer = 0f;

    private void FixedUpdate()
    {
        if (IsOn && !fire)
        {
            fadeTimer += Time.fixedDeltaTime;
            currentAlpha = Mathf.Clamp01(fadeTimer / appearTime);

            Color color = renderer.color;
            color.a = currentAlpha;
            renderer.color = color;

            if (currentAlpha >= 1f)
            {
                fire = true;
            }
        }
    }

    private bool waitAfterAttack = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") & !waitAfterAttack)
        {
            player.GetComponent<PlayerStats>().ReduceHp(damage);
            waitAfterAttack = true;
        }
    }

    [Space]
    [Header("кд урона")]
    [SerializeField] private float fireCD;
    private IEnumerator WaitAfterAttack()
    {

        while (true)
        {
            if (waitAfterAttack)
            {
                yield return new WaitForSeconds(fireCD);
                waitAfterAttack = false;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    
}
