using UnityEngine;
using UnityEngine.InputSystem;

public class Entity : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float airSpeedMultiplier;


    /// <summary>
    /// Использовать только с FixedUpdate. При вызове осуществляет передвежение объекта по x
    /// </summary>
    /// <param name="rigidB"></param>
    /// <param name="speed"></param>
    /// <param name="airSpeedMultiplier"></param>
    public void Movement(Rigidbody2D rigidB, Vector2 input)
    {
        rigidB.MovePosition(rigidB.position + input * speed * Time.fixedDeltaTime);
    }

    void Awake()
    {

    }

    void Update()
    {

    }
}
