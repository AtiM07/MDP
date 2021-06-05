using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    private void Start()
    {
        //PlayerPrefs.DeleteAll();
    }
    public void startGame_1()
    {
        SceneManager.LoadScene("Game_1");
    }

    public void startGame_2()
    {
        SceneManager.LoadScene("Game_2");
        PlayerPrefs.DeleteKey("Level");
    }

    public void startGame_3()
    {
        SceneManager.LoadScene("Game_3");
        PlayerPrefs.DeleteKey("Level");
    }
    public void startGame_4()
    {
        SceneManager.LoadScene("Game_4");
    }

    public void profileGame()
    {
        SceneManager.LoadScene("Profile");
    }
    public void backGame_1()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //PlayerPrefs[] Data = new PlayerPrefs.SetInt("Level", 0);
}
