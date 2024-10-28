using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float hp;
    public float HpNow { get; private set; }
    public void IncHp(float damage)
    {
        HpNow -= damage;
        if (HpNow <= 0)
            Lose();
    }

    public void Lose()
    {
        //Some code
        HpNow = hp;
    }
}
