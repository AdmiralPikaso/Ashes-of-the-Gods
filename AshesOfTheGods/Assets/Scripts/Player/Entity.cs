using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
public class Entity : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float airSpeedMultiplier;
    protected float moveDirection;

    protected bool on_ground = false;
    protected bool on_platform = false;
    protected bool in_air = false;
    protected bool in_wall = false;

    protected float lastMoveDir = 0;

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
        //print(in_wall);
        //print(rigidB.linearVelocityX);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.CompareTag("Ground"))
            on_ground = true;
        if (collisionObject.CompareTag("Platform"))
            on_platform = true;
        if (collisionObject.CompareTag("PlatformDown"))
        {
            on_platform = false;
        }
        if (collisionObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
        {
            in_wall = true;
        }

    }

    protected void OnCollisionExit2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.CompareTag("Ground"))
            on_ground = false;
        if (collisionObject.CompareTag("Platform"))
            on_platform = false;
        if (collisionObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
            in_wall = false;

    }

}
