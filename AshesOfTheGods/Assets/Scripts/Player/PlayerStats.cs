using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float hp;
    public float HpNow { get; private set; }
    public void ReduceHp(float damage)
    {
        if (this.GetComponent<FirstSkill>().In_armor)
            this.GetComponent<FirstSkill>().AttacksCount+=1;
        else{
        HpNow -= damage;
        }
        if (HpNow <= 0)
            Lose();
    }

    public void Lose()
    {
        //Some code
        HpNow = hp;
    }
    void Update()
    {
       // print(HpNow);
    }
}
