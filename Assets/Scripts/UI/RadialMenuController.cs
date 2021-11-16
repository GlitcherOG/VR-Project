using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class RadialMenuController : MonoBehaviour
{
    private List<Button> childButtons = new List<Button>();
    private bool isOpen; //Tracks if radial menu is open or not
    [SerializeField] private float buttonDistance; //Distance between parent radial button and children buttons

    // Start is called before the first frame update
    void Start()
    {
        //Get all the child button components
        childButtons = GetComponentsInChildren<Button>(true)
            .Where(x => x.gameObject.transform.parent != transform.parent).ToList();

        GetComponent<Button>().onClick.AddListener( () => {OpenMenu();} );

        //Loop through buttons and set to inactive on start
        foreach (Button btn in childButtons)
        {
            btn.gameObject.SetActive(false);
        }
    }

    public void OpenMenu()
    {
        isOpen = !isOpen;
        //Calculate gaps between the child buttons in the radial menu
        float angle = (90 / childButtons.Count - 1) * Mathf.Deg2Rad;
        
        //Calculate the position of each child button based on the angle around the main parent button
        for (int i = 0; i < childButtons.Count; i++)
        {
            if (isOpen)
            {
                float xPosition = Mathf.Cos(angle * i) * buttonDistance;
                float yPosition = Mathf.Sin(angle * i) * buttonDistance;

                childButtons[i].transform.position = new Vector3(transform.position.x + xPosition,
                    transform.position.y + yPosition, transform.position.z);
            }
            else
            {
                childButtons[i].transform.position = transform.position;
            }
        }
        
        foreach (Button btn in childButtons)
        {
            btn.gameObject.SetActive(true);
        }

        if (!isOpen)
        {
            foreach (Button btn in childButtons)
            {
                btn.gameObject.SetActive(false);
            }
        }
    }

    public void OpenMainMenu()
    {
        MainMenuManager.instance.optionMenu.SetActive(true);
    }

    public void QuitButton()
    {
        MainMenuManager.instance.QuitGame();
    }
}
