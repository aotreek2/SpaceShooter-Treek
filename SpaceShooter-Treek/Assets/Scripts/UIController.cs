//////////////////////////////////////////////
//Assignment/Lab/Project: SpaceShooter_Treek
//Name: Ahmed Treek
//Section: SGD.213.0021
//Instructor: Aurore Locklear
//Date: 3/31/2024
/////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel, instructionsPanel;

    void Start()
    {
        if(instructionsPanel != null)
        {
            instructionsPanel.SetActive(false); //if the instruction panel is found, hide it
        }

    }

  

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("MainGame"); //load game
    }
    public void OnHelpButtonClicked()
    {
        instructionsPanel.SetActive(true); //hides menu,  shows instructions
        mainMenuPanel.SetActive(false);
    }

    public void OnBackButtonClicked()
    {
        instructionsPanel.SetActive(false); //goes back to menu
        mainMenuPanel.SetActive(true);
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit(); //quits
    }

    public void OnMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
