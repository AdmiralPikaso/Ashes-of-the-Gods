using UnityEngine;
using TMPro;
using NUnit;

public class fpsScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private float timeLeft = 0f;
    private float accum = 0f;
    private int frames = 0;
    // Update is called once per frame
    void Update()
    {
        timeLeft += Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        frames++;

        if (timeLeft >= 0.5f)
        {
            float fps = accum / frames;
            gameObject.GetComponent<TextMeshProUGUI> ().text = $"FPS = {fps}";
            accum = 0f;
            frames = 0;
            timeLeft = 0f;
        }
        
    }
}
