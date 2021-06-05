using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScript_2 : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Level_Game2")) PlayGame(0);

        if (PlayerPrefs.GetInt("Level_Game2") > DropdownScript.count_lvl)
        {
            DropdownScript.ResultGame();
            PlayGame(0);
        }
    }

    // Update is called once per frame
    public void PlayGame(int LevelNumber)
    {
        PlayerPrefs.SetInt("Level_Game2", 0);
    }

    public void backGame_1()
    {
        PlayerPrefs.SetInt("Level_Game2", 0);
        DropdownScript.ResultGame();
        SceneManager.LoadScene("MainMenu");
    }
}
