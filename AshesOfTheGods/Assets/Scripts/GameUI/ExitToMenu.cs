using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitToMenuScript : MonoBehaviour
{
    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ExitToMenu);
    }

    private void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenu");//Название сцены, которую надо загрузить
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
