using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Entity : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float airSpeedMultiplier;

    public void MovementLogic(Rigidbody2D rigidB, Vector2 input, float speed)
    {

        rigidB.transform.position = new Vector2(rigidB.transform.position.x + input.x * speed * Time.deltaTime, rigidB.transform.position.y);
      //  if (input.x == 0 || input.y == 0)
            //rigidB.GetPointVelocity(new Vector2(0,0));
    }

    public void JumpLogic(Rigidbody2D rigidB, float jumpForce)
    {
        Vector2 moveDir = new Vector2 (0,jumpForce);
        rigidB.AddForce(moveDir,ForceMode2D.Impulse);
    }
    void Awake()
    {

    }

    void Update()
    {

    }
}
