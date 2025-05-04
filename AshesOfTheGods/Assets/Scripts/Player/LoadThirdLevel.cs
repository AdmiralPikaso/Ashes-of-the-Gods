using System.Security.Cryptography;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadThirdLevel : MonoBehaviour
{
    private GameObject nextLevelPoint;
    private GameObject Simargl;
    void Start()
    {
            Simargl = GameObject.Find("Simargl");
            nextLevelPoint = GameObject.FindGameObjectWithTag("NextLevel");
                
    }

    private bool flag = false;
    void Update()
    {
        if(!flag && Simargl.GetComponent<Enemy>().isDead)
            flag = true;
        if (flag && gameObject.GetComponent<CapsuleCollider2D>().bounds.center.x > nextLevelPoint.transform.position.x)
            LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("LevelNumber", 3);
        SceneManager.LoadScene(PlayerPrefs.GetInt("LevelNumber"));
    }
}
