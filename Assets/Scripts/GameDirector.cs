using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{

    //ゲームのオンオフの管理
    bool gameSwitch = true;

    //Enemyの管理
    #region

    public GameObject EnemyPrefab;//敵オブジェクトの情報
    GameObject go;//敵オブジェクトの生成用変数

    [Header("敵出現の頻度の設定")]
    [SerializeField]
    float span;//生成の間隔
    float delta = 0f;//生成時間管理

    int rndPosX;//敵の出現位置(X)の情報
    #endregion


    //スコア記録用
    #region
    GameObject timeGauge;//時間経過を表すゲージの情報
    float Score;//スコア（距離）の記録
    GameObject ScoreText;//スコアの表示用Textの情報
    #endregion


    //時間管理
    #region

    float endTime = 1;//制限時間（いじる必要は今のところ必要なし）

    [Header("制限時間の設定")]
    [SerializeField]
    float countTime;//時間調整用　ここの値で制限時間がかわる。100なら100秒。30なら30秒。
    #endregion


    
    float resultCount;


    // Start is called before the first frame update
    void Start()
    {
        timeGauge = GameObject.Find("TimeGauge");
        ScoreText = GameObject.Find("Score");
    }

    // Update is called once per frame
    void Update()
    {

  
        if (gameSwitch) //ゲーム起動
        {

            ScoreRecord();
            EyeEnemy();
            DecreaseTime();

        }
        else
        {

            resultCount = Time.deltaTime;

            if(resultCount >= 3)
            {
                resultCount = 0;
                SceneManager.LoadScene("Result");
            }



        }

        

    }


    public void ScoreRecord()　//スコア記録用メソッド
    {
        Score += Time.deltaTime;
        ScoreText.GetComponent<Text>().text = Score.ToString("F2") + " Km";
    }



    public void DecreaseTime()　//ゲーム時間管理のメソッド
    {
        timeGauge.GetComponent<Image>().fillAmount -= 1.0f / countTime * Time.deltaTime;　//ゲージUIの時間管理

        endTime -= 1.0f / countTime * Time.deltaTime;　//内部の時間管理

        if (endTime < 0)　//制限時間がなくなったら
        {

            gameSwitch = false;　//ゲームを止める

        }

        Debug.Log(endTime);　//endTimeの確認
    }



    public void EnemyHit()　//敵被弾時のメソッド
    {
        endTime -= 1.0f / countTime;
        timeGauge.GetComponent<Image>().fillAmount -= 1f / countTime;
    }



    private void EyeEnemy()  //EyeEnemyの生成管理
    {

        delta += Time.deltaTime;

        if (delta > span)
        {
            delta = 0f;
            rndPosX = Random.Range(-9, 10);
            go = Instantiate(EnemyPrefab);
            go.transform.position = new Vector2(rndPosX, 6);
        }
    }
}
