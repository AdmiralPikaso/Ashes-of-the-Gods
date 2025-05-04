using UnityEngine;

public class PerunBodyMoveScript : MonoBehaviour
{
    
    Vector3 lastMHposition;
    private void Start()
    {
        lastMHposition = meleeHand.transform.position;
    }

    private Vector3 positionDelta;

    [SerializeField] private GameObject meleeHand;
    [SerializeField] private float speed = 13f;

    [SerializeField] private GameObject meleeArm1;
    [SerializeField] private GameObject meleeArm2;

    [SerializeField] private GameObject rangeArm1;
    [SerializeField] private GameObject rangeArm2;

    [SerializeField] private GameObject rangeHand;
    [SerializeField] private GameObject head;
    [SerializeField] private GameObject greenBack;
    [SerializeField] private GameObject coat;
    private void FixedUpdate()
    {
        //Body movement behind meleeHand
        positionDelta = meleeHand.transform.position - lastMHposition;
        MeleeHandMove();

        positionDelta.y = 0f;
        positionDelta.z = 0f;
        
        BodyMove(gameObject);        
        
        BodyMove(meleeArm2);

        BodyMove(rangeArm1);
        BodyMove(rangeArm2);
        BodyMove(rangeHand);
        
        BodyMove(head);
        BodyMove(greenBack);
        BodyMove(coat);
        lastMHposition = meleeHand.transform.position;
        //Body movement behind meleeHand

    }

    private void BodyMove(GameObject slave)
    {
        Vector3 newPos = slave.transform.position + positionDelta;
        newPos.y = slave.transform.position.y;
        newPos.z = slave.transform.position.z;
        slave.transform.position = Vector3.MoveTowards(slave.transform.position, newPos, Time.fixedDeltaTime * speed);
    }

    private void MeleeHandMove()
    { 
        Vector3 newPos = meleeArm1.transform.position + positionDelta;
        meleeArm1.transform.position = Vector3.MoveTowards(meleeArm1.transform.position, newPos, Time.fixedDeltaTime * speed);
    }
}
