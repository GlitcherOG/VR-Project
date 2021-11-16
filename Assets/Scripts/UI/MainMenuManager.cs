using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenu, optionMenu;

    public static MainMenuManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        optionMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    //Send player to the starting scene
    public void StartGame()
    {
        SceneManager.LoadScene("Jordy Test Scene");
    }

    //Close the app
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }

    //Sends player back to temp main menu
    public void RestartGame()
    {
        SceneManager.LoadScene("TestLara");
    }
}