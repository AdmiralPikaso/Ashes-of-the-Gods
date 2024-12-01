using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float hp;
    public float HpNow { get; private set; }
    public float HpMax { get; private set; }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        HpNow = hp;
        HpMax = hp;
    }
    public void TakeDamage(float damage)
    {
        HpNow -= damage;
        if (HpNow <= 0)
        {
            Die();
        }
        Debug.Log("Хп врага" + HpNow);
    }

    void Die()
    {
        print("Смерть");
        animator.SetBool("IsDie", true);
        Destroy(gameObject, 1.5f);
    }
}
