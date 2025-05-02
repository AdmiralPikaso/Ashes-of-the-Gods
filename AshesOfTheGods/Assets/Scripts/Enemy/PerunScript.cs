using System.Collections;
using UnityEngine;
using static System.Math;

public class PerunScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameObject player;
    void Start()
    {
        StartCoroutine(MelleAttackCd());

        player = GameObject.FindWithTag("Player");
        returnMelleHandPos = melleHand.transform.position;
    }

    [Header("точка старта")]
    [SerializeField] GameObject startTarget;

    [Space]
    [Header("кулак ближник")]
    [SerializeField] GameObject melleHand;

    [Space]
    [Header("кулак дальник")]
    [SerializeField] GameObject rangeHand;
    
    private bool active = false;

    private Vector3 returnMelleHandPos;
    private bool inMelle = false;
    private Vector3 attackMove;
    private bool lightningSkill = true;
    private bool waitHand = false;
    void FixedUpdate()
    {
        //Debug.Log();
        if (!active & player.transform.position.x > startTarget.transform.position.x & !gameObject.GetComponent<Enemy>().isDead)
            active = true;

        if (gameObject.GetComponent<Enemy>().isDead)
            active = false;

        if (active) 
        {
            if (rangeHand.GetComponent<PerunRangeHandScript>().Attack == false)
            {
                if (!waitMelleAttack)
                {
                    if (Mathf.Abs(melleHand.transform.position.x - player.transform.position.x) <= 1f & !attacked)
                    {

                        inMelle = true;
                        attackMove = player.transform.position;
                        MelleHandAttack(attackMove);
                    }

                    if (inMelle)
                        MelleHandAttack(attackMove);

                    if (!inMelle)
                        MelleHandMove();

                    if (attacked)
                        ReturnMelleHand();
                }
            }
           
        }

        //if (HpNow <= HpMax/2 & lightningSkill)
          //  LightningSkill();

        
            
            
    }

    private bool attacked = false;

    [Space]
    [Header("Большая молния")]
    [SerializeField] private GameObject lightning;
    [Space]
    [Header("Споты молний")]
    [SerializeField] private GameObject lightningSpot1;
    [SerializeField] private GameObject lightningSpot2;
    [SerializeField] private GameObject lightningSpot3;
    [SerializeField] private GameObject lightningSpot4;
    private void LightningSkill()
    {
        Instantiate(lightning, lightningSpot1.transform.position, Quaternion.identity);
        Instantiate(lightning, lightningSpot2.transform.position, Quaternion.identity);
        Instantiate(lightning, lightningSpot3.transform.position, Quaternion.identity);
        Instantiate(lightning, lightningSpot4.transform.position, Quaternion.identity);
        lightningSkill = false;
    }
    private void MelleHandMove()
    {
        Vector2 melleHandMove = (player.transform.position - melleHand.transform.position).normalized;
        melleHandMove.y = 0;

        //melleHand.GetComponent<Rigidbody2D>().MovePosition
        //  (melleHand.GetComponent<Rigidbody2D>().position + Time.fixedDeltaTime * melleHandMove * 10);

        melleHand.GetComponent<Rigidbody2D>().MovePosition
            (Vector2.MoveTowards(melleHand.GetComponent<Rigidbody2D>().position, new Vector2(player.transform.position.x, melleHand.transform.position.y),
            10*Time.fixedDeltaTime));
    }
    private void MelleHandAttack(Vector2 attackMove)
    {
        if (Mathf.Abs(melleHand.GetComponent<BoxCollider2D>().bounds.min.y - startTarget.transform.position.y) > 0.1f)
        {
            
            melleHand.GetComponent<Rigidbody2D>().MovePosition
                (Vector2.MoveTowards(melleHand.GetComponent<Rigidbody2D>().position, attackMove, 10f * Time.fixedDeltaTime));
        }
        else
        {
            inMelle = false;
            attacked = true;
        }
    }

    private bool waitMelleAttack = false;
    private void ReturnMelleHand()
    {
        Vector2 returnMove = returnMelleHandPos;

        if (Vector2.Distance(melleHand.transform.position, returnMelleHandPos) > 0.1f)
        {
            melleHand.GetComponent<Rigidbody2D>().MovePosition
                (Vector2.MoveTowards(melleHand.GetComponent<Rigidbody2D>().position,returnMove, 10f * Time.fixedDeltaTime));
        }
        else
        {
            attacked = false;
            waitMelleAttack = true;
        }
        

    }

    private IEnumerator MelleAttackCd()
    {
        while (true)
        {
            if (waitMelleAttack)
            {
                rangeHand.GetComponent<PerunRangeHandScript>().Attack = true;   
                yield return new WaitForSeconds(3);
                waitMelleAttack = false;
                

            }

            yield return new WaitForFixedUpdate();
        }

    }
}
