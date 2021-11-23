using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
  public bool isGamePaused;
  public GameObject pauseMenu;

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
    pauseMenu.SetActive(true);
    //Time.timeScale = 0f;
    isGamePaused = true;
  }
}
