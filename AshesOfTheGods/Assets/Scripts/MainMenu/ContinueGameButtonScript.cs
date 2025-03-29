using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueGameButtonScript : MonoBehaviour
{
    Button button;
    
    
   
    
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(GameContinue);
    }

    
    private void GameContinue()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("LevelNumber"));
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
