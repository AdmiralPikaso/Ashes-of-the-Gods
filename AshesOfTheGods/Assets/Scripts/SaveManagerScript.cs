using UnityEngine;
using UnityEngine.Rendering.LookDev;

public class SaveManagerScript : MonoBehaviour
{
    private const string POS_X_KEY = "PlayerPosX";
    private const string POS_Y_KEY = "PlayerPosY";
    private const string POS_Z_KEY = "PlayerPosZ";
    
    private const string Sky_X_KEY = "SkyPosX";
    private const string Sky_Y_KEY = "SkyPosY";
    private const string Sky_Z_KEY = "SkyPosZ";

    private GameObject savePoint;
    
    private bool IsSaved = false;

    private Transform SkyPos;
    private void Awake()
    {
        SkyPos = GameObject.Find("Sky").transform;
    }
    private void Start()
    {
        savePoint = GameObject.FindGameObjectWithTag("SavePoint");
        Debug.Log($"{PlayerPrefs.HasKey(POS_X_KEY) & PlayerPrefs.HasKey(POS_Y_KEY) & PlayerPrefs.HasKey(POS_Z_KEY)}");
        if (PlayerPrefs.HasKey(POS_X_KEY) & PlayerPrefs.HasKey(POS_Y_KEY) & PlayerPrefs.HasKey(POS_Z_KEY) & PlayerPrefs.HasKey(Sky_X_KEY) & PlayerPrefs.HasKey(Sky_Y_KEY) & PlayerPrefs.HasKey(Sky_Z_KEY))
        {
            transform.position = LoadPlayerPosition();
            SkyPos.position = LoadSkyPosition();
        }
    }

    private void Update()
    {
        if (transform.position.x >= savePoint.transform.position.x & !IsSaved)
        { 
            SavePlayerPosition(transform.position,SkyPos.position);
            IsSaved = true;
        }
    }


    public void SavePlayerPosition(Vector3 pos, Vector3 SkyPos)
    { 
        PlayerPrefs.SetFloat(POS_X_KEY, pos.x);
        PlayerPrefs.SetFloat(POS_Y_KEY, pos.y);
        PlayerPrefs.SetFloat(POS_Z_KEY, pos.z);

        PlayerPrefs.SetFloat(Sky_X_KEY, pos.x);
        PlayerPrefs.SetFloat(Sky_Y_KEY, pos.y);
        PlayerPrefs.SetFloat(Sky_Z_KEY, pos.z);

        PlayerPrefs.Save();
        Debug.Log("Saved");


    }

    public Vector3 LoadPlayerPosition()
    {
        float x = PlayerPrefs.GetFloat(POS_X_KEY, 0f); // 0 - �������� �� ���������
        float y = PlayerPrefs.GetFloat(POS_Y_KEY, 0f);
        float z = PlayerPrefs.GetFloat(POS_Z_KEY, 0f);
        Vector3 loadedPosition = new Vector3(x, y, z);
        Debug.Log($"Position loaded: {loadedPosition}");
        return loadedPosition;
    }
    public Vector3 LoadSkyPosition()
    {
        float Skyx = PlayerPrefs.GetFloat(Sky_X_KEY, 0f);
        float Skyy = PlayerPrefs.GetFloat(Sky_Y_KEY, 0f);
        float Skyz = PlayerPrefs.GetFloat(Sky_Z_KEY, 0f);

        Vector3 loadedSkyPosition = new Vector3(Skyx, Skyy, Skyz);

        return loadedSkyPosition;
    }
}
