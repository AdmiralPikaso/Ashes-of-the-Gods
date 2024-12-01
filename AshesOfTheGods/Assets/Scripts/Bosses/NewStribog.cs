using UnityEngine;

public class NewStribog : Enemy
{
    public float MaxHp = 300;
    public float GetHp() => hp;
    void Awake()
    {
        hp = 300;
    }
}
