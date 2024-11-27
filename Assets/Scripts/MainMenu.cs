using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public void loadGame()
    {
        SceneManager.LoadScene("MainMap");
    }
    public void loadMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }   
}
