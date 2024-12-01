using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private GameObject deathScreen; 
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
            Death();
    }
    void Awake()
    {

        HpNow = hp;
    }
    public void Lose()
    {
        print(HpNow);     
    }

    public void Death()
    {
        Destroy(gameObject);
        deathScreen.SetActive(true);      
        //LevelManager.instance.Respawn();
    }
}
