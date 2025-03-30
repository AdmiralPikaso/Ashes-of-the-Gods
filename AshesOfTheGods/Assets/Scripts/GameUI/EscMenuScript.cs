using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class EscMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject escMenu;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject settingsMenu;

    private void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine(Delay());
    }

    private bool wantOpen = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) & !player.GetComponent<PlayerStats>().isEsc)
            wantOpen = true;
        if (Input.GetKeyDown(KeyCode.Escape) & player.GetComponent<PlayerStats>().isEsc)
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

    private IEnumerator Delay()
    {
        while (true)
        {
            if (wantOpen)
            {
                yield return new WaitForSeconds(0.1f);
                wantOpen = false;
                OpenEscMenu();
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
