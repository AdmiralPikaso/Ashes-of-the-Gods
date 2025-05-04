using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZoneScript : MonoBehaviour
{
    private GameObject tileZone;
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
    private List<GameObject> enemies = new List<GameObject>();
    private void Awake()
    {
        switch (gameObject.name)
        {
            case "Zone1":
                {
                    tileZone = GameObject.Find("TileZone1");
                    break;
                }
            case "Zone2":
                {
                    tileZone = GameObject.Find("TileZone2");
                    break;
                }
            case "Zone3":
                {
                    tileZone = GameObject.Find("TileZone3");
                    break;
                }
            default:
                break;
        }       
        enemies = GameObject.FindGameObjectsWithTag("Enemy").Where(x => x.transform.position.x > point1.position.x && x.transform.position.x < point2.position.x).ToList();
    }
    
    private bool open = false;

    private void Update()
    {
        tileZone.SetActive(!open);
        print(enemies.Count);
    }
    private void OnEnable()
    {
        print("Подписались");
        Enemy.DieCalled += CheckToZoneEmpty;
    }

    private void OnDisable()
    {
        Enemy.DieCalled -= CheckToZoneEmpty;
    }
    void CheckToZoneEmpty(int id)
    {
        enemies.RemoveAll(obj => obj.GetInstanceID() == id);
        open = enemies.Count == 0;
    }
}
