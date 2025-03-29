using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSecondLevelScript : MonoBehaviour
{
    Button button;
    
    
   
    
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(GameStart);
    }

    
    private void GameStart()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("LevelNumber", 2);
        SceneManager.LoadScene(PlayerPrefs.GetInt("LevelNumber"));
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
