using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultDirector : MonoBehaviour
{
    GameDirector gameDirector;
    GameObject resultScoreText;
    int  resultScore;


    void Start()
    {
        gameDirector = GetComponent<GameDirector>();
        resultScore = PlayerPrefs.GetInt("SCORE");
        resultScoreText = GameObject.Find("ScoreMessage");
        resultScoreText.GetComponent<Text>().text = resultScore.ToString() +  " Km";
    }


    void Update()
    {

        if (Input.GetButtonDown("reset"))
        {
            SceneManager.LoadScene("Title");
        }
    }
}
