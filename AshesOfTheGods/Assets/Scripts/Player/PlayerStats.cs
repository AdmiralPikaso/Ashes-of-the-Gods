using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private float hp;
    public float HpNow { get; private set; }
    public float HpMax { get; private set; }

    public bool godMode = false;
    private SecondSkill dash;
    Collider2D col;
    void Awake()
    {
        dash = this.GetComponent<SecondSkill>();

        HpNow = hp;
        HpMax = hp;
        col = GetComponent<Collider2D>();
    }


    public void ReduceHp(float damage)
    {
        if (!godMode)
        {
            if (!dash.GetinDash())
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
        }
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

    public bool isEsc { get; set; } = false;
    public void Death()
    {
        //Destroy(gameObject);
        isEsc = true;
        deathScreen.SetActive(true);
        //LevelManager.instance.Respawn();
    }
}
