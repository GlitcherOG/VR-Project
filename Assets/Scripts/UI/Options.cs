using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    #region UI Elements
    public Toggle muteToggle;
    public AudioMixer masterAudio;
    public Slider musicSlider;
    public Slider SFXSlider;
    #endregion

   

    // Start is called before the first frame update
    void Start()
    {
        LoadPlayerPrefs();
    }
    
    #region Change Settings
    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    //This changes volume in options
    public void SetMusicVolume(float MusicVol)
    {
        masterAudio.SetFloat("MusicVol", MusicVol);
    }
    //This changes sound effects volume 
    public void SetSFXVolume(float SFXVol)
    {
        masterAudio.SetFloat("SFXVol", SFXVol);
    }

    //Function to mute volume when toggle is active
    public void ToggleMute(bool isMuted)
    {
        //string reference isMuted connects to the AudioMixer master group Volume and isMuted parameters in Unity
        if (isMuted)
        {
            //-40 is the minimum volume
            masterAudio.SetFloat("isMutedVolume", -40);
        }
        else
        {
            //0 is the maximum volume
            masterAudio.SetFloat("isMutedVolume", 0);
        }
    }
    #endregion

    #region Save Prefs
    public void SavePlayerPrefs()
    {
        //save audio sliders
        float musicVol;
        if (masterAudio.GetFloat("MusicVol", out musicVol))
        {
            PlayerPrefs.SetFloat("MusicVol", musicVol);
        }
        float SFXVol;
        if (masterAudio.GetFloat("SFXVol", out SFXVol))
        {
            PlayerPrefs.SetFloat("SFXVol", SFXVol);
        }

        PlayerPrefs.Save();
    }
    #endregion

    #region Load Prefs
    public void LoadPlayerPrefs()
    {
        //load audio Sliders
        if (PlayerPrefs.HasKey("MusicVol"))
        {
            float musicVol = PlayerPrefs.GetFloat("MusicVol");
            musicSlider.value = musicVol;
            masterAudio.SetFloat("MusicVol", musicVol);
        }
        if (PlayerPrefs.HasKey("SFXVol"))
        {
            float SFXVol = PlayerPrefs.GetFloat("SFXVol");
            SFXSlider.value = SFXVol;
            masterAudio.SetFloat("SFXVol", SFXVol);
        }
    }
    #endregion
}

