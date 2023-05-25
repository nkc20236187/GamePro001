using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{

    public GameObject EnemyPrefab;
    GameObject go;
    GameObject timeGauge;

    bool gameSwitch = true;

    [SerializeField]
    float span;//¶¬‚ÌŠÔŠu

    float delta = 0f;//¶¬ŽžŠÔ



    [SerializeField]
    float endTime;//§ŒÀŽžŠÔ


    [SerializeField]
    float countTime;//ŽžŠÔ’²®—p


    int rndPosX;

    // Start is called before the first frame update
    void Start()
    {
        timeGauge = GameObject.Find("TimeGauge");
    }

    // Update is called once per frame
    void Update()
    {

        if (gameSwitch)
        {

            delta += Time.deltaTime;

            if (delta > span)
            {
                delta = 0f;
                rndPosX = Random.Range(-9, 10);
                go = Instantiate(EnemyPrefab);
                go.transform.position = new Vector2(rndPosX, 6);
            }

            DecreaseTime();
            endTime -= 1.0f / countTime * Time.deltaTime;

            if (endTime < 0)
            {
                gameSwitch = false;
            }

            Debug.Log(endTime);
        }

        

    }

    public void DecreaseTime()
    {
        timeGauge.GetComponent<Image>().fillAmount -= 1.0f / countTime * Time.deltaTime; 
    }

    public void EnemyHit()
    {
        endTime -= 1.0f / countTime;
        timeGauge.GetComponent<Image>().fillAmount -= 1f / countTime;
    }
}
