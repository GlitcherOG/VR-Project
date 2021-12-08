using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CameraDot : MonoBehaviour
{
    public float dot;
    public float downThreshold = -0.7f;
    public float timer;
    public float triggerTime = 3;
    public Image slider;

    public bool isLookingDown = false;

    public UnityEvent onLookDownEvent;

    bool isTriggered;
    bool triggered;

    void Update()
    {
        dot = Vector3.Dot(Vector3.up, transform.forward);
        isLookingDown = (dot < downThreshold);

        if (isLookingDown && !isTriggered)
        {
            isTriggered = true;
        }

        if (!isLookingDown && isTriggered)
        {
            isTriggered = false;
        }

        if (isTriggered && !triggered && timer <= 3)
        {
            slider.enabled = true;
            timer += Time.deltaTime;
            slider.fillAmount = (timer / triggerTime);
        }
        else if(isTriggered && !triggered)
        {
            triggered = false;
            onLookDownEvent.Invoke();
        }
        else
        {
            slider.enabled = false;
            timer = 0;
        }
    }

    public void ResetVarables()
    {
        triggered = false;
        isTriggered = false;
    }
}
