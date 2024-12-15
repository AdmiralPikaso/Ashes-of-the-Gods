using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenMainMenu : MonoBehaviour
{
    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(MainMenu);
    }

    private void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");//Название сцены, которую надо загрузить
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
