using UnityEngine;

public class HintActive : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    [SerializeField] private GameObject hint1;
    [SerializeField] private GameObject hint2;
    void Update()
    {
        if (Vector2.Distance(gameObject.transform.position, hint1.transform.position) <= Vector2.Distance(gameObject.transform.position, hint2.transform.position))
        {
            hint1.SetActive(true);
            hint2.SetActive(false);
        }
        else
        { 
            hint2.SetActive(true);
            hint1.SetActive(false);
        }
    }
}
