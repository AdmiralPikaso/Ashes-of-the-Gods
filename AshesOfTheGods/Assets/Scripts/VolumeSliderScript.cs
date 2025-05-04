using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class VolumeSliderScript : MonoBehaviour
{
    public class AudioSourceData
    {
        public AudioSource source;
        [HideInInspector] public float initialVolume;
    }

    

    private AudioSourceData[] audioSources;
    void Awake()
    {
        if (!flag)
        {
            AudioSource[] sources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
            audioSources = new AudioSourceData[sources.Length];
            for (int i = 0; i < sources.Length; i++)
            {
                Debug.Log(sources[i].volume);
                audioSources[i] = new AudioSourceData
                {
                    
                    source = sources[i],
                    initialVolume = sources[i].volume
                };
            }

            
            Debug.Log(audioSources.Length);
            InitializeSlider();
            flag = true;
        }
    }

    private bool flag = false;
    private void Update()
    {
       
    }


    [SerializeField] Slider volumeSlider;
    
    private void InitializeSlider()
    {
        volumeSlider.minValue = 0f;
        volumeSlider.maxValue = 1f;
        volumeSlider.value = 1f;
        volumeSlider.onValueChanged.AddListener(ApplyVolume);
    }


    private void ApplyVolume(float sliderVolume)
    {
        foreach (var data in audioSources)
        {
            if (data.source != null)
                data.source.volume = data.initialVolume * sliderVolume;
        }
    }
}
