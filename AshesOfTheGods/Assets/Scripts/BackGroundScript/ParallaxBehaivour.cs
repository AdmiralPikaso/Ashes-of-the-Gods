using Mono.Cecil;
using UnityEngine;
using UnityEngine.AI;

public class ParallaxBehaivour : MonoBehaviour
{
    [SerializeField] Transform followingTarget;
    [SerializeField, Range(0f, 1f)] float parallaxStenght = 0.1f;
    [SerializeField] bool disableVerticalParallax;
    Vector3 targetPrevPos;
    void Start()
    {
        if (!followingTarget)
            followingTarget = Camera.main.transform;
        targetPrevPos = followingTarget.position;
        //print(targetPrevPos);
    }
    void FixedUpdate()
    {
        var delta = followingTarget.position - targetPrevPos;
        if (disableVerticalParallax)
            delta.y = 0;
        targetPrevPos = followingTarget.position;
        //transform.Translate(new Vector3(delta.x * parallaxStenght,delta.y * parallaxStenght,0));
        transform.position = new Vector3(transform.position.x + delta.x * parallaxStenght,transform.position.y + delta.y,10);
        //print(transform.position);
    }
}
