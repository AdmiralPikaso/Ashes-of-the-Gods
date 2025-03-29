using UnityEngine;

public class ChooseLevelButtonScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [SerializeField] GameObject chooseLevelWindow;
    public void ChooseLevel()
    { 
        chooseLevelWindow.SetActive(true);
        this.transform.parent.gameObject.SetActive(false);
    }
}
