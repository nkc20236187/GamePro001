using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //GameDirectorスクリプトの情報
    GameObject gameDirector;


    //Animatorの情報
    Animator anim;


    //弾の情報管理
    #region
    public GameObject blastPrefab;
    GameObject Blast;
    #endregion


    //Playerの情報
    #region

    [Header("移動速度設定")]
    [SerializeField]
    float moveSpeed;// プレイヤーの移動速度
    float hInput;// Horizontal
    float vInput;// Vertical

    Vector2 Pos; //プレイヤーの位置情報

    [SerializeField]
    float minX, maxX; //横移動の上限

    [SerializeField]
    float minY,maxY; //縦移動の上限

    #endregion


    void Start()
    {
        anim = GetComponent<Animator>();
        gameDirector = GameObject.Find("GameDirector");
    }



    void Update()
    {

        Pos = transform.position;

        Pos.x = Mathf.Clamp(Pos.x, minX, maxX);
        Pos.y = Mathf.Clamp(Pos.y, minY, maxY);


        PlayerMover();
        Shot();
        PlayerAnimation();

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

        transform.Translate(hInput, vInput, 0);

        hInput = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        vInput = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

    }


    private void Shot() //弾の制御
    {
        if (Input.GetButtonDown("Blast"))
        {
            Blast = Instantiate(blastPrefab);
            Blast.transform.position = new Vector2(transform.position.x, transform.position.y + 1);
        }
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

