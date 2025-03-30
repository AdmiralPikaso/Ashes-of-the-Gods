using UnityEngine;

public class SaveManagerScript : MonoBehaviour
{
    private const string POS_X_KEY = "PlayerPosX";
    private const string POS_Y_KEY = "PlayerPosY";
    private const string POS_Z_KEY = "PlayerPosZ";
    
    
    private GameObject savePoint;
    
    private bool IsSaved = false;

    private void Awake()
    {
        
    }
    private void Start()
    {
        savePoint = GameObject.FindGameObjectWithTag("SavePoint");
        Debug.Log($"{PlayerPrefs.HasKey(POS_X_KEY) & PlayerPrefs.HasKey(POS_Y_KEY) & PlayerPrefs.HasKey(POS_Z_KEY)}");
        if (PlayerPrefs.HasKey(POS_X_KEY) & PlayerPrefs.HasKey(POS_Y_KEY) & PlayerPrefs.HasKey(POS_Z_KEY))
            transform.position = LoadPlayerPosition();
    }

    private void Update()
    {
        if (transform.position.x >= savePoint.transform.position.x & !IsSaved)
        { 
            SavePlayerPosition(transform.position);
            IsSaved = true;
        }
    }


    public void SavePlayerPosition(Vector3 pos)
    { 
        PlayerPrefs.SetFloat(POS_X_KEY, pos.x);
        PlayerPrefs.SetFloat(POS_Y_KEY, pos.y);
        PlayerPrefs.SetFloat(POS_Z_KEY, pos.z);
        PlayerPrefs.Save();
        Debug.Log("Saved");


    }

    public Vector3 LoadPlayerPosition()
    {
        float x = PlayerPrefs.GetFloat(POS_X_KEY, 0f); // 0 - значение по умолчанию
        float y = PlayerPrefs.GetFloat(POS_Y_KEY, 0f);
        float z = PlayerPrefs.GetFloat(POS_Z_KEY, 0f);

        Vector3 loadedPosition = new Vector3(x, y, z);
        Debug.Log($"Position loaded: {loadedPosition}");

        return loadedPosition;
    }
}
