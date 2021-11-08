using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraInteraction : MonoBehaviour
{
    public static CameraInteraction instance;
    public GameObject canvas;
    public Slider slider;
    public InteractionManager currentInteraction;
    public InteractionManager newInteraction;
    public RaycastHit hit;
    Camera cam;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100.0f))
        {
            canvas.SetActive(true);
            newInteraction = hit.transform.GetComponent<InteractionManager>();
            if (newInteraction == null)
            {
                if (currentInteraction != null)
                {
                    currentInteraction.HitDisabled();
                    currentInteraction = null;
                }
            }
            else
            {
                if (currentInteraction != newInteraction)
                {
                    if (currentInteraction != null)
                    {
                        currentInteraction.HitDisabled();
                    }
                    currentInteraction = newInteraction;
                    currentInteraction.HitActive();
                }
                else
                {
                    slider.value = currentInteraction.count / currentInteraction.Maxseconds;
                }
            }
        }
        else
        {
            canvas.SetActive(false);
            if (currentInteraction != null)
            {
                currentInteraction.HitDisabled();
                currentInteraction = null;
            }
        }
    }
}
