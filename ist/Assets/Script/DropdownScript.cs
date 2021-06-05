using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;

public class DropdownScript : MonoBehaviour
{
    public CodeList[] quenstions;

    public Sprite[] Gameicons = new Sprite[3];

    public GameObject panel;
    // Start is called before the first frame update

    List<object> qList;
    public static List<string> answersRand;
    CodeList crntQ;
    int randQuest;

    public TextMeshProUGUI txt_result;
    public static int count_lvl;

    public static int sum = 0;
    public static int bestIS, lostIs=0;


    public TextMeshProUGUI countQuestions;
    public TextMeshProUGUI countQuest;
    public GameObject lineRes;
 

    void Start()
    {
        countQuestions.text = quenstions.Length.ToString();

        count_lvl = quenstions.Length - 1;
        if (panel.activeSelf) panel.SetActive(false);
        if (PlayerPrefs.GetInt("Level_Game2") > count_lvl)
        {
            ResultGame();
            PlayerPrefs.SetInt("Level_Game2", 0);
        }


        qList = new List<object>(quenstions);
        codeGenerate();
    }

   // Update is called once per frame
   void Update()
    {
        
    }
    void codeGenerate()
    {
        
        if (countQuestions.text != countQuest.text)

            countQuest.text = (PlayerPrefs.GetInt("Level_Game2") + 1).ToString();

        lineRes.GetComponent<Image>().fillAmount = float.Parse(countQuest.text.ToString()) / float.Parse(countQuestions.text.ToString());


        if (qList.Count > 0)
        {
          
            


            crntQ = qList[PlayerPrefs.GetInt("Level_Game2")] as CodeList;

            Image();

            List<string> answers = new List<string>(crntQ.answers);
            List<string> answersRand = new List<string>(crntQ.answers);
            for (int ii = 0; ii < crntQ.answers.Length; ii++)
            {
                int rand = Random.Range(0, answers.Count);
                answersRand[ii] = answers[rand];
                answers.RemoveAt(rand);
            }

            for (int i = 0; i < crntQ.answers.Length; i++)
            {
                if (GameObject.Find("Dropdown (" + i + ")").GetComponent<TMP_Dropdown>().options.Count == 0)
                    GameObject.Find("Dropdown (" + i + ")").GetComponent<TMP_Dropdown>().interactable = true;

                GameObject.Find("Dropdown (" + i + ")").GetComponent<TMP_Dropdown>().AddOptions(answersRand);
                GameObject.Find("Dropdown (" + i + ")").GetComponent<TMP_Dropdown>().value = Random.Range(0, answersRand.Count);
            }
            
            int a = 0;
            while (a < 10)
            {
                if (GameObject.Find("Dropdown (" + a + ")").GetComponent<TMP_Dropdown>().options.Count == 0) 
                    GameObject.Find("Dropdown (" + a + ")").GetComponent<TMP_Dropdown>().interactable = false;

                a++;
            }
        }
        
       

    }

    public void Image()
    {
        for (int i= 0; i < 25; i++ )
        {
            int a = 0;
            while (a < 25)
            {
                GameObject.Find("block (" + a + ")").GetComponent<SpriteRenderer>().sprite = Gameicons[1];
                GameObject.Find("block (" + a + ")").GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);
                a++;
            }

            GameObject.Find("block (" + crntQ.man + ")").GetComponent<SpriteRenderer>().sprite = Gameicons[0];
            GameObject.Find("block (" + crntQ.man + ")").GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);

            GameObject.Find("block (" + crntQ.prize + ")").GetComponent<SpriteRenderer>().sprite = Gameicons[3];
            GameObject.Find("block (" + crntQ.prize + ")").GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);

            a = 0;
            while (a < crntQ.block.Length)
            {
                GameObject.Find("block (" + crntQ.block[a] + ")").GetComponent<SpriteRenderer>().sprite = Gameicons[1];
                GameObject.Find("block (" + crntQ.block[a] + ")").GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                a++;
            }


        }
    }


    public void Check()
    {
        int result = 0;
        for (int i = 0; i < crntQ.answers.Length; i++)
        {
            int value = GameObject.Find("Dropdown (" + i + ")").GetComponent<TMP_Dropdown>().value;
            if (GameObject.Find("Dropdown (" + i + ")").GetComponent<TMP_Dropdown>().options[value].text == crntQ.answers[i].ToString())
            {
                GameObject.Find("Dropdown (" + i + ")").GetComponent<Image>().color = new Color32(102, 186, 92, 255);
                result += 1;                
            }
            else
            {
                GameObject.Find("Dropdown (" + i + ")").GetComponent<Image>().color = new Color32(186, 95, 95, 255);
            }
        }

        StartCoroutine(checkColor(result));

    }

    IEnumerator checkColor(int result)
    {
        if (result< crntQ.answers.Length)
        {
            txt_result.text = "Неправильно!";
            txt_result.color = new Color32(186, 95, 95, 255);            
        }
        else
        {
            txt_result.text = "Правильно!";
            txt_result.color = new Color32(102, 186, 92, 255);

            StartCoroutine(Animation());
        }
        if (!txt_result.gameObject.activeSelf) txt_result.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);
        for (int i = 0; i < crntQ.answers.Length; i++)
        {
            GameObject.Find("Dropdown (" + i + ")").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }


        GameObject.Find("txt_result").gameObject.SetActive(false);

        yield break;
    }

    IEnumerator Animation()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < crntQ.way.Length-1; i++)
        {
            GameObject.Find("block (" + crntQ.way[i] + ")").GetComponent<SpriteRenderer>().sprite = Gameicons[1];
            GameObject.Find("block (" + crntQ.way[i] + ")").GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);

            if (crntQ.way[i+1] == crntQ.way[crntQ.way.Length-1]) GameObject.Find("block (" + crntQ.way[i + 1] + ")").GetComponent<SpriteRenderer>().sprite = Gameicons[2];
            else
            GameObject.Find("block (" + crntQ.way[i+1] + ")").GetComponent<SpriteRenderer>().sprite = Gameicons[0];
            GameObject.Find("block (" + crntQ.way[i+1] + ")").GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            yield return new WaitForSeconds(0.5f);
        }


        NextLvl();
        yield return new WaitForSeconds(1);
        yield break;

    }
    public void NextLvl()
    {
        PlayerPrefs.SetInt("Level_Game2", PlayerPrefs.GetInt("Level_Game2") + 1);
        sum += 1;
        int a = 0;
        while (a < 10)
        {
            GameObject.Find("Dropdown (" + a + ")").GetComponent<TMP_Dropdown>().interactable = false;     
            
            a++;
        }
        a = 0;
        while (a < 25)
        {
            GameObject.Find("block (" + a + ")").GetComponent<SpriteRenderer>().sprite = Gameicons[1];
            GameObject.Find("block (" + a + ")").GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);
            a++;
        }

        panel.gameObject.SetActive(true);

        //SceneManager.LoadScene("Game_2");
    }

    public void Nl()
    {
        panel.gameObject.SetActive(false);

        SceneManager.LoadScene("Game_2");
        codeGenerate();
    }
    public static void ResultGame()
    {
        if (PlayerPrefs.HasKey("BestCountIS_Game2"))
            bestIS = PlayerPrefs.GetInt("BestCountIS_Game2");
        else bestIS = 0;

        if (bestIS < sum || bestIS == 0)
        {
            lostIs = bestIS;
            bestIS = sum;
            // ProfileScript.countII += bestII;
            PlayerPrefs.SetInt("countIS", PlayerPrefs.GetInt("countIS") + bestIS * 2 - lostIs * 2);
            PlayerPrefs.SetInt("BestCountIS_Game2", bestIS);
        }

        if (bestIS > PlayerPrefs.GetInt("BestCountIS_Game2")) PlayerPrefs.SetInt("BestCountIS_Game2", bestIS);

        ResultScript.result = sum;
        ResultScript.num_Game = 2;
        sum = 0;
        ProfileScript.DataProfile();
        SceneManager.LoadScene("Result");
    }

    [System.Serializable]
    public class CodeList
    {
        public string[] answers;
        public int[] way;
        public int man;
        public int prize;
        public int[] block;
    }

}
