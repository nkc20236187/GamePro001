using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float EnemySpeed;
    GameObject gameDirector;

    // Start is called before the first frame update
    void Start()
    {
        gameDirector = GameObject.Find("GameDirector");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -EnemySpeed * Time.deltaTime, 0);

        if (transform.position.y < -6)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Blast")
        {
            Destroy(gameObject);
        }

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }



}
