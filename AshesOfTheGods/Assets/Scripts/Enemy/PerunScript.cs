using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using UnityEngine;
using static System.Math;

public class PerunScript : MonoBehaviour
{
    [SerializeField] private AudioClip lightningPerunSound;
    private AudioSource perunLightningAudioSource;
    [SerializeField] private float lightningVolume;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameObject player;
    void Start()
    {
        perunLightningAudioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(MelleAttackCd());
        StartCoroutine(WaitAfterMelee());
        player = GameObject.FindWithTag("Player");
        returnMelleHandPos = melleHand.transform.position;
    }

    [Header("����� ������")]
    [SerializeField] GameObject startTarget;

    [Space]
    [Header("����� �������")]
    [SerializeField] GameObject melleHand;
    

    [Space]
    [Header("����� �������")]
    [SerializeField] GameObject rangeHand;
    
    private bool active = false;

    private Vector3 returnMelleHandPos;
    private bool inMelle = false;
    private Vector3 attackMove;
    private bool lightningSkill = true;
    public bool waitHand { get; set; } = false;
    [SerializeField] private GameObject hpBar;

    [SerializeField] private GameObject win;
    void FixedUpdate()
    {
        //Debug.Log(Mathf.Abs(melleHand.transform.position.x - player.transform.position.x) <= 1f & !attacked);
        //Debug.Log(Mathf.Abs(melleHand.transform.position.x - player.transform.position.x) <= 1f & !attacked);
        if (!active & player.transform.position.x > startTarget.transform.position.x & !gameObject.GetComponent<Enemy>().isDead)
        {
            active = true;
            hpBar.SetActive(true);
        }
        if (gameObject.GetComponent<Enemy>().isDead)
        {
            win.SetActive(true);
            active = false;
            Destroy(gameObject, 5f);
            hpBar.SetActive(false);

        }
        if (active)
        {
            if (rangeHand.GetComponent<PerunRangeHandScript>().Attack == false)
            {
                if (!waitMelleAttack & !waitHand)
                {

                    if (Mathf.Abs(melleHand.transform.position.x - player.transform.position.x) <= 1f & !attacked)
                    {
                        if (!inMelle)
                        {
                            returnMelleHandPos = melleHand.transform.position;

                        }
                        inMelle = true;
                        attackMove = player.transform.position;
                    }

                    if (inMelle)
                        MelleHandAttack(attackMove);

                    if (!inMelle)
                        MelleHandMove();

                    if (attacked)
                        ReturnMelleHand();
                }
            }
            
            

            
            LightningSkill();
        }

       




    }

    private bool attacked = false;

    [Space]
    [Header("������� ������")]
    [SerializeField] private GameObject lightning;
    [Space]
    [Header("����� ������")]
    [SerializeField] private GameObject lightningSpot1;
    [SerializeField] private GameObject lightningSpot2;
    [SerializeField] private GameObject lightningSpot3;
    [SerializeField] private GameObject lightningSpot4;
    [SerializeField] private GameObject lightningSpot5;
    [SerializeField] private GameObject lightningSpot6;

    private bool flagOne = false;
    private bool flagTwo = false;
    private bool flagThree = false;
    private void LightningSkill()
    {
        Sounds.StaticSound(lightningPerunSound, perunLightningAudioSource, lightningVolume);
        if (gameObject.GetComponent<Enemy>().HpNow > (gameObject.GetComponent<Enemy>().HpMax * 2 / 3))
            flagOne = true;
        if (gameObject.GetComponent<Enemy>().HpNow <= (gameObject.GetComponent<Enemy>().HpMax * 2 / 3) & flagOne)
        {
            Debug.Log("Lightning 1");
            
            Instantiate(lightning, lightningSpot1.transform.position, Quaternion.identity);
            Instantiate(lightning, lightningSpot2.transform.position, Quaternion.identity);
            Instantiate(lightning, lightningSpot3.transform.position, Quaternion.identity);
            Instantiate(lightning, lightningSpot4.transform.position, Quaternion.identity);
            Instantiate(lightning, lightningSpot5.transform.position, Quaternion.identity);
            Instantiate(lightning, lightningSpot6.transform.position, Quaternion.identity);
            flagOne = false;
        }


        if (gameObject.GetComponent<Enemy>().HpNow > (gameObject.GetComponent<Enemy>().HpMax / 2))
            flagTwo = true;

        if (gameObject.GetComponent<Enemy>().HpNow <= (gameObject.GetComponent<Enemy>().HpMax / 2) & flagTwo)
        {
            Debug.Log("Lightning 2");
            
            Instantiate(lightning, lightningSpot1.transform.position, Quaternion.identity);
            Instantiate(lightning, lightningSpot2.transform.position, Quaternion.identity);
            Instantiate(lightning, lightningSpot3.transform.position, Quaternion.identity);
            Instantiate(lightning, lightningSpot4.transform.position, Quaternion.identity);
            Instantiate(lightning, lightningSpot5.transform.position, Quaternion.identity);
            Instantiate(lightning, lightningSpot6.transform.position, Quaternion.identity);
            flagTwo = false;
        }

        if (gameObject.GetComponent<Enemy>().HpNow > (gameObject.GetComponent<Enemy>().HpMax / 4))
            flagThree = true;

        if (gameObject.GetComponent<Enemy>().HpNow <= (gameObject.GetComponent<Enemy>().HpMax / 4) & flagThree)
        {
            Debug.Log("Lightning 3");
            
            Instantiate(lightning, lightningSpot1.transform.position, Quaternion.identity);
            Instantiate(lightning, lightningSpot2.transform.position, Quaternion.identity);
            Instantiate(lightning, lightningSpot3.transform.position, Quaternion.identity);
            Instantiate(lightning, lightningSpot4.transform.position, Quaternion.identity);
            Instantiate(lightning, lightningSpot5.transform.position, Quaternion.identity);
            Instantiate(lightning, lightningSpot6.transform.position, Quaternion.identity);
            flagThree = false;
        }
        
    }

    
    private void MelleHandMove()
    {
        Vector2 melleHandMove = (player.transform.position - melleHand.transform.position).normalized;
        melleHandMove.y = 0;

      
        melleHand.GetComponent<Rigidbody2D>().MovePosition
            (Vector2.MoveTowards(melleHand.GetComponent<Rigidbody2D>().position, new Vector2(player.transform.position.x, melleHand.transform.position.y),
            10f*Time.fixedDeltaTime));
    }
    private void MelleHandAttack(Vector2 attackMove)
    {
        if (Mathf.Abs(melleHand.GetComponent<BoxCollider2D>().bounds.min.y - startTarget.transform.position.y) > 0.1f)
        {
            
            melleHand.GetComponent<Rigidbody2D>().MovePosition
                (Vector2.MoveTowards(melleHand.GetComponent<Rigidbody2D>().position, attackMove, 13f * Time.fixedDeltaTime));
        }
        else
        {
            inMelle = false;
            attacked = true;
            waitHand = true;
            melleHand.GetComponent<PerunMelleHandScript>().Active = false;
        }
    }

    private bool waitMelleAttack = false;
    private void ReturnMelleHand()
    {
        Vector2 returnMove = returnMelleHandPos;
        
        if (Vector2.Distance(melleHand.transform.position, returnMelleHandPos) > 0.1f)
        {
            melleHand.GetComponent<Rigidbody2D>().MovePosition
                (Vector2.MoveTowards(melleHand.GetComponent<Rigidbody2D>().position,returnMove, 13f * Time.fixedDeltaTime));
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

    private IEnumerator WaitAfterMelee()
    {
        while (true)
        {
            if (waitHand)
            {
                yield return new WaitForSeconds(2);
                waitHand = false;
                melleHand.GetComponent<PerunMelleHandScript>().Active = true;
            }

            yield return new WaitForFixedUpdate();
        }
    }

   
}
