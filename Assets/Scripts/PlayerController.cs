using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //GameDirector�X�N���v�g�̏��
    GameObject gameDirector;


    //Animator�̏��
    Animator anim;

    //Rigidbody�̏��
    Rigidbody2D rb2d;


    //�e�̏��Ǘ�
    #region
    public GameObject blastPrefab;
    Vector2 shotPoint1;
    Vector2 shotPoint2;
    Vector2 shotPoint3;
    int shotAngle;
    int KillCount;
    #endregion


    //Player�̏��
    #region

    [Header("�ړ����x�ݒ�")]
    [SerializeField]
    float moveSpeed;// �v���C���[�̈ړ����x
    float hInput;// Horizontal
    float vInput;// Vertical
    Vector2 dir;//�v���C���[�̓����̒l��ۉ�����ϐ�
    Vector2 Pos; //�v���C���[�̈ʒu���

    [SerializeField]
    float minX, maxX; //���ړ��̏��

    [SerializeField]
    float minY, maxY; //�c�ړ��̏��

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
        if (other.gameObject.tag == "Enemy") //�G�ɓ���������
        {
            Debug.Log("hit");
            gameDirector.GetComponent<GameDirector>().EnemyHit(); //EnemyHit���\�b�h���Ăяo���B���g�́u�������Ԃ̌����v
            Destroy(other.gameObject); //�ڐG�����I�u�W�F�N�g�̍폜
        }
    }



    public void PlayerMover() //�v���C���[�̐���
    {

        dir = Vector2.zero;
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        dir = new Vector2(hInput, vInput).normalized;//�΂߈ړ��ő����Ȃ�Ȃ��悤�ɂ���i�x�N�g���̐��K���j
        rb2d.velocity =(dir * moveSpeed);



        Pos = transform.position;
        Pos.x = Mathf.Clamp(Pos.x, minX, maxX);
        Pos.y = Mathf.Clamp(Pos.y, minY, maxY);
        transform.position = Pos;



    }



    private void Shot() //�e�̐���
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



    private void PlayerAnimation()�@//�v���C���[�̃A�j���[�V��������
    {

        if (hInput > 0) //���ɓ������Ƃ�
        {
            anim.SetBool("Right", true);
        }

        else if (hInput < 0) //���ɓ������Ƃ�
        {
            anim.SetBool("Left", true);
        }

        else�@//����ȊO
        {
            anim.SetBool("Left", false);
            anim.SetBool("Right", false);
        }

    }



}