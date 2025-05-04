using UnityEngine;
using UnityEngine.Tilemaps;

public class ActiveScript : MonoBehaviour
{
    private StribogScript script;
    void Start()
    {
        script = GameObject.Find("Stribog").GetComponent<StribogScript>();
    }   
    void Update()
    {
        this.gameObject.GetComponent<TilemapRenderer>().enabled = !script.getActive();
    }
}
