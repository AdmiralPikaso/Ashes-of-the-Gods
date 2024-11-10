using UnityEngine;
using System.Collections;
public class Archer:MonoBehaviour
{

    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(WaitMode());
    }

   
    private void Update()
    {

       // Shoot();
           
    }

    private bool shootWaitMode = false;
    [SerializeField] float distance;
    private void FixedUpdate()
    {
        Vector2 directionLeft = new Vector2(player.transform.position.x, player.transform.position.y) - new Vector2(transform.position.x - transform.localScale.x / 2, transform.position.y);
        RaycastHit2D enemyVisionLeft = Physics2D.Raycast(new Vector2(transform.position.x - transform.localScale.x / 2, transform.position.y), directionLeft);
        Debug.DrawRay(new Vector2(transform.position.x-transform.localScale.x/2, transform.position.y), directionLeft * distance, Color.green);
        
        Vector2 directionRight = new Vector2(player.transform.position.x, player.transform.position.y) - new Vector2(transform.position.x + transform.localScale.x / 2, transform.position.y);
        RaycastHit2D enemyVisionRight = Physics2D.Raycast(new Vector2(transform.position.x + transform.localScale.x / 2, transform.position.y), directionRight);
        Debug.DrawRay(new Vector2(transform.position.x + transform.localScale.x / 2, transform.position.y), directionRight * distance, Color.green);
        if ((enemyVisionLeft.collider != null && enemyVisionLeft.collider.gameObject.CompareTag("Player")) | (enemyVisionRight.collider != null && enemyVisionRight.collider.gameObject.CompareTag("Player")))
        {
                Shoot();
        }
        
    }

    [SerializeField] private GameObject arrow;
    private void Shoot()
    {
        if (!shootWaitMode)
        {
            Instantiate(arrow, transform.position, Quaternion.identity);
            shootWaitMode = true;
        }

    }

        [SerializeField] private float atackCD;
    private IEnumerator WaitMode()
    {
        while (true)
        {
            if (shootWaitMode)
            {
                print(" д выстрела");
                yield return new WaitForSeconds(atackCD);
                shootWaitMode = false;
            }
            yield return new WaitForFixedUpdate();


        }
    }
}
