using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonScript : MonoBehaviour
{
    Button button;
    [Space]
    [Header ("� ���������� ����� ������ ������������� ������")]
   
    [SerializeField] private int sceneBuildIndex;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(GameStart);
    }

    private void GameStart()
    {
        SceneManager.LoadScene(sceneBuildIndex); //�������� �����, ������� ���� ���������
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
