using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonScript : MonoBehaviour
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
        PlayerPrefs.SetInt("LevelNumber", 1);
        SceneManager.LoadScene(PlayerPrefs.GetInt("LevelNumber"));
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
