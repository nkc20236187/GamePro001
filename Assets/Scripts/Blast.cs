using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Blast : MonoBehaviour
{
    //弾速
    [SerializeField]
    float blastSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
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
        if( collision.gameObject.tag == "Enemy")//敵に当たった時
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }


}
