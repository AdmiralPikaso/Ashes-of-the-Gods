using System.Runtime.CompilerServices;
using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;

public class FirstSkill : MonoBehaviour
{
    [SerializeField] private float CoolDownTime = 10;
    private PlayerMovement pMovement;
    private bool ready = true;
    public bool In_armor { get; private set; } = false;
    public int AttacksCount { get; set; }
    void Awake()
    {
        pMovement = this.GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        StartCoroutine(WaitMode());
        StartCoroutine(ReloadUi());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && ready)
            GetArmor();

        if (AttacksCount >= 5)
            RemArmor();
        //print($"{In_armor}, {ready}, {AttacksCount}");
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

    private bool firstSkillUiReload = false;
    private IEnumerator WaitMode()
    {
        while (true)
        {
            if (!ready && !In_armor)
            {
                print("Кнопка уходит в кд");
                firstSkillUiReload = true;
                yield return new WaitForSeconds(CoolDownTime);
                print("Кнопка вышла из кд");
                ready = true;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    [SerializeField] Image firstSkillFill;
    [SerializeField] GameObject firstSkillShade;
    private float cd = 0;
    private IEnumerator ReloadUi()
    {
        while (true)
        {
            
            if (firstSkillUiReload)
            {
                firstSkillShade.SetActive(true);
                firstSkillFill.fillAmount = 0;
                while (cd != 1)
                {
                    cd += Time.deltaTime;
                    print("Кдшится");
                    firstSkillFill.fillAmount = cd/CoolDownTime;
                    if (ready == true)
                        cd = 1;
                    yield return null;
                }
                firstSkillUiReload = false;
                firstSkillShade.SetActive(false);
                cd = 0;
            }
            yield return new WaitForFixedUpdate();
        }
    }


}
