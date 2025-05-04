using UnityEngine;
using UnityEngine.Tilemaps;

public class FirstLevelHelperACtivates : MonoBehaviour
{
    private SimarglScript script;
    void Start()
    {
        script = GameObject.Find("Simargl").GetComponent<SimarglScript>();
    }
    void Update()
    {
        this.gameObject.GetComponent<TilemapRenderer>().enabled = !script.IsActive;
    }
}
