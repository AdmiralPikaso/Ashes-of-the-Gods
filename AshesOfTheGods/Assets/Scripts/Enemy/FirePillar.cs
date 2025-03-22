using UnityEngine;
using System.Collections;

public class FirePillar : MonoBehaviour
{
    private GameObject player;

    [Header("Урон")]
    [SerializeField] private float damage;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(WaitAfterAttack());
    }

    private void Update()
    {
        //Debug.Log($"wait {waitAfterAttack}");
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
