using System.Collections;
using UnityEngine;

public class ParallaxBehaivour : MonoBehaviour
{
    [SerializeField] Transform followingTarget;
    [SerializeField, Range(0f, 1f)] float parallaxStenght = 0.1f;
    [SerializeField] bool disableVerticalParallax;
    Vector3 targetPrevPos;
    private bool wantOpen = false;
    void Start()
    {
        StartCoroutine(Delay());
        wantOpen = true;
        
        //print(targetPrevPos);
    }
    void FixedUpdate()
    {
        if (!wantOpen)
        {
            var delta = followingTarget.position - targetPrevPos;
            if (disableVerticalParallax)
                delta.y = 0;
            targetPrevPos = followingTarget.position;
            //transform.Translate(new Vector3(delta.x * parallaxStenght,delta.y * parallaxStenght,0));
            transform.position = new Vector3(transform.position.x + delta.x * parallaxStenght, transform.position.y + delta.y, 10);
            //print(transform.position);
        }
    }

    private IEnumerator Delay()
    {
        while (true)
        {
            if (wantOpen)
            {
                yield return new WaitForSeconds(0.1f);
                wantOpen = false;

                if (!followingTarget)
                    followingTarget = Camera.main.transform;
                targetPrevPos = followingTarget.position;

            }
            yield return new WaitForEndOfFrame();
        }
    }
}

