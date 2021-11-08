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
  public float count;
  private bool hit;
  public UnityEvent Events;

  bool reached;

  // Update is called once per frame
  void Update()
  {
    if (!Active) return;

    Canvas.SetActive(count > 0);

    if (count > 0 && !reached)
    {
      slider.value = count / Maxseconds;
      if (count >= Maxseconds)
      {
        Events.Invoke();
        reached = true;
      }
    }
    else if(count < Maxseconds)
    {
      reached = false;
    }

    if (hit && count <= Maxseconds)
    {
      count += Time.deltaTime;

    }
    else if (!hit && count > 0)
    {
      count -= Time.deltaTime;
    }
  }

  public void HitActive() => hit = true;
  public void HitDisabled() => hit = false;
}
