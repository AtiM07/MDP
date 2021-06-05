using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject SelectedPiece;
    public Sprite[] PuzzlePhoto;
    public GameObject panel;


    int OIL = 1;
    public int PlacedPieces = 0;
    public static int sum = 0;
    public static int bestIM, lostIm=0;
    public static int count_puz;

    public TextMeshProUGUI countQuestions;
    public TextMeshProUGUI countQuest;
    public GameObject lineRes;

    void Start()
    {
        countQuestions.text = PuzzlePhoto.Length.ToString();
        countQuest.text = (PlayerPrefs.GetInt("Level") +1).ToString();
        lineRes.GetComponent<Image>().fillAmount = float.Parse(countQuest.text.ToString()) / float.Parse(countQuestions.text.ToString());

        count_puz = PuzzlePhoto.Length - 1;
        if (panel.activeSelf) panel.SetActive(false);
        if (PlayerPrefs.GetInt("Level") > PuzzlePhoto.Length-1)
        {
            ResultGame();
            PlayerPrefs.SetInt("Level", 0);
        }
            for (int i = 0; i < 36; i++)
            {
                GameObject.Find("Pieces_puzzle (" + i + ")").transform.Find("puzzle1").GetComponent<SpriteRenderer>().sprite = PuzzlePhoto[PlayerPrefs.GetInt("Level")];
            }

        
    }
        // Update is called once per frame
        void Update()
        {
           if (!panel.activeSelf)
                 { 
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.transform.CompareTag("Puzzle"))
                {
                    if (!hit.transform.GetComponent<piceseScript>().InRightPosition)
                    {
                        SelectedPiece = hit.transform.gameObject;
                        SelectedPiece.GetComponent<piceseScript>().Selected = true;
                        SelectedPiece.GetComponent<SortingGroup>().sortingOrder = OIL;
                        OIL++;
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (SelectedPiece != null)
                {
                    SelectedPiece.GetComponent<piceseScript>().Selected = false;
                    SelectedPiece = null;
                }
            }

            if (SelectedPiece != null)
            {
                Vector3 MousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                SelectedPiece.transform.position = new Vector3(MousePoint.x, MousePoint.y, 0);
            }

            if (PlacedPieces == 36)
            {

                sum += 1;
                GameOver();      

            }
                         }
    }
        public void GameOver()
        {            
            if (!panel.activeSelf) panel.SetActive(true);
        }
        public void NextLevel()
        {

        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);

        
        SceneManager.LoadScene("Game_3");
        }


    public static void ResultGame()
        {
            if (PlayerPrefs.HasKey("BestCountIM_Game3"))
                bestIM = PlayerPrefs.GetInt("BestCountIM_Game3");
            else bestIM = 0;

            if (bestIM < sum || bestIM == 0)
            {
                lostIm = bestIM;
            bestIM = sum;
                // ProfileScript.countII += bestII;
                PlayerPrefs.SetInt("countIM", PlayerPrefs.GetInt("countIM") + bestIM*2 - lostIm*2);
                PlayerPrefs.SetInt("BestCountIM_Game3", bestIM);
            }

            if (bestIM > PlayerPrefs.GetInt("BestCountIM_Game3")) PlayerPrefs.SetInt("BestCountIM_Game3", bestIM);

            ResultScript.result = sum;
            ResultScript.num_Game = 3;
            sum = 0;
            ProfileScript.DataProfile();
            SceneManager.LoadScene("Result");
    }

    
}
