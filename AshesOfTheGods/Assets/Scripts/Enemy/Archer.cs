using UnityEngine;
using System.Collections;
public class Archer:MonoBehaviour
{

    private GameObject player;
    [SerializeField] private Transform firstGuardedPoint;
    [SerializeField] private Transform secondGuardedPoint;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (firstGuardedPoint.transform.position.x > secondGuardedPoint.transform.position.x)
        {
            Transform transform = firstGuardedPoint;
            firstGuardedPoint = secondGuardedPoint;
            secondGuardedPoint = transform;
        }
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(WaitMode());
    }

    private bool shootWaitMode = false;
    [SerializeField] float distance;
    private bool guardMode = true;
    private bool shootMode = false;
    private Vector2 movement;
    private bool returnWaitMode = false;
    private void FixedUpdate()
    {
        if (!shootMode & !returnWaitMode & !guardMode)
        {
            guardMode = true;
        }

        print($"{guardMode}{shootMode}{returnWaitMode}");
        //Vector towards the enemy 
        movement = (player.transform.position - transform.position).normalized;
        movement.y = 0;

        //Archer vision raycast system
        Vector2 directionLeft = new Vector2(player.transform.position.x, player.transform.position.y) - new Vector2(transform.position.x - transform.localScale.x / 2, transform.position.y);
        RaycastHit2D enemyVisionLeft = Physics2D.Raycast(new Vector2(transform.position.x - transform.localScale.x / 2, transform.position.y), directionLeft);
        Debug.DrawLine(transform.position, new Vector2(transform.position.x - distance, transform.position.y), Color.red);
        
        Vector2 directionRight = new Vector2(player.transform.position.x, player.transform.position.y) - new Vector2(transform.position.x + transform.localScale.x / 2, transform.position.y);
        RaycastHit2D enemyVisionRight = Physics2D.Raycast(new Vector2(transform.position.x + transform.localScale.x / 2, transform.position.y), directionRight);
        Debug.DrawLine(transform.position, new Vector2(transform.position.x + distance, transform.position.y), Color.red);
        //


        if ((enemyVisionLeft.collider != null && enemyVisionLeft.collider.gameObject.CompareTag("Player")) | (enemyVisionRight.collider != null && enemyVisionRight.collider.gameObject.CompareTag("Player")))
        {
            print("¬идит");
            if (Vector2.Distance(player.transform.position, transform.position) <= distance) //exit from guard, entrance to angry
            {

                guardMode = false;
                shootMode = true;


            }
            else if (!shootWaitMode & shootMode)
            {
                shootMode = false;
                guardMode = false;
                returnWaitMode = true;
            }
            

        }
        else if (!shootWaitMode & shootMode)
        {
            print("Ќе видит");
            shootMode = false;
            guardMode = false;
            returnWaitMode = true;

        }

        if (guardMode)
            GuardMode();
        
        if (shootMode)
            ShootMode();
        
    }

    private bool guardModeRightMove = false;
    private bool guardWaitMode = false;
    [SerializeField] private float speed;
    
    private void GuardMode()
    {
        
            if (guardModeRightMove & !guardWaitMode)
                rb.MovePosition(rb.position + Vector2.right * speed * Time.fixedDeltaTime);

            if (!guardModeRightMove & !guardWaitMode)
                rb.MovePosition(rb.position + Vector2.left * speed * Time.fixedDeltaTime);

            if (rb.position.x + transform.localScale.x >= secondGuardedPoint.position.x & guardModeRightMove)
            {
                guardWaitMode = true;
                guardModeRightMove = false;
            }
            else if (rb.position.x - transform.localScale.x <= firstGuardedPoint.position.x & !guardModeRightMove)
            {
                guardWaitMode = true;
                guardModeRightMove = true;
            }
        
    }
  

    [SerializeField] private GameObject arrow;
    private void ShootMode()
    {
        
        if (!shootWaitMode)
        {
            Instantiate(arrow, transform.position, Quaternion.identity);
            shootWaitMode = true;
        }

    }
   

    [SerializeField] private float atackCD;
    [SerializeField] float guardWaitTime;
    [SerializeField] float targetLostTime;
    private IEnumerator WaitMode()
    {
        while (true)
        {
            if (guardWaitMode)
            {
                print("∆дЄт");
                yield return new WaitForSeconds(guardWaitTime);
                guardWaitMode = false;
            }
            if (shootWaitMode)
            {
                print(" д выстрела");
                yield return new WaitForSeconds(atackCD);
                shootWaitMode = false;
            }
            if (returnWaitMode)
            {
                print("∆дЄт ретЄрна");
                yield return new WaitForSeconds(targetLostTime);
                returnWaitMode = false;
            }
            yield return new WaitForFixedUpdate();


        }
    }
}
