using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //GameDirector�X�N���v�g�̏��
    GameObject gameDirector;


    //Animator�̏��
    Animator anim;


    //�e�̏��Ǘ�
    #region
    public GameObject blastPrefab;
    GameObject Blast;
    #endregion


    //Player�̏��
    #region

    [Header("�ړ����x�ݒ�")]
    [SerializeField]
    float moveSpeed;// �v���C���[�̈ړ����x
    float hInput;// Horizontal
    float vInput;// Vertical

    Vector2 Pos; //�v���C���[�̈ʒu���

    [SerializeField]
    float minX, maxX; //���ړ��̏��

    [SerializeField]
    float minY,maxY; //�c�ړ��̏��

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
        if (other.gameObject.tag == "Enemy") //�G�ɓ���������
        {
            Debug.Log("hit");
            gameDirector.GetComponent<GameDirector>().EnemyHit(); //EnemyHit���\�b�h���Ăяo���B���g�́u�������Ԃ̌����v
            Destroy(other.gameObject); //�ڐG�����I�u�W�F�N�g�̍폜
        }
    }


    public void PlayerMover() //�v���C���[�̐���
    {

        transform.Translate(hInput, vInput, 0);

        hInput = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        vInput = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

    }


    private void Shot() //�e�̐���
    {
        if (Input.GetButtonDown("Blast"))
        {
            Blast = Instantiate(blastPrefab);
            Blast.transform.position = new Vector2(transform.position.x, transform.position.y + 1);
        }
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

