using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //GameDirectorスクリプトの情報
    GameObject gameDirector;


    //Animatorの情報
    Animator anim;

    //Rigidbodyの情報
    Rigidbody2D rb2d;


    //弾の情報管理
    #region
    public GameObject blastPrefab;
    Vector2 shotPoint1;
    Vector2 shotPoint2;
    Vector2 shotPoint3;
    int shotAngle;
    int KillCount;
    #endregion


    //Playerの情報
    #region

    [Header("移動速度設定")]
    [SerializeField]
    float moveSpeed;// プレイヤーの移動速度
    float hInput;// Horizontal
    float vInput;// Vertical
    Vector2 dir;//プレイヤーの動きの値を保温する変数
    Vector2 Pos; //プレイヤーの位置情報

    [SerializeField]
    float minX, maxX; //横移動の上限

    [SerializeField]
    float minY, maxY; //縦移動の上限

    #endregion


    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        gameDirector = GameObject.Find("GameDirector");
        shotAngle = -60;

    }



    void Update()
    {

        PlayerMover();
        Shot();
        PlayerAnimation();
        ShotController();

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy") //敵に当たった時
        {
            Debug.Log("hit");
            gameDirector.GetComponent<GameDirector>().EnemyHit(); //EnemyHitメソッドを呼び出す。中身は「制限時間の減少」
            Destroy(other.gameObject); //接触したオブジェクトの削除
        }
    }



    public void PlayerMover() //プレイヤーの制御
    {

        dir = Vector2.zero;
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        dir = new Vector2(hInput, vInput).normalized;//斜め移動で早くならないようにする（ベクトルの正規化）
        rb2d.velocity =(dir * moveSpeed);



        Pos = transform.position;
        Pos.x = Mathf.Clamp(Pos.x, minX, maxX);
        Pos.y = Mathf.Clamp(Pos.y, minY, maxY);
        transform.position = Pos;



    }



    private void Shot() //弾の制御
    {
        #region
        if (Input.GetButtonDown("Blast") || Input.GetKeyDown(KeyCode.Space))
        {
            
            GameObject Blast;
            //GameObject Blast2;
            //GameObject Blast3;

            KillCount =gameDirector.GetComponent<GameDirector>().killScore;

            if (KillCount >= 0 && KillCount < 10 )
            {
                Blast = Instantiate(blastPrefab);
                Blast.transform.position = shotPoint1;
            }

            else if(KillCount >= 10 && KillCount < 20)
            {
                Instantiate(blastPrefab, shotPoint2, Quaternion.Euler(0, 0, 0));
                Instantiate(blastPrefab, shotPoint3, Quaternion.Euler(0, 0, 0));

            }

            else if(KillCount >= 20)
            {

                for (int i = 0; i < 3; i++)
                {
                    Instantiate(blastPrefab, shotPoint1, Quaternion.Euler(0, 0, shotAngle));
                    shotAngle += 60;
                }
            }
        }

        else
        {
            shotAngle = -60;
        }
        #endregion
    }

    private void ShotController()
    {
        shotPoint1 = new Vector3(transform.position.x,transform.position.y + 1);
        shotPoint2 = new Vector3(transform.position.x + 1, transform.position.y + 1);
        shotPoint3 = new Vector3(transform.position.x - 1, transform.position.y + 1);
    }



    private void PlayerAnimation()　//プレイヤーのアニメーション制御
    {

        if (hInput > 0) //→に動いたとき
        {
            anim.SetBool("Right", true);
        }

        else if (hInput < 0) //←に動いたとき
        {
            anim.SetBool("Left", true);
        }

        else　//それ以外
        {
            anim.SetBool("Left", false);
            anim.SetBool("Right", false);
        }

    }



}