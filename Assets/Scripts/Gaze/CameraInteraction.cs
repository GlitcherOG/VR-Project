using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInteraction : MonoBehaviour
{
    public InteractionManager Current;
    public InteractionManager New;
    public RaycastHit hit;

  Camera cam;

  private void Start()
  {
    cam = Camera.main;
  }
  void Update()
    {
        
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100.0f))
        {
            New = hit.transform.GetComponent<InteractionManager>();
            if (New == null)
            {
                if (Current != null)
                {
                    Current.HitDisabled();
                    Current = null;
                }
            }
            else
            {
                if (Current != New)
                {
                    if (Current != null)
                    {
                        Current.HitDisabled();
                    }                    
                    Current = New;
                    Current.HitActive();
                }
            }
        }
        else
        {
            if (Current != null)
            {
                Current.HitDisabled();
                Current = null;
            }
        }
    }
}
