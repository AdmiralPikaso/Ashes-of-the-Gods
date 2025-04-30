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
    
    private bool active = false;

    private Vector3 returnMelleHandPos;
    private bool inMelle = false;
    private Vector3 attackMove;
    void FixedUpdate()
    {
        //Debug.Log();
        if (!active & player.transform.position.x > startTarget.transform.position.x)
            active = true;

        if (active) 
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

    private bool attacked = false;

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
                yield return new WaitForSeconds(3);
                waitMelleAttack = true;
            }

            yield return new WaitForFixedUpdate();
        }

    }
}
