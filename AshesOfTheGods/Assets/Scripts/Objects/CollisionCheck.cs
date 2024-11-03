using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    public bool contact;
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            print("Контакт!");
            contact = true;
        }
    }
    private void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            print("Потерян!");
            contact = false;
        }
    }
}
