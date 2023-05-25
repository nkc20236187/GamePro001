using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float EnemySpeed;//敵のスピード

    GameObject gameDirector;//GameDirectorの情報



    void Start()
    {
        gameDirector = GameObject.Find("GameDirector");
    }



    void Update()
    {

        EnemyMover();

    }


    public void EnemyMover()  //敵の動きの制御
    {
        transform.Translate(0, -EnemySpeed * Time.deltaTime, 0);　//EnemySpeedの設定の速さで落ちる

        if (transform.position.y < -6) //画面外に行ったら削除
        {
            Destroy(gameObject);
        }
    }


    private void OnBecameInvisible()//画面外に行ったら消えるはず…？
    {
        Destroy(gameObject);
    }



}
