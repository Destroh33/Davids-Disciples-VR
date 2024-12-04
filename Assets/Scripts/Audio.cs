using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Audio : MonoBehaviour
{
    [SerializeField] Slider volumeSlider; 
    private static Audio instance;  
    private AudioSource bgMusic; 

    void Start()
    {
        volumeSlider.value = AudioListener.volume;  
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

        bgMusic = GameObject.Find("Audio Source").GetComponent<AudioSource>(); 

        UpdateAudioVolumes();
    }

    private void OnVolumeChanged(float volume)
    {
        AudioListener.volume = volume;  
        UpdateAudioVolumes();  
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

    private void UpdateAudioVolumes()
    {
        foreach (Transform child in transform)
        {
            AudioSource childAudioSource = child.GetComponent<AudioSource>(); 
            
            if (childAudioSource != null)
            {
                childAudioSource.volume = AudioListener.volume; 
            }
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

