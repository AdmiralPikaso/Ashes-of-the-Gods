using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using NUnit.Framework;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.CompilerServices;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;

public class SecondSkill : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float CoolDownTime;
    [SerializeField] private float DashDistance = 15;
    [SerializeField] private float DashTime = 0.25f;

    private bool cd;

    public float GetDashDistance() { return DashDistance; }
    public float GetDashTime() { return DashTime; }
    private PlayerMovement pmovement;
    void Awake()
    {
        pmovement = this.GetComponent<PlayerMovement>();
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(DashTimer());
        StartCoroutine(DashCoolDown());
        StartCoroutine(ReloadUi());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) & GetComponent<PlayerMovement>().canMoveInAttack & GetComponent<PlayerMovement>().canMoveInRegularAttackArmor & GetComponent<PlayerMovement>().canMoveInHeavyAttackArmor & !GetComponent<PlayerMovement>().Death & !GetComponent<FirstSkill>().inArmorAnim & !inDash)
            Dash();
    }
    private bool inDash = false;

    public bool GetinDash() { return inDash; }
    void Dash()
    {
        if (!cd)
        {
            animator.SetTrigger("Dash");
            inDash = true;
            ready = false;
        }
    }

    private IEnumerator DashTimer()
    {
        while (true)
        {
            if (inDash == true)
            {
                yield return new WaitForSeconds(DashTime);
                inDash = false;
                cd = true;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private bool secondSkillUiReload = false;
    private IEnumerator DashCoolDown()
    {
        while (true)
        {
            if (!ready && cd)
            {
                secondSkillUiReload = true;
                yield return new WaitForSeconds(CoolDownTime);
                cd = false;
                ready = true;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    [SerializeField] Image secondSkillFill;
    [SerializeField] GameObject secondSkillShade;
    private float skillCd = 0;
    private bool ready = true;

    private IEnumerator ReloadUi()
    {
        while (true)
        {

            if (secondSkillUiReload)
            {
                secondSkillShade.SetActive(true);
                secondSkillFill.fillAmount = 0;
                while (skillCd != 1)
                {
                    skillCd += Time.deltaTime;
                    //print("Êהרטעסÿ");
                    secondSkillFill.fillAmount = skillCd / CoolDownTime;
                    if (ready == true)
                        skillCd = 1;
                    yield return null;
                }
                secondSkillUiReload = false;
                secondSkillShade.SetActive(false);
                skillCd = 0;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}