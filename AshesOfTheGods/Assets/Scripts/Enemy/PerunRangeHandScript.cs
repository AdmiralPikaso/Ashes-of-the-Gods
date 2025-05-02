using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PerunRangeHandScript : MonoBehaviour
{
   
    void Start()
    {
        StartCoroutine(WaitAfterAttack());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log($"Стрельба {Attack}");
        Debug.Log($"Кд стрельбы {waitAfterAttack}");
        if (Attack & !waitAfterAttack)
            LightningBlast();
        
    }

    [SerializeField] private GameObject lightning;
    [SerializeField] private GameObject fireSpot1;
    [SerializeField] private GameObject fireSpot2;
    [SerializeField] private GameObject fireSpot3;

    public bool Attack { get; set; } = false;

    private bool waitAfterAttack = false;
    private int attackCount = 0;
    private void LightningBlast()
    {
        switch (attackCount)
        {
            case 0:
                Instantiate(lightning, fireSpot1.transform.position, Quaternion.identity);
                break;
            case 1:
                Instantiate(lightning, fireSpot2.transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(lightning, fireSpot3.transform.position, Quaternion.identity);
                break;
        }
        waitAfterAttack = true;
        attackCount++;
    }

    private IEnumerator WaitAfterAttack()
    {
        while (true)
        {
            if (waitAfterAttack)
            {
                yield return new WaitForSeconds(2);
                waitAfterAttack = false;
                if (attackCount == 3)
                {
                    Attack = false;
                    attackCount = 0;
                }

            }
            yield return new WaitForFixedUpdate();
        }
    }
}
