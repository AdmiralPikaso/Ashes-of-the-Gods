using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject escMenu;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject settingsMenu;

    private void Start()
    {
        Time.timeScale = 1f;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) & !player.GetComponent<PlayerStats>().isEsc)
            OpenEscMenu();
    }

    [SerializeField] private GameObject player;
    private void OpenEscMenu()
    {
        if (deathScreen.activeSelf == false & settingsMenu.activeSelf == false)
        {
            escMenu.SetActive(!escMenu.activeSelf);
            if (escMenu.activeSelf == true)
            {
                player.GetComponent<PlayerStats>().isEsc = true;
                Time.timeScale = 0f;
            }
            else
            {
                player.GetComponent<PlayerStats>().isEsc = false;
                Time.timeScale = 1f;
            }
        }
    }
}
