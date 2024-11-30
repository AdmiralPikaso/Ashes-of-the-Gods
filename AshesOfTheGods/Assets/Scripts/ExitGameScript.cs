using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitGameScript : MonoBehaviour
{
    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ExitGame);
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
