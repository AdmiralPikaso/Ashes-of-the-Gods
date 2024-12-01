using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    Button button;
    [SerializeField] private GameObject settingsMenu;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Continue);
    }

    private void Continue()
    {
        settingsMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
