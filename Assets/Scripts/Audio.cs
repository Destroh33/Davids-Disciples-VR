using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Audio : MonoBehaviour
{
    [SerializeField] Slider volumeSlider; 
    [SerializeField] Slider BGMSlider;
    private static Audio instance;  
    private AudioSource bgMusic; 

    void Start()
    {
        volumeSlider.value = 1f;  
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        BGMSlider.onValueChanged.AddListener(OnBGMSChanged);

        bgMusic = GameObject.Find("Audio Source").GetComponent<AudioSource>(); 
        BGMSlider.value = 1f;
    }

    private void OnVolumeChanged(float volume)
    {  
        foreach (Transform child in transform)
        {
            AudioSource childAudioSource = child.GetComponent<AudioSource>(); 
            
            if (childAudioSource != null)
            {
                if (child.CompareTag("SFX"))
                {
                childAudioSource.volume = volume; 
                } 
            }
        }
    }

    private void OnBGMSChanged(float volume)
    {
        foreach (Transform child in transform)
        {
            AudioSource childAudioSource = child.GetComponent<AudioSource>(); 
            
            if (childAudioSource != null)
            {
                if (child.CompareTag("BGM"))
                {
                childAudioSource.volume = volume; 
                } 
            }
        }

    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }


    void Update(){
        if (bgMusic != null && SceneManager.GetActiveScene().name == "MainMap")
        {
            // Debug.Log("in is audio update");
            if (!bgMusic.isPlaying)
            {
                bgMusic.Play();  
            }
        }
        if(SceneManager.GetActiveScene().name != "MainMap")
        {
            // Debug.Log("in is audio update2 " );
            if(bgMusic.isPlaying){
                 bgMusic.Stop();
            }
        }
    }
}

