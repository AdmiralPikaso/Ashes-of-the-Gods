using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private bool on_ground = false;
    private bool on_platform = false;
    private bool in_air = false;
    private bool in_wall = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.CompareTag("Ground"))
            on_ground = true;
        if (collisionObject.CompareTag("Platform"))
            on_platform = true;
        if (collisionObject.CompareTag("PlatformDown"))
            on_platform = false;
        if (collisionObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
            in_wall = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.CompareTag("Ground"))
            on_ground = false;
        if (collisionObject.CompareTag("Platform"))
            on_platform = false;
        if (collisionObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
            in_wall = false;
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

    public void MovementLogic(Rigidbody2D rigidB, float moveDir)
    {
        float RealSpeed = speed;
        if (in_air)
            RealSpeed *= airSpeedMultiplier;
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
    void Update()
    {
        moveDirection = Input.GetAxis("Horizontal");
        in_air = !on_ground && !on_platform;
        JumpLogic();
    }
    void FixedUpdate()
    {
        //print($"{in_wall}, {on_ground},{on_platform}"); //if you need debug colission uncommnet it
        MovementLogic(rigidB, moveDirection);
    }
}