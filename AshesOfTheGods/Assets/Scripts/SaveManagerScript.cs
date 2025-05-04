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

    
    
    private bool IsSaved1 = false;
    private bool IsSaved2 = false;

    private Transform SkyPos;
   // private Transform MountainsPos;
    private void Awake()
    {
        //if (!(PlayerPrefs.GetInt("LevelNumber")  == 3))
            SkyPos = GameObject.FindGameObjectWithTag("Sky").transform;
       // MountainsPos = GameObject.FindGameObjectWithTag("Mountains").transform;
        
    }
    private void Start()
    {
        
        Debug.Log($"{PlayerPrefs.HasKey(POS_X_KEY) & PlayerPrefs.HasKey(POS_Y_KEY) & PlayerPrefs.HasKey(POS_Z_KEY)}");
        if (PlayerPrefs.HasKey(POS_X_KEY) & PlayerPrefs.HasKey(POS_Y_KEY) & PlayerPrefs.HasKey(POS_Z_KEY) & PlayerPrefs.HasKey(Sky_X_KEY) & PlayerPrefs.HasKey(Sky_Y_KEY) & PlayerPrefs.HasKey(Sky_Z_KEY))
        {
            transform.position = LoadPlayerPosition();
            SkyPos.position = LoadSkyPosition();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Тригернуло");
        if (collision.gameObject.CompareTag("SavePoint1") && !IsSaved1)
        {
            SavePlayerPosition(transform.position, SkyPos.position);
            IsSaved1 = true;
        }
        else if (collision.gameObject.CompareTag("SavePoint2") && !IsSaved2)
        {
            SavePlayerPosition(transform.position, SkyPos.position);
            IsSaved2 = true;
        }
    }

    private void Update()
    {
        
    }


    public void SavePlayerPosition(Vector3 pos, Vector3 SkyPos)
    { 
        PlayerPrefs.SetFloat(POS_X_KEY, pos.x);
        PlayerPrefs.SetFloat(POS_Y_KEY, pos.y);
        PlayerPrefs.SetFloat(POS_Z_KEY, pos.z);

        PlayerPrefs.SetFloat(Sky_X_KEY, SkyPos.x);
        PlayerPrefs.SetFloat(Sky_Y_KEY, SkyPos.y);
        PlayerPrefs.SetFloat(Sky_Z_KEY, SkyPos.z);

        PlayerPrefs.Save();
        Debug.Log("Saved");
        Debug.Log($"Sky Position {SkyPos.x} {SkyPos.y} {SkyPos.z}");


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
        Debug.Log($"Sky Position loaded: {loadedSkyPosition}");
        return loadedSkyPosition;
    }
}
