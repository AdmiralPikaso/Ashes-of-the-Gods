using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using System.Collections;
using NUnit.Framework;
using System.Runtime.InteropServices.WindowsRuntime;
public class SecondSkill : MonoBehaviour
{
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
        StartCoroutine(DashTimer());
         StartCoroutine(DashCoolDown());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) & GetComponent<PlayerMovement>().canMoveInAttack & GetComponent<PlayerMovement>().canMoveInRegularAttackArmor & !GetComponent<PlayerMovement>().Death & !GetComponent<FirstSkill>().inArmorAnim)
            Dash();
    }
    private bool inDash = false;

    public bool GetinDash() { return inDash; }
    void Dash()
    {
        if (!cd)
            inDash = true;
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
    private IEnumerator DashCoolDown()
    {
        while (true)
        {
            if (cd == true)
            {
                yield return new WaitForSeconds(CoolDownTime);
                cd = false;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}