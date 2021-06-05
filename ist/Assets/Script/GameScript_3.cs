using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript_3 : MonoBehaviour
{
    //см. DragAndDrop + piecesScript
    private void Start()
    {
        if (!PlayerPrefs.HasKey("Level")) PlayGame(0);

        if (PlayerPrefs.GetInt("Level") > DragAndDrop.count_puz)
        {
            DragAndDrop.ResultGame();
            PlayGame(0);
        }
    }
    public void PlayGame(int LevelNumber)
    {
        PlayerPrefs.SetInt("Level", 0);
    }

    public void backGame_1()
    {
        PlayerPrefs.SetInt("Level", 0);
        DragAndDrop.ResultGame();
        SceneManager.LoadScene("MainMenu");
        //GUIUtility.ExitGUI();
    }
}
