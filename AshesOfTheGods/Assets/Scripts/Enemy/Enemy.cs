using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float hp;
    public float HpNow { get; private set; }
    public float HpMax { get; private set; }
    private void Awake()
    {
        HpNow = hp;
        HpMax = hp;
    }
    public void TakeDamage(float damage)
    {
        HpNow -= damage;
        if (hp <= 0)
        {
            Die();
        }
        Debug.Log(hp);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
