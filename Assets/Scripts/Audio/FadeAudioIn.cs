using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FadeAudioIn : MonoBehaviour
{
  public AudioMixer audioMixer;

  public float fadeTime = 3;
  float timer;

  public float sfxVol;
  public float audioVal;

  private void Start()
  {
    audioMixer.GetFloat("SFXVol", out sfxVol);

    sfxVol = BT.BaneSound.LogAudioToLinear(sfxVol);
  }

  private void Update()
  {
    if (timer < fadeTime)
    {
      timer += Time.deltaTime;
      audioVal = Mathf.Lerp(0, sfxVol, (timer / fadeTime));
      
      audioMixer.SetFloat("SFXVol", BT.BaneSound.LinearToLogAudio(audioVal));
    }
  }
}
