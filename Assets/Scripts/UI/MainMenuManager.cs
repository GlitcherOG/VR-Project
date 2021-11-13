using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenu, optionMenu;

    // Start is called before the first frame update
    void Start()
    {
        optionMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    //Close the app
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}