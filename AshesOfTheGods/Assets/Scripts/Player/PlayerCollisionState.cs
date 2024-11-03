using UnityEngine;

public class PlayerCollisionState : MonoBehaviour
{
    private bool on_ground = false;
    private bool on_platform = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.CompareTag("Ground"))
            on_ground = true;
        if (collisionObject.CompareTag("Platform"))
            on_platform = true;

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.CompareTag("Ground"))
            on_ground = false;
        if (collisionObject.CompareTag("Platform"))
            on_platform = false;

    }

    public bool GetOnPlatform()
    {
        return on_platform;
    }
}
