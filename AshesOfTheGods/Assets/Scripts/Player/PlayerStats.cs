using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] protected AudioClip healSound;
    protected AudioSource audioSource;
    [SerializeField] protected float vlm;
    
    [Header("Damage Sounds")]
    [SerializeField] private AudioClip[] damageSounds;
    [SerializeField] private float hitSoundVolume;
    [SerializeField] protected float minPitch;
    [SerializeField] protected float maxPitch;
    [SerializeField] private AudioClip[] armorSounds;
    [SerializeField] private float armorVolume;
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
        Application.targetFrameRate = 200;
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
                    PlayRandomHitArmorSound();
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
        int randomIndex = Random.Range(0, damageSounds.Length);
        if (randomIndex == lastSoundIndex)
            randomIndex = Random.Range(0, damageSounds.Length);
        else
            Sounds.StaticSound(damageSounds[randomIndex], audioSource, hitSoundVolume);
        lastSoundIndex = randomIndex;
    }

    private void PlayRandomHitArmorSound()
    {
        int randomIndex = Random.Range(0, armorSounds.Length);
        if (randomIndex == lastSoundIndex)
            randomIndex = Random.Range(0, armorSounds.Length);
        else
            Sounds.StaticSound(armorSounds[randomIndex], audioSource, armorVolume);
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
        Sounds.StaticSound(healSound, audioSource, vlm);
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
