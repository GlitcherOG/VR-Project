using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraInteraction : MonoBehaviour
{
    public static CameraInteraction instance;
    public GameObject canvas;
    public Image slider;
    public InteractionManager oldInteraction;
    public InteractionManager currentInteraction;
    private InteractionManager newInteraction;
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
        canvas.SetActive(false);
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
                    newInteraction = null;
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
                    oldInteraction = currentInteraction;
                    currentInteraction.HitActive();
                }
            }
        }
        else
        {
            if (currentInteraction != null)
            {
                currentInteraction.HitDisabled();
                currentInteraction = null;
            }
        }
        if (oldInteraction != null)
        {
            slider.fillAmount = (oldInteraction.count / oldInteraction.Maxseconds);
            if (oldInteraction.count <= 0)
            {
                canvas.SetActive(false);
            }
        }
    }
}
