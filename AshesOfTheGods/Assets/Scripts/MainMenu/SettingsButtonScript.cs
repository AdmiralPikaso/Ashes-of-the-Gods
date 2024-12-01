using UnityEngine;
using UnityEngine.UI;
public class SettingsButtonScript : MonoBehaviour
{
    Button button;
    [SerializeField] private GameObject settingsMenu;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenSettings);
    }

    private void OpenSettings()
    {
        settingsMenu.SetActive(true);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
