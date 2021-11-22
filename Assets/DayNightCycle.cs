using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
  public Vector3 rotateSpeed;

  public static bool isDay;

  void Update()
  {
    transform.Rotate(rotateSpeed * Time.deltaTime, Space.World);

    isDay = Vector3.Dot(transform.forward, Vector3.up) <= 0;
  }
}

