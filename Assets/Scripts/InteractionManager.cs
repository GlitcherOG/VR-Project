using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class InteractionManager : MonoBehaviour
{
    public bool Active = true;
    public GameObject Canvas;
    public Slider slider;
    public float Maxseconds = 3;
    private float count;
    private bool hit;
    public UnityEvent Events;
    // Update is called once per frame
    void Update()
    {
        if(count>0)
        {
            Canvas.SetActive(true);
            slider.value = count / Maxseconds;
        }
        else if(count >= Maxseconds)
        {
            Active = false;
            Events.Invoke();
        }
        else
        {
            Canvas.SetActive(false);
        }


        if (hit && count <= Maxseconds)
        {
            count += Time.deltaTime;

        }
        else if(!hit && count>0)
        {
            count -= Time.deltaTime;
        }
    }

    public void HitActive()
    {
        hit = true;
    }

    public void HitDisabled()
    {
        hit = false;
    }
}
