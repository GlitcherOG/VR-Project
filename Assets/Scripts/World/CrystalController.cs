using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
  public Color dayColor, nightColor;

  Renderer rend;
  Material mat;

  bool needsUpdate = false;
  bool isDay;

  void Start()
  {
    rend = GetComponent<Renderer>();
    mat = rend.material;
  }

  void Update()
  {
    if (DayNightCycle.isDay != isDay)
    {
      isDay = DayNightCycle.isDay;

      mat.SetColor("_DiffuseTint", isDay ? dayColor : nightColor);
      mat.SetColor("_Emission", isDay ? dayColor : nightColor);
    }
  }
}
