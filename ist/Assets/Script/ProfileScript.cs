using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ProfileScript : MonoBehaviour
{

    //public static int countII, countIS, countIM;
    public TextMeshProUGUI resultProfile;
    public TextMeshProUGUI IS, II, IM;
    public GameObject[] lineRes = new GameObject[3];

    public static int countII, countIS, countIM, sum;

    public static void DataProfile()
    {
        countII = PlayerPrefs.GetInt("countII");
        countIS = PlayerPrefs.GetInt("countIS");
        countIM = PlayerPrefs.GetInt("countIM");
        sum = countII + countIS + countIM;
    }

    void Start()
    {
        DataProfile();
        if (countII == countIM && countIM == countIS) resultProfile.text = " ";
        else
           if (countII >= countIS && countII >= countIM) // ИИ
            resultProfile.text = "ИИ";
        else if (countIS >= countII && countIS >= countIM) // ИС
            resultProfile.text = "ИС";
        else if (countIM >= countII && countIM >= countIS) // ИМ
            resultProfile.text = "ИМ";

        lineRes[0].GetComponent<Image>().fillAmount = float.Parse(countIS.ToString()) / float.Parse(sum.ToString());
        lineRes[1].GetComponent<Image>().fillAmount = float.Parse(countII.ToString()) / float.Parse(sum.ToString());
        lineRes[2].GetComponent<Image>().fillAmount = float.Parse(countIM.ToString()) / float.Parse(sum.ToString());

        IS.text = countIS + "/" + sum;
        II.text = countII + "/" + sum;
        IM.text = countIM + "/" + sum;
    }

    public void backGame_1()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Btn_Reset()
    {
        PlayerPrefs.DeleteAll();
        Start();
    }
}
