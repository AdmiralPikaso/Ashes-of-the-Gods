using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonScript : MonoBehaviour
{
    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(GameStart);
    }

    private void GameStart()
    {
        SceneManager.LoadScene("PlayerDeath");//�������� �����, ������� ���� ���������
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
