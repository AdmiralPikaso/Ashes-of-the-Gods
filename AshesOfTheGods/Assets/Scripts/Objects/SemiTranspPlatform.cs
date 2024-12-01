using UnityEngine;
public class SemiTranspPlatform : MonoBehaviour
{
    private BoxCollider2D coll;
    private Transform PlayerTransform;
    [SerializeField] private LayerMask satisMask;
    private bool playerContact;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
            playerContact = true;
    }
    private void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
            playerContact = false;
    }
    void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        InclusionLogic();
    }

    void InclusionLogic()
    {
       // print($"{PlayerTransform.position.y - PlayerTransform.localScale.y / 2.0f}, {this.transform.position.y + this.transform.localScale.y / 2.0f}");

        if (Physics2D.OverlapCircle(this.transform.position, this.transform.localScale.x, satisMask))
        {
            if (this.transform.position.y + this.transform.localScale.y / 2.0f < PlayerTransform.position.y - PlayerTransform.localScale.y / 2.0f)
                coll.enabled = (Input.GetAxis("Vertical") < 0) ? (playerContact ? false : coll.enabled) : true;
            else
                coll.enabled = false;
        }
        else
            coll.enabled = false;

    }
}
