using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : Player
{
    void Awake()
    {
        rigidB = GetComponent<Rigidbody2D>();
    }

    private bool JumpPressed;

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

        MovementLogic(rigidB, moveDirection);
    }

}
