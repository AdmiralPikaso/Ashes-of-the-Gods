using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ThirdSkill : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform firePoint;
    public GameObject leftArm;
    public GameObject rightArm;
    public GameObject lieLeft;
    public GameObject lieRight;

    [SerializeField] private float coolDownTime = 10.0f;
    private bool ready = true;

    private void Start()
    {
        StartCoroutine(WaitMode());
        StartCoroutine(ReloadUi());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && ready)
        {
            SpawnFireball();
        }
    }

    void SpawnFireball()
    {
        print("Œ√ŒÕ‹");
        GameObject target = leftArm;
        bool between = true;
        if (transform.position.x < leftArm.transform.position.x || transform.position.x > rightArm.transform.position.x)
            between = false;
        if (!between)
        {
            target = Vector2.Distance(transform.position, leftArm.transform.position) < Vector2.Distance(transform.position, rightArm.transform.position) ? leftArm : rightArm;
            if (target == leftArm)
                if (transform.localScale.x < 1)
                    target = lieLeft;
            if (target == rightArm)
                if (transform.localScale.x > 1)
                    target= lieRight;
        }
        else
        {
            if (transform.localScale.x > 0)
                target = rightArm;
            else
                target = leftArm;
        }
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
        FireBlast homing = fireball.GetComponent<FireBlast>();
        
        
            homing.target = target.transform;
        
        ready = false;
    }
    
    private IEnumerator WaitMode()
    {
        while (true)
        {
            if (!ready)
            {
                print("”ıÓ‰ËÚ ‚ Í‰");
                thirdSkillUiReload = true;
                yield return new WaitForSeconds(coolDownTime);              
                ready = true;
                print("√Œ“Œ¬");
            }
            yield return new WaitForFixedUpdate();
        }
    }


    private bool thirdSkillUiReload;
    [SerializeField] Image thirdSkillFill;
    [SerializeField] GameObject thirdSkillShade;
    private float cd = 0;
    
    private IEnumerator ReloadUi()
    {
        while (true)
        {

            if (thirdSkillUiReload)
            {
                thirdSkillShade.SetActive(true);
                thirdSkillFill.fillAmount = 0;
                while (cd != 1)
                {
                    cd += Time.deltaTime;
                    //print(" ‰¯ËÚÒˇ");
                    thirdSkillFill.fillAmount = cd / coolDownTime;
                    if (ready == true)
                        cd = 1;
                    yield return null;
                }
                thirdSkillUiReload = false;
                thirdSkillShade.SetActive(false);
                cd = 0;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
