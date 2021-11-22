using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraDot : MonoBehaviour
{
  public float dot;
  public float downThreshold = -0.7f;

  public bool isLookingDown = false;

  public UnityEvent onLookDownEvent;

  bool isTriggered;

  void Update()
  {
    dot = Vector3.Dot(Vector3.up, transform.forward);
    isLookingDown = (dot < downThreshold);

    if (isLookingDown && !isTriggered)
    {
      isTriggered = true;
      onLookDownEvent.Invoke();
    }

    if (!isLookingDown && isTriggered)
      isTriggered = false;
  }
}
