using UnityEngine;
using UnityEngine.UI;

public class CloseSettingsButton : MonoBehaviour
{
    Button button;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject levelManagerWindow;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ExitSettingsMenu);
    }

    private void ExitSettingsMenu()
    {
        settingsMenu.SetActive(false);
        levelManagerWindow.SetActive(true);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
