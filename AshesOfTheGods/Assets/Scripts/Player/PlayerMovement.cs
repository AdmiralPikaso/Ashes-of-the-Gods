using Unity.IO.LowLevel.Unsafe;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private bool on_ground = false;
    private bool on_platform = false;
    private bool on_moving_platform = false;
    private bool in_air = false;
    private bool in_wall = false;
    public bool GetOnPlatform()
    {
        return on_platform;
    }

    public bool InAir()
    {
        return in_air;
    }
    private Transform targetParent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;

        if (collisionObject.CompareTag("Ground"))
        {
            on_ground = true;
            animator.SetBool("Jump", false);
        }

        if (collisionObject.CompareTag("Platform"))
        {
            on_platform = true;
            animator.SetBool("Jump", false);
        }

        if (collisionObject.CompareTag("PlatformDown"))
        {
            on_platform = false;
            on_moving_platform = false;
        }

        if (collisionObject.CompareTag("Wall"))
        {
            in_wall = true;
            animator.SetBool("Jump", false);
        }

        if (collisionObject.CompareTag("MovingPlatform"))
        {
            on_moving_platform = true;
            targetParent = collisionObject.transform;
            transform.SetParent(targetParent);
            animator.SetBool("Jump", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;

        if (collisionObject.CompareTag("Ground"))
            on_ground = false;

        if (collisionObject.CompareTag("Platform"))
            on_platform = false;

        if (collisionObject.CompareTag("Wall"))
            in_wall = false;

        if (collisionObject.CompareTag("MovingPlatform"))
        {
            on_moving_platform = false;
            if (gameObject.activeInHierarchy)
            {
                transform.SetParent(null);
            }
        }
    }
    private bool in_enemy;
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Enemy"))
            speed = speed / 2;
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Enemy"))
            speed = speed *= 2;
    }

    private Rigidbody2D rigidB;
    private SecondSkill dash;
    void Awake()
    {
        dash = GetComponent<SecondSkill>();
        rigidB = GetComponent<Rigidbody2D>();
    }


    [SerializeField] private float speed;
    [SerializeField] private float airSpeedMultiplier;
    private float moveDirection;
    private float lastMoveDir = 0;
    public float GetMoveDir() => lastMoveDir;
    public float GetSpeed() => speed;
    public void SetSpeed(float newSpeed) => speed = newSpeed;
    
    public float GetVelocityX() => rigidB.linearVelocityX;
    public void SetVelocityX(float newVel) => rigidB.linearVelocityX = newVel;

    public float GetMoveDirection()
    {
        return moveDirection;
    }

    [SerializeField] private AudioClip walkingSound;
    private AudioSource audioSource;
    [SerializeField] private float minPitch;
    [SerializeField] private float maxPitch;
    [SerializeField] private float volume;
    public bool canMoveInAttack = true;
    public bool canMoveInRegularAttackArmor = true;
    public bool canMoveInHeavyAttackArmor = true;
    public bool Death = false;
    private void CanNotMoveInAttack()
    {
        canMoveInAttack = false;
    }
    private void CanMoveInAttack()
    {
        canMoveInAttack = true;
    }
    private void CanNotMoveInRegularAttackArmor()
    {
        canMoveInRegularAttackArmor = false;
    }
    private void CanMoveInRegularAttackArmor()
    {
        canMoveInRegularAttackArmor = true;
    }
    private void CanNotMoveInHeavyAttackArmor()
    {
        canMoveInHeavyAttackArmor = false;
    }
    private void CanMoveInHeavyAttackArmor()
    {
        canMoveInHeavyAttackArmor = true;
    }
    private void CanNotMoveInDeath()
    {
        Death = true;
    }
    public Vector2 direction;
    
    public void MovementLogic(float moveDir)
    {
        if (canMoveInAttack & canMoveInRegularAttackArmor & canMoveInHeavyAttackArmor)
        {
            if (!dash.GetinDash())
            {
                float RealSpeed = speed;
                if (in_air)
                    RealSpeed *= airSpeedMultiplier;
                if (in_enemy)
                {
                    RealSpeed /= 2;
                    in_enemy = false;
                }
                if (!in_wall)
                {
                    rigidB.linearVelocityX = RealSpeed * moveDir;
                    if (!in_air)
                    {
                        animator.SetBool("Walk", moveDir != 0);
                        animator.SetBool("WalkArmor", moveDir != 0);
                    }
                    if (moveDir != 0)
                        transform.localScale = new Vector2(moveDir < 0 ? -math.abs(transform.localScale.x) : math.abs(transform.localScale.x), transform.localScale.y);
                    }
                else
                {
                    if (lastMoveDir == moveDir)
                    {
                        rigidB.linearVelocityX = 0;
                        if (!in_air)
                    {
                        animator.SetBool("Walk", moveDir != 0);
                        animator.SetBool("WalkArmor", moveDir != 0);
                    }
                    }
                    else 
                    {
                        rigidB.linearVelocityX = RealSpeed * moveDir;
                        if (!in_air)
                    {
                        animator.SetBool("Walk", moveDir != 0);
                        animator.SetBool("WalkArmor", moveDir != 0);
                    }
                        if (moveDir != 0)
                         transform.localScale = new Vector2(moveDir < 0 ? -math.abs(transform.localScale.x) : math.abs(transform.localScale.x), transform.localScale.y);
                    }
                    lastMoveDir = moveDir;
                }
            }
        }
        else
        {
            if (!in_air)
                rigidB.linearVelocityX = 0;
            if (!in_air)
                {
                    animator.SetBool("Walk", moveDir != 0);
                    animator.SetBool("WalkArmor", moveDir != 0);
                }
        }
    }
    private float DashlastMoveDir;
    public void DastLogic(float movedir)
    {
        if (canMoveInAttack & canMoveInRegularAttackArmor & canMoveInHeavyAttackArmor)
        {
            bool wasDashing = false;
            if (dash.GetinDash())
            {
                float RealSpeed = dash.GetDashDistance() / dash.GetDashTime();
                rigidB.linearVelocityX = RealSpeed * DashlastMoveDir;
                rigidB.linearVelocityY = 0;
                wasDashing = true;
            }
            else if (wasDashing)
            {
                rigidB.linearVelocityX = 0;
            }
            if (!dash.GetinDash() && movedir != 0)
                DashlastMoveDir = movedir;
        }
    }

    public void ImpulseLogic(float force)
    {
        rigidB.AddForceX(force);
    }

    private bool JumpPressed;
    [SerializeField] private float jumpForce;
    private void JumpLogic()
    {
        if (canMoveInAttack & canMoveInRegularAttackArmor & canMoveInHeavyAttackArmor)
        {
            JumpPressed = Input.GetKeyDown(KeyCode.Space);
            if (!in_air & JumpPressed & !FindFirstObjectByType<SecondSkill>().GetinDash())
            {
                rigidB.linearVelocityY = 0;
                rigidB.AddForceY(jumpForce, ForceMode2D.Impulse);
                animator.SetBool("Jump", true);
                JumpPressed = false;
            }
        }
    }

    private void WalkSounds()
    {
        if ((on_ground || on_platform || on_moving_platform) && moveDirection != 0 && walkingSound != null & canPlayStepSound)
        {
            Sounds.Sound(walkingSound, audioSource, volume, minPitch, maxPitch);
            canPlayStepSound = false;
        }
    }

    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(WaitStepSound());
    }

    void Update()
    {
        if (gameObject.GetComponent<FirstSkill>().inArmorAnim & !in_air)
                rigidB.linearVelocityX = 0;
        if (!gameObject.GetComponent<FirstSkill>().inArmorAnim & !Death & !gameObject.GetComponent<PlayerStats>().isEsc)
        {
            moveDirection = Input.GetAxis("Horizontal");
            in_air = !on_ground && !on_platform && !on_moving_platform;
            JumpLogic();
        }
    }
    void FixedUpdate()
    {
        if (!gameObject.GetComponent<FirstSkill>().inArmorAnim & !Death & !gameObject.GetComponent<PlayerStats>().isEsc)
        {
            MovementLogic(moveDirection);
            DastLogic(moveDirection);
        }
    }

    [SerializeField] private float StepSoundDelay = 0.15f;
    private bool canPlayStepSound;
    private IEnumerator WaitStepSound()
    {
        while (true)
        {
            if (!canPlayStepSound)
            {
                yield return new WaitForSeconds(StepSoundDelay);
                canPlayStepSound = true;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}