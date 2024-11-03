using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float platformSpeed;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    private Transform targetTransform;

    void Start()
    {
        targetTransform = pointB;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetTransform.position, platformSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, targetTransform.position) < 0.1f)
        {
            targetTransform = (targetTransform == pointA) ? pointB : pointA;
        }
    }
}