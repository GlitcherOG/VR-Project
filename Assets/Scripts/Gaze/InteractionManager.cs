using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class InteractionManager : MonoBehaviour
{
    public bool Active = true;
    public bool resetable = true;
    public float Maxseconds = 3;
    public float count;
    private bool hit;
    public UnityEvent Events;

    bool reached;
    bool cooldown;

    // Update is called once per frame
    void Update()
    {
        if (!Active) return;

        if (count > 0 && !reached)
        {
            if (count >= Maxseconds)
            {
                count = 0;
                cooldown = true;
                Events.Invoke();
                if (!resetable)
                {
                    reached = true;
                }
            }
        }
        else if (count < Maxseconds)
        {
            reached = false;
        }

        if (!cooldown)
        {
            if (hit && count <= Maxseconds)
            {
                count += Time.deltaTime;

            }
            else if (!hit && count > 0)
            {
                count -= Time.deltaTime;
            }
        }
    }

    public void HitActive() => hit = true;
    public void HitDisabled()
    {
        hit = false;
        cooldown = false;
    }
}
