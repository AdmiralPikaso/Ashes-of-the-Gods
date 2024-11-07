using UnityEngine;
using UnityEngine.Android;

public class SemiTranspPlatform : MonoBehaviour
{
    private BoxCollider2D coll;
    private Transform PlayerTransform;
    [SerializeField] private LayerMask satisMask;
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
        print($"{PlayerTransform.position.y - PlayerTransform.localScale.y}, {this.transform.position.y + this.transform.localScale.y}");
        if (Input.GetAxis("Vertical") < 0
        && Physics2D.OverlapCircle(this.transform.position, this.transform.localScale.x, satisMask)
        && PlayerTransform.position.y - PlayerTransform.localScale.y > this.transform.position.y + this.transform.localScale.y)
            coll.enabled = false;
        else if (Physics2D.OverlapCircle(this.transform.position, this.transform.localScale.x, satisMask) &&
         PlayerTransform.position.y - PlayerTransform.localScale.y > this.transform.position.y + this.transform.localScale.y)
        {
            coll.enabled = true;
        }
        else
            coll.enabled = false;

    }
}
