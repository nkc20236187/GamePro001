using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Blast : MonoBehaviour
{
    GameObject gameDirector;

    //弾速
    [SerializeField]
    float blastSpeed;

    // Start is called before the first frame update
    void Start()
    {
        gameDirector = GameObject.Find("GameDirector");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,blastSpeed * Time.deltaTime,0 );

        if(transform.position.y > 6)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.gameObject.tag == "EyeEnemy")//目の敵に当たった時
        {
            gameDirector.GetComponent<GameDirector>().BlastHit();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if( collision.gameObject.tag == "Balloon")
        {
            gameDirector.GetComponent<GameDirector>().BalloonHit();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }


}
