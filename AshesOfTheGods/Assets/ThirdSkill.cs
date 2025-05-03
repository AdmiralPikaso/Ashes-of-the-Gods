using System;
using System.Collections;
using UnityEngine;

public class ThirdSkill : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform firePoint;
    public GameObject leftArm;
    public GameObject rightArm;
    public GameObject lieLeft;
    public GameObject lieRight;

    [SerializeField] private float coolDownTime = 10.0f;
    private bool ready;

    private void Start()
    {
        StartCoroutine(WaitMode());
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
        
        if (homing != null && target != null)
        {
            homing.target = target.transform;
        }
        ready = false;
    }
    private bool thirdskillUIreload;
    private IEnumerator WaitMode()
    {
        while (true)
        {
            if (!ready)
            {
                print("”ıÓ‰ËÚ ‚ Í‰");
                thirdskillUIreload = true;
                yield return new WaitForSeconds(coolDownTime);              
                ready = true;
                print("√Œ“Œ¬");
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
