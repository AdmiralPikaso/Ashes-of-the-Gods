using UnityEngine;

public class GameStartButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject levelManager;
    [SerializeField] private GameObject settingsMenu;
    public void OpenLevelManager()
    {
        settingsMenu.SetActive(false);
        levelManager.SetActive(true);
    }
}
