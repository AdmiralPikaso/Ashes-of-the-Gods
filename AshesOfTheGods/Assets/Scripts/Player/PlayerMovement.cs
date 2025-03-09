using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
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
            on_ground = true;

        if (collisionObject.CompareTag("Platform"))
            on_platform = true;

        if (collisionObject.CompareTag("PlatformDown"))
        {
            on_platform = false;
            on_moving_platform = false;
        }

        if (collisionObject.CompareTag("Wall"))
            in_wall = true;

        if (collisionObject.CompareTag("MovingPlatform"))
        {
            on_moving_platform = true;
            targetParent = collisionObject.transform;
            transform.SetParent(targetParent);
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
    void Awake()
    {
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

    [SerializeField] private AudioClip walkingSound;
    private AudioSource audioSource;
    private float lastWalkingSoundTime;
    [SerializeField] private float walkingSoundInterval;
    [SerializeField] private float minPitch;
    [SerializeField] private float maxPitch;
    [SerializeField] private float volume;

    public void MovementLogic(float moveDir)
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
            rigidB.linearVelocityX = RealSpeed * moveDir;
        else
        {
            if (lastMoveDir == moveDir)
                rigidB.linearVelocityX = 0;
            else rigidB.linearVelocityX = RealSpeed * moveDir;
            lastMoveDir = moveDir;
        }
    }

    private bool JumpPressed;
    [SerializeField] private float jumpForce;
    private void JumpLogic()
    {
        JumpPressed = Input.GetKeyDown(KeyCode.Space);
        if (!in_air && JumpPressed)
        {
            rigidB.AddForceY(jumpForce, ForceMode2D.Impulse);
            JumpPressed = false;
        }
    }

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        moveDirection = Input.GetAxis("Horizontal");
        in_air = !on_ground && !on_platform && !on_moving_platform;
        JumpLogic();

        if ((on_ground || on_platform || on_moving_platform) && moveDirection != 0 && walkingSound != null && Time.time - lastWalkingSoundTime >= walkingSoundInterval)
        {
            Sounds.Sound(walkingSound, audioSource, volume, minPitch, maxPitch);
            lastWalkingSoundTime = Time.time;
        }
    }
    void FixedUpdate()
    {
        /*if (Physics2D.Raycast(this.transform.position, new Vector2(0, -1), this.transform.localScale.y / 2 + 0.2f).collider.CompareTag("Ground")
        && !in_air)
        Debug.DrawRay((this.transform.position,)*/
            MovementLogic(moveDirection);
    }
}
