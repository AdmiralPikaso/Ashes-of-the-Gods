using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    Button button;
    [SerializeField] private GameObject escMenuBackground;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Continue);
    }

    [SerializeField] private GameObject player;
    private void Continue()
    {
        player.GetComponent<PlayerStats>().isEsc = false;
        escMenuBackground.SetActive(false);
        Time.timeScale = 1f;
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
