using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelScript : MonoBehaviour
{
    private GameObject nextLevelPoint;
    private GameObject stribog;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextLevelPoint = GameObject.FindGameObjectWithTag("NextLevel");
        stribog = GameObject.Find("Stribog");
    }

    private bool flag = false;
    // Update is called once per frame
    void Update()
    {
        if (!flag && stribog.GetComponent<NewStribog>().isDead)
            flag = true;
         if (flag && gameObject.GetComponent<CapsuleCollider2D>().bounds.center.x > nextLevelPoint.transform.position.x)
            LoadSecondLevel();

    }

    private void LoadSecondLevel()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("LevelNumber", 2);
        SceneManager.LoadScene(PlayerPrefs.GetInt("LevelNumber"));

    }
}
