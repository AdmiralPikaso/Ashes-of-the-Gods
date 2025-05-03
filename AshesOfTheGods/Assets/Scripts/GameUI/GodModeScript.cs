using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
public class GodModeScript : MonoBehaviour
{
    GameObject player;
    private List<GameObject> horizontalThorns = new List<GameObject>();   
    private List<GameObject> verticalThorns = new List<GameObject>();   
    private List<GameObject> allThorns = new List<GameObject>();
    private List<GameObject> Lava = new List<GameObject>();
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (SceneManager.GetActiveScene().name == "True_First_Level")
        {
            allThorns = GameObject.FindGameObjectsWithTag("Death").ToList<GameObject>();

            foreach (GameObject p in allThorns)
            {
                print(p);
                if (p.name == "WallThorn")
                    verticalThorns.Add(p);
                if (p.name == "Thorn")
                    horizontalThorns.Add(p);
            }
        }
        if(SceneManager.GetActiveScene().name == "True_Second_Level")
        {
            Lava = GameObject.FindGameObjectsWithTag("Death").Where(obj => obj.name == "TileDeath").ToList<GameObject>();
        }
        GodModeToogle();
    }

    public void GodModeToogle()
    {
        player.GetComponent<PlayerStats>().godMode = gameObject.GetComponent<Toggle>().isOn;
        if (SceneManager.GetActiveScene().name == "True_First_Level")
        {
            foreach (GameObject p in horizontalThorns)
                p.tag = gameObject.GetComponent<Toggle>().isOn ? "Ground" : "Death";
            foreach (GameObject p in verticalThorns)
                p.gameObject.GetComponent<BoxCollider2D>().enabled = !gameObject.GetComponent<Toggle>().isOn;
                //p.tag = gameObject.GetComponent<Toggle>().isOn ? "Wall" : "Death";
        }
        if (SceneManager.GetActiveScene().name == "True_Second_Level")
        {
            Lava[0].tag = gameObject.GetComponent<Toggle>().isOn ? "Ground" : "Death";
        }
    }
}
