using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
  public bool isGamePaused;
  public GameObject pauseMenu;

  Transform parent, cam;

  private void Awake()
  {
    pauseMenu = gameObject;
    gameObject.SetActive(false);
  }

  public void Resume()
  {
    pauseMenu.SetActive(false);
    //Time.timeScale = 1f;
    isGamePaused = false;
  }

  public void Pause()
  {
    // Cache camera and parent
    if (!cam) 
      cam = Camera.main.transform;
    if (!parent)
      parent = transform.parent;

    // Get camera rotation and position and apply appropriate values to UI parent
    Vector3 newRot = new Vector3(0, cam.localEulerAngles.y, 0);
    parent.localEulerAngles = newRot;
    Vector3 newPos = new Vector3(cam.localPosition.x, 0, cam.localPosition.z);
    parent.localPosition = newPos;

    // Enable the UI
    pauseMenu.SetActive(true);
    //Time.timeScale = 0f;
    isGamePaused = true;
  }
}
