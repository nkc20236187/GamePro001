using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    GameObject gameDirector;//GameDirector�̏��
    public float EnemySpeed;//�G�̃X�s�[�h


    void Start()
    {
        gameDirector = GameObject.Find("GameDirector");
    }



    void Update()
    {

        EnemyMover();

    }


    public void EnemyMover()  //�G�̓����̐���
    {
        #region
        transform.Translate(0, -EnemySpeed * Time.deltaTime, 0);�@//EnemySpeed�̐ݒ�̑����ŗ�����

        if (transform.position.y < -6) //��ʊO�ɍs������폜
        {
            Destroy(gameObject);
        }
        #endregion
    }


    private void OnBecameInvisible()//��ʊO�ɍs�����������͂��c�H
    {
        Destroy(gameObject);
    }



}
