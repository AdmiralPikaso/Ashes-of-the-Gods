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

    /// <summary>
    /// Вызывать в FixedUpdate, в moveDir передавать ввод(Значение от -1 до 1), speed - скорость.
    /// </summary>
    /// <param name="rigidB"></param>
    /// <param name="moveDir"></param>
    /// <param name="speed"></param>
    public void MovementLogic(Rigidbody2D rigidB, float moveDir)
    {
        if (!in_wall)
            rigidB.linearVelocityX = speed * moveDir;
        else
        {
            if (lastMoveDir == moveDir)
                rigidB.linearVelocityX = 0;
            else rigidB.linearVelocityX = speed * moveDir;
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
        if (collisionObject.CompareTag("Wall"))
        {
            in_wall = true;
        }
        print(in_wall);
    }

    protected void OnCollisionExit2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.CompareTag("Ground"))
            on_ground = false;
        if (collisionObject.CompareTag("Platform"))
            on_platform = false;
        if (collisionObject.CompareTag("Wall"))
            in_wall = false;
        print(in_wall);
    }

}
