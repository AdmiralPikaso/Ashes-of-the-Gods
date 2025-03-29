using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartScript : MonoBehaviour
{
    Button button;
    
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Restart);
    }

    private void Restart()
    {
        //deathScreen.SetActive(false);
        SceneManager.LoadScene(PlayerPrefs.GetInt("LevelNumber"));
        
    }
    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
