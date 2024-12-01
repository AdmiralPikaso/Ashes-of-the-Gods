using NUnit.Framework;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float hp;

    public void TakeDamage(float damage)
    {
        hp -= damage;
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
