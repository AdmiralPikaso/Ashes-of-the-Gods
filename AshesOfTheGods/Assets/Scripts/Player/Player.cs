using Unity.VisualScripting;
using UnityEngine;

public class Player : Entity
{
    protected Rigidbody2D rigidB;
    [SerializeField] protected float jumpForce;
}
