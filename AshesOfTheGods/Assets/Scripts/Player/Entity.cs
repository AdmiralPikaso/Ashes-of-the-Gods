using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
public class Entity : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float airSpeedMultiplier;
    protected float moveDirection;

    protected bool on_ground;
    protected bool on_platform;
    protected bool in_air = false;

    /// <summary>
    /// Вызывать в FixedUpdate, в moveDir передавать ввод(Значение от -1 до 1), speed - скорость.
    /// </summary>
    /// <param name="rigidB"></param>
    /// <param name="moveDir"></param>
    /// <param name="speed"></param>
    public void MovementLogic(Rigidbody2D rigidB, float moveDir)
    {
        // rigidB.MovePosition(new Vector2(rigidB.position.x + moveDir * speed * Time.fixedDeltaTime, rigidB.position.y));
        rigidB.linearVelocityX = speed * moveDir;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 BordersCollisionCoordinates =
        new Vector2(collision.gameObject.transform.position.x - collision.gameObject.transform.localScale.x,
        collision.gameObject.transform.position.x + collision.gameObject.transform.localScale.x);

        Vector2 BordersObjectCoorditates =
        new Vector2(this.transform.position.x - this.transform.localScale.x,
        this.transform.position.x + this.transform.localScale.x);

        /* if (this.transform.position.y - this.transform.localScale.y > collision.transform.position.y
        && BordersObjectCoorditates.x > BordersCollisionCoordinates.x + 0.1e2
        && BordersObjectCoorditates.y < BordersCollisionCoordinates.y - 0.1e2
         )*/
        {
            if (collision.gameObject.CompareTag("Ground"))
                on_ground = true;
            if (collision.gameObject.CompareTag("Platform"))
                on_platform = true;

        }
        print($"{this.transform.position.y - this.transform.localScale.y} > {collision.transform.position.y + collision.gameObject.transform.localScale.y + 0.1e2}\n{BordersObjectCoorditates.x} > {BordersCollisionCoordinates.x} \n {BordersObjectCoorditates.y} < {BordersCollisionCoordinates.y}");
    }

    protected void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            on_ground = false;
        if (collision.gameObject.CompareTag("Platform"))
            on_platform = false;
    }

}
