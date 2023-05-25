using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject blastPrefab;
    GameObject shot;
    GameObject gameDirector;
    public float moveSpeed;
    float hInput;
    float vInput;

    Animator anim;

    Vector2 Pos;

    [SerializeField]
    float minX, maxX;

    [SerializeField]
    float minY,maxY;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        gameDirector = GameObject.Find("GameDirector");
    }

    // Update is called once per frame
    void Update()
    {
        Pos = transform.position;

        Pos.x = Mathf.Clamp(Pos.x, minX, maxX);
        Pos.y = Mathf.Clamp(Pos.y, minY, maxY);


        if (Input.GetButtonDown("Fire2"))
        {
            shot = Instantiate(blastPrefab);
            shot.transform.position = new Vector2(transform.position.x, transform.position.y + 1);
        }

        hInput = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        vInput = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        transform.Translate(hInput, vInput, 0);


        if (hInput > 0)
        {
            anim.SetBool("Right", true);
        }
        else if (hInput < 0)
        {
            anim.SetBool("Left", true);
        }
        else
        {
            anim.SetBool("Left", false);
            anim.SetBool("Right", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("hit");
            gameDirector.GetComponent<GameDirector>().EnemyHit();
            Destroy(other.gameObject);
        }
    }
}

