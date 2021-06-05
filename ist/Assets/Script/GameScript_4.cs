using System.Collections;
using System.Collections.Generic;

using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameScript_4 : MonoBehaviour
{
    public AlgebraicList[] quenstions;
    public TextMeshProUGUI quest;
    public TextMeshProUGUI answer;
    public GameObject[] answBtn = new GameObject[10];
    public Sprite[] TFicons = new Sprite[2];
    public Image TFIcon;
    public TextMeshProUGUI countQuestions;
    public TextMeshProUGUI countQuest;
    public GameObject lineRes;
    public TextMeshProUGUI timerText;

    int count = 0;
    public int sum = 0;
    int time = 10, t = 10;
    bool stopTime = false;
    public static int bestII;


    List<object> qList;
    AlgebraicList crntQ;
    int randQuest;

    private void Start()
    {
        qList = new List<object>(quenstions);
        countQuestions.text = qList.Count.ToString();

        questGenerate();

    }


    public void NumOnClick(int indexBtn)
    {
        answer.text += indexBtn;
    }

    public void ResetOnClick()
    {
        if (answer.text != "")
        answer.text = answer.text.Substring(0, answer.text.Length - 1);
    }

    void questGenerate()
    {
        answer.text = "";
        timerText.text = "10";
        count += 1;
        if (countQuestions.text != countQuest.text)
            countQuest.text = count.ToString();
        lineRes.GetComponent<Image>().fillAmount = float.Parse(countQuest.text.ToString()) / float.Parse(countQuestions.text.ToString());

        if (qList.Count > 0)
        {
            randQuest = Random.Range(0, qList.Count);
            crntQ = qList[randQuest] as AlgebraicList;

            quest.text = crntQ.question;

            quest.gameObject.SetActive(true);
            StartCoroutine(timer());
        }
        else
        {
            //roolPanel.gameObject.SetActive(true);
            //if (!resPanel.gameObject.activeSelf) resPanel.gameObject.SetActive(true);
            //resPanel.gameObject.GetComponentInChildren<Text>().text = (II + IS + IM).ToString();
            GameOver();
        }


    }

    IEnumerator TrueFalse(bool check)
    {
        stopTime = true;
        GameObject.Find("btn_send").GetComponent<Button>().interactable = false;

        yield return new WaitForSeconds(1);

        TFIcon.gameObject.SetActive(true);

        if (check)
        {
            sum += 1;
            TFIcon.GetComponent<Image>().sprite = TFicons[0];
            
            yield return new WaitForSeconds(1);

            TFIcon.gameObject.SetActive(false);
            qList.RemoveAt(randQuest);
            questGenerate();
        }
        else
        {
            TFIcon.GetComponent<Image>().sprite = TFicons[1];

            yield return new WaitForSeconds(1);

            TFIcon.gameObject.SetActive(false);
            qList.RemoveAt(randQuest);
            questGenerate();
        }

        GameObject.Find("btn_send").GetComponent<Button>().interactable = true;
        yield break;
    }

    IEnumerator timer()
    {
        if (!timerText.gameObject.activeSelf) timerText.gameObject.SetActive(true);

        time = t;
        stopTime = false;
        while (time >= 0)
        {
            
            if (!stopTime)
            {
                timerText.color = Color.black;
                timerText.transform.parent.gameObject.GetComponentInParent<TextMeshProUGUI>().color = Color.black;
                if (time == t) timerText.text = time.ToString();
                else timerText.text = "0" + time.ToString();

                time--;
                yield return new WaitForSeconds(0.5f);
                if (time <= 2)
                {
                    timerText.color = Color.red;
                    timerText.transform.parent.gameObject.GetComponentInParent<TextMeshProUGUI>().color = Color.red;
                }
                yield return new WaitForSeconds(0.5f);

            }
            else yield break;
        }
        if (time == -1) GameOver();
    }

    
    
    public void answerBtns()
    {

        if (answer.text.ToString() == crntQ.answers.ToString())
        {
           
            StartCoroutine(TrueFalse(true));
        }
        else
        {
           
            StartCoroutine(TrueFalse(false));
        }
        /*qList.RemoveAt(randQuest);
        questGenerate();*/
    }

    public void backGame_1()
    {
        SceneManager.LoadScene("MainMenu");
        //GUIUtility.ExitGUI();
    }

    void GameOver()
    {
        if (PlayerPrefs.HasKey("BestCountII_Game4"))
            bestII = PlayerPrefs.GetInt("BestCountII_Game4");
        else bestII = 0;

        if (bestII < sum || bestII == 0)
        {
            bestII = sum;
            // ProfileScript.countII += bestII;
            PlayerPrefs.SetInt("countII", PlayerPrefs.GetInt("countII") + bestII);
            PlayerPrefs.SetInt("BestCountII_Game4", bestII);
        }

        if (bestII > PlayerPrefs.GetInt("BestCountII_Game4")) PlayerPrefs.SetInt("BestCountII_Game4", bestII);

        ResultScript.result = sum;
        ResultScript.num_Game = 4;
        sum = 0;
        ProfileScript.DataProfile();
        SceneManager.LoadScene("Result");
    }

}

[System.Serializable]
public class AlgebraicList
{
    public string question;
    public string answers;
}
