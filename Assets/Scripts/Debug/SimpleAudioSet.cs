using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SimpleAudioSet : MonoBehaviour
{
  public AudioMixer mixer;

  public Slider slider;
    // Start is called before the first frame update
    void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    UpdateMixer();
  }

  public void UpdateMixer()
  {
    mixer.SetFloat("MusicVol", BT.BaneSound.LinearToLogAudio(slider.value));
  }
}
