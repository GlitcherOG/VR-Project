using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandom : MonoBehaviour
{
    AudioSource source;
    // Update is called once per frame
    void Start()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine("PlayAudio");
    }

    IEnumerator PlayAudio()
    {
        float temp = Random.Range(5,30);
        yield return new WaitForSeconds(temp);
        source.Play();
        StartCoroutine("PlayAudio");
    }
}
