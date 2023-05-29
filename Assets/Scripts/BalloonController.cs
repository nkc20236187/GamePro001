using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    [SerializeField]
    float balloonSpeed;

    GameObject gameDirector;

    // Start is called before the first frame update
    void Start()
    {
        gameDirector = GameObject.Find("GameDirector");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -balloonSpeed * Time.deltaTime, 0);

        if (transform.position.y < -6)
        {
            Destroy(gameObject);
        }
    }
}
