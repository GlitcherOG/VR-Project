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

    public void AddMusicVolume(float MusicVol)
    {
        float Temp;
        masterAudio.GetFloat("MusicVol", out Temp);
        Temp += MusicVol;
        masterAudio.SetFloat("MusicVol", Temp);
    }
    //This changes sound effects volume 
    public void SetSFXVolume(float SFXVol)
    {
        masterAudio.SetFloat("SFXVol", SFXVol);
    }

    public void AddSFXVolume(float SFXVol)
    {
        float Temp;
        masterAudio.GetFloat("SFXVol", out Temp);
        Temp += SFXVol;
        masterAudio.SetFloat("SFXVol", Temp);
    }
    //Function to mute volume when toggle is active
    public void ToggleMute()
    {
        muteToggle.isOn = !muteToggle.isOn;
        //string reference isMuted connects to the AudioMixer master group Volume and isMuted parameters in Unity
        if (muteToggle.isOn)
        {
            //-80 is the minimum volume
            masterAudio.SetFloat("isMutedVolume", -80);
        }
        else
        {
            //0 is the maximum volume
            masterAudio.SetFloat("isMutedVolume", 0);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
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

