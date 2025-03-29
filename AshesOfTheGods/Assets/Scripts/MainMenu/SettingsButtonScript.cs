using UnityEngine;
using UnityEngine.UI;
public class SettingsButtonScript : MonoBehaviour
{
    Button button;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject levelManagerWindow;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenSettings);
    }

    private void OpenSettings()
    {
        levelManagerWindow.SetActive(false);
        settingsMenu.SetActive(true);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
