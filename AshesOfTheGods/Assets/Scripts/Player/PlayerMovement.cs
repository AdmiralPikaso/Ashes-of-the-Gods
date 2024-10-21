using UnityEngine;

public class PlayerMovement : Player
{
    void Awake()
    {
        rigidB = GetComponent<Rigidbody2D>();
    }

    private Vector2 HorInput;
    void Update()
    {
        HorInput = new Vector2(Input.GetAxis("Horizontal"), 0);
    }

    void FixedUpdate()
    {
        Movement(rigidB, HorInput);
    }
}
