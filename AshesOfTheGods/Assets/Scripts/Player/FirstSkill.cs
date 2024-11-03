using System.Runtime.CompilerServices;
using System.Collections;
using UnityEngine;
using System;

public class FirstSkill : MonoBehaviour
{
    [SerializeField] private float CoolDownTime = 10;
    private PlayerMovement pMovement;
    private bool ready = true;
    public bool In_armor {get; private set;} = false;
    public int AttacksCount {get;set;}
    void Awake()
    {
        pMovement = this.GetComponent<PlayerMovement>();
        StartCoroutine(WaitMode());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && ready)
            GetArmor();

        if (AttacksCount >= 5)
            RemArmor();
        print($"{In_armor}, {ready}, {AttacksCount}");
    }

    private void GetArmor()
    {
        In_armor = true;
        pMovement.SetSpeed((float)(pMovement.GetSpeed() * 1.5));
        AttacksCount = 0;
        this.GetComponent<SpriteRenderer>().color = Color.grey;
        ready = false;
    }
    private void RemArmor()
    {
        pMovement.SetSpeed(pMovement.GetSpeed() / 1.5f);
        this.GetComponent<SpriteRenderer>().color = Color.red;
        In_armor = false;
        AttacksCount = 0;  
    }

    private IEnumerator WaitMode()
    {
        while (true)
        {
            if (In_armor && AttacksCount <=5)
            AttacksCount+=2;
                yield return new WaitForSeconds(1);
            if (!ready && !In_armor)
            {
                print("Кнопка уходит в кд");
                yield return new WaitForSeconds(CoolDownTime);
                ready = true;
            }
            yield return new WaitForFixedUpdate();
        }
    }

}
