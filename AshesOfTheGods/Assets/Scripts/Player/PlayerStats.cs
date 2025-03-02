using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private GameObject deathScreen; 
    [SerializeField] private float hp;
    public float HpNow { get; private set; }
    public float HpMax { get; private set; }
    public void ReduceHp(float damage)
    {
        if (this.GetComponent<FirstSkill>().In_armor)
            this.GetComponent<FirstSkill>().AttacksCount += 1;
        else
        {
            HpNow -= damage;
        }
        if (HpNow <= 0)
            Death();
    }
    Collider2D col;
    void Awake()
    {

        HpNow = hp;
        HpMax = hp;
        col = GetComponent<Collider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
            Death();
    }
    public void Lose()
    {
        print(HpNow);     
    }

    public bool IsDead { get; private set; } = false;
    public void Death()
    {
        IsDead = true;
        
        //Destroy(gameObject);
        deathScreen.SetActive(true);
        Time.timeScale = 0f;
        //LevelManager.instance.Respawn();
    }

    public bool IsEsc { get; set; } = false;
    
    
}
