using UnityEngine;
using UnityEngine.UI;

public class GodModeScript : MonoBehaviour
{
    GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GodModeToogle();
    }

    public void GodModeToogle()
    {
        
        player.GetComponent<PlayerStats>().godMode = gameObject.GetComponent<Toggle>().isOn;
    }
}
