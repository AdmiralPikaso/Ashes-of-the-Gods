using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject escMenu;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject player;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OpenEscMenu();
    }
    private void OpenEscMenu()
    {
        if (deathScreen.activeSelf == false & settingsMenu.activeSelf == false)
        {
            escMenu.SetActive(!escMenu.activeSelf);
            if (escMenu.activeSelf == true)
            {
                Time.timeScale = 0f;
                player.GetComponent<PlayerStats>().IsEsc = true;
            }
            else
            {
                player.GetComponent<PlayerStats>().IsEsc = false;
                Time.timeScale = 1f;
            }
        }
    }
}
