using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ResultScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI txt_result;
    public static int result;
    public static int num_Game;

   
    void Start()
    {
        txt_result.text = result.ToString();
    }

    public void startGame()
    {
        if (num_Game == 1)
            SceneManager.LoadScene("Game_1");
        if (num_Game == 2)
            SceneManager.LoadScene("Game_2");
        if (num_Game == 3)
            SceneManager.LoadScene("Game_3");
        if (num_Game == 4)
            SceneManager.LoadScene("Game_4");

    }

    public void backGame_1()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void profileGame()
    {
        SceneManager.LoadScene("Profile");
    }
}
