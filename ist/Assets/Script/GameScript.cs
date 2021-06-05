using System.Collections;
using System.Collections.Generic;

using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameScript : MonoBehaviour
{
    
    public categoryList[] category = new categoryList[3];
    public QuestionList[] quenstions;
    public TextMeshProUGUI quest;
    public TextMeshProUGUI[] answersText;
    public Button[] answBtn = new Button[4];
    //public GameObject resPanel;
    public TextMeshProUGUI countQuestions;
    public TextMeshProUGUI countQuest;
    public GameObject lineRes;
    public GameObject card_quest;


    int count = 0;
    public int IM = 0, II = 0, IS = 0;
    public static int bestII=0, bestIS=0, bestIM=0;

    List<object> qList;
    QuestionList crntQ;
    int randQuest;

    private void Start()
    {

        qList = new List<object>(quenstions);
        countQuestions.text = qList.Count.ToString();

        questGenerate();

    }

    void Update()
    {
        //cardChange();
        //new WaitForSeconds(1);
        
    }

    void questGenerate()
    {
        
        count += 1;
        if (countQuestions.text != countQuest.text)
        countQuest.text = count.ToString();

        lineRes.GetComponent<Image>().fillAmount = float.Parse(countQuest.text.ToString()) / float.Parse(countQuestions.text.ToString());

        randQuest = Random.Range(0, qList.Count);

        if (qList.Count >0)
        {
            
            crntQ = qList[randQuest] as QuestionList;

            quest.text = crntQ.question;

            List<string> answers = new List<string>(crntQ.answers);

            for (int i = 0; i < crntQ.answers.Length; i++)
            {
                int rand = Random.Range(0, answers.Count);
                answersText[i].text = answers[rand];
                answers.RemoveAt(rand);
            }

            StartCoroutine(animBtns());
        }
        else
        {
            ////roolPanel.gameObject.SetActive(true);
            //if (!resPanel.gameObject.activeSelf) resPanel.gameObject.SetActive(true);
            //resPanel.gameObject.GetComponentInChildren<Text>().text = (II+IS+IM).ToString();
            GameOver();

        }

        
    }
     IEnumerator animBtns()
    {
        int a = 0;
        while (a < crntQ.answers.Length)
        {
            answBtn[a].gameObject.GetComponent<Image>().color = Color.white;
            a++;
        }

        if (!quest.gameObject.activeSelf) quest.gameObject.SetActive(true);

        a = 0;
        while (a < crntQ.answers.Length)
        {
            if (!answBtn[a].gameObject.activeSelf) answBtn[a].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);            
            a++;
        }
        
            
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < crntQ.answers.Length; i++) answBtn[i].interactable = true;
        yield break;

    }


    IEnumerator trueFalse(bool check)
    {
        //yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < answBtn.Length; i++) answBtn[i].interactable = false;

        yield return new WaitForSeconds(0.8f);
        
        int a = 0;

        quest.gameObject.SetActive(false);
        while (a < crntQ.answers.Length)
        {
            
            answBtn[a].gameObject.GetComponent<Animator>().SetTrigger("Out");
            yield return new WaitForSeconds(0.1f);
            a++;
        }
        yield return new WaitForSeconds(0.8f);
        a = 0;
        while (a < crntQ.answers.Length)
        {
            if (answBtn[a].gameObject.activeSelf) answBtn[a].gameObject.SetActive(false);
            a++;
        }

        if (check)
        {

            if (crntQ.nameofCategory == category[0].nameofCategory) //ИИ
                II += 1;
            if (crntQ.nameofCategory == category[1].nameofCategory) //ИС
                IS += 1;
            if (crntQ.nameofCategory == category[2].nameofCategory) //ИМ
                IM += 1;                   

           
            yield return new WaitForSeconds(0.2f);
            qList.RemoveAt(randQuest);
            questGenerate();
        }
        else
        {
                       
            yield return new WaitForSeconds(0.2f);
            qList.RemoveAt(randQuest);
            questGenerate();
        }              


    }

    //void cardChange()
    //{
    //    float a = 0;
    //    float speed = 1.2f;
    //var c = card_quest.GetComponent<Rigidbody2D>();
    //    while (a !=0.50f)
    //    {
            
    //        c.GetComponent<Rigidbody2D>().MovePosition(c.position + Vector2.down * a * speed);
    //        a+=0.1f;
    //    }
        
      
    public void answersBtns(int indexBtn)
    {
        
        if (answersText[indexBtn].text.ToString() == crntQ.answers[0])
        {
            answBtn[indexBtn].gameObject.GetComponent<Image>().color = new Color32(102, 186, 92, 255); 
            StartCoroutine(trueFalse(true));
        }
        else 
        {
            answBtn[indexBtn].gameObject.GetComponent<Image>().color = new Color32(186, 95, 95, 255);
            StartCoroutine(trueFalse(false));
        }

        /*qList.RemoveAt(randQuest);
        questGenerate();*/
    }

    public void backGame_1()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void GameOver()
    {
        if (PlayerPrefs.HasKey("BestCountII_Game1"))
            bestII = PlayerPrefs.GetInt("BestCountII_Game1");
        else bestII = 0;
        if (PlayerPrefs.HasKey("BestCountIS_Game1"))
            bestIS = PlayerPrefs.GetInt("BestCountIS_Game1");
        else bestIS = 0;
        if (PlayerPrefs.HasKey("BestCountIM_Game1"))
            bestIM = PlayerPrefs.GetInt("BestCountIM_Game1");
        else bestIM = 0;


        if (bestII < II || bestII == 0)
        {            
            bestII = II;
            //ProfileScript.countII += bestII;
            PlayerPrefs.SetInt("countII", PlayerPrefs.GetInt("countII") + bestII);
            PlayerPrefs.SetInt("BestCountII_Game1", bestII);
        }
        if (bestIS < IS || bestIS == 0) 
        { 
            bestIS = IS;
            //ProfileScript.countIS += bestIS;
            PlayerPrefs.SetInt("countIS", PlayerPrefs.GetInt("countIS") + bestIS);
            PlayerPrefs.SetInt("BestCountIS_Game1", bestIS);
        }
        if (bestIM < IM || bestIM == 0)
        {
            bestIM = IM;
            //ProfileScript.countIM += bestIM;
            PlayerPrefs.SetInt("countIM", PlayerPrefs.GetInt("countIM") + bestIM);
            PlayerPrefs.SetInt("BestCountIM_Game1", bestIM);
        }
        if (bestII > PlayerPrefs.GetInt("BestCountII_Game1")) PlayerPrefs.SetInt("BestCountII_Game1", bestII);
        if (bestIS > PlayerPrefs.GetInt("BestCountIS_Game1")) PlayerPrefs.SetInt("BestCountIS_Game1", bestIS);
        if (bestIM > PlayerPrefs.GetInt("BestCountIM_Game1")) PlayerPrefs.SetInt("BestCountIM_Game1", bestIM);

        ResultScript.result = II + IM + IS;
        ResultScript.num_Game = 1;
        II = 0; IM = 0; IS = 0;

        ProfileScript.DataProfile();
        SceneManager.LoadScene("Result");
        
    }
}

[System.Serializable]
public class QuestionList
{
    public string nameofCategory;
    public string question;
    public string[] answers = new string[4];

}

[System.Serializable]
public class categoryList
{
    public string nameofCategory;
}
