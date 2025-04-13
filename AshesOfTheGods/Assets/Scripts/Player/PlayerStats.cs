using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [Header("Hit Sounds")]
    [SerializeField] private AudioClip[] hitSounds;
    [SerializeField] private float hitSoundVolume;
    [SerializeField] private float volume;
    [SerializeField] protected float minPitch;
    [SerializeField] protected float maxPitch;
    [SerializeField] protected AudioClip armorDamageSound;
    private AudioSource audioSource;
    private Animator animator;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private float hp;
    public float HpNow { get; private set; }
    public float HpMax { get; private set; }

    public bool godMode = false;
    private SecondSkill dash;
    Collider2D col;
    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();  
        animator = GetComponent<Animator>();
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
                {
                    Sounds.Sound(armorDamageSound, audioSource, volume, minPitch, maxPitch);
                    this.GetComponent<FirstSkill>().AttacksCount += 1;
                }
                else
                {
                    PlayRandomHitSound();
                    HpNow -= damage;
                }
                if (HpNow <= 0)
                    animator.SetBool("Die", true);
            }
        }
    }

    private int lastSoundIndex = -1;

    private void PlayRandomHitSound()
    {
        int randomIndex = Random.Range(0, hitSounds.Length);
        if (randomIndex == lastSoundIndex)
            randomIndex = Random.Range(0, hitSounds.Length);
        else
            Sounds.Sound(hitSounds[randomIndex], audioSource, volume);
        lastSoundIndex = randomIndex;
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
            animator.SetBool("Die", true);
    }
    public void Lose()
    {
        print(HpNow);
    }

    public void Heal(float heal)
    {
        HpNow += heal;
        if (HpNow > 100)
            HpNow = 100;
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
