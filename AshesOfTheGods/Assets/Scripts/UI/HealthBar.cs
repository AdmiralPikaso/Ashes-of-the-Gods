using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    
    [SerializeField] GameObject creature;
    private float maxHealth;
    private float currentHealth;
    Image healthBar;
    private bool playerOrEnemy = true; //true == player; false == enemy
    private void Start()
    {
        if (creature.CompareTag("Player"))
        {
            maxHealth = creature.GetComponent<PlayerStats>().HpMax;
            playerOrEnemy = true;

        }
        else if (creature.CompareTag("Enemy"))
        {
            maxHealth = creature.GetComponent<Enemy>().HpMax;
            playerOrEnemy = false;

        }

        healthBar = GetComponent<Image>();

    }

   


    // Update is called once per frame
    void Update()
    {

        if (playerOrEnemy)
        {
            
            currentHealth = creature.GetComponent<PlayerStats>().HpNow;
        }
        else
        {
            
            currentHealth = creature.GetComponent<Enemy>().HpNow;
        }
        //print($"Ьръё ея = {maxHealth}");
        //print($"ея = {currentHealth}");

        healthBar.fillAmount = currentHealth/maxHealth;
    }
}
