using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
 public void loadGame()
    {
        SceneManager.LoadScene("MainMap");
        // change to the final play scene later
    }
 public void loadMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }   
}
