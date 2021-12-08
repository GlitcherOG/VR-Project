using UnityEngine;

public class ToggleFuck : MonoBehaviour
{
  Camera cam;
  void Start()
  {
    cam = GetComponent<Camera>();
    cam.enabled = false;
    cam.enabled = true;
    }
}
