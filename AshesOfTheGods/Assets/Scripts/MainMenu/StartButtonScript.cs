using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonScript : MonoBehaviour
{
    Button button;
    [Space]
    [Header ("в настройках билда сценам присваиваются номера")]
   
    [SerializeField] private int sceneBuildIndex;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(GameStart);
    }

    private void GameStart()
    {
        SceneManager.LoadScene(sceneBuildIndex); //Название сцены, которую надо загрузить
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
