using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{

    //�Q�[���̃I���I�t�̊Ǘ�
    bool gameSwitch = true;

    //Enemy�̊Ǘ�
    #region

    public GameObject EnemyPrefab;//�G�I�u�W�F�N�g�̏��
    GameObject go;//�G�I�u�W�F�N�g�̐����p�ϐ�

    [Header("�G�o���̕p�x�̐ݒ�")]
    [SerializeField]
    float span;//�����̊Ԋu
    float delta = 0f;//�������ԊǗ�

    int rndPosX;//�G�̏o���ʒu(X)�̏��
    #endregion


    //�X�R�A�L�^�p
    #region
    GameObject timeGauge;//���Ԍo�߂�\���Q�[�W�̏��
    float Score;//�X�R�A�i�����j�̋L�^
    GameObject ScoreText;//�X�R�A�̕\���pText�̏��
    #endregion


    //���ԊǗ�
    #region

    float endTime = 1;//�������ԁi������K�v�͍��̂Ƃ���K�v�Ȃ��j

    [Header("�������Ԃ̐ݒ�")]
    [SerializeField]
    float countTime;//���Ԓ����p�@�����̒l�Ő������Ԃ������B100�Ȃ�100�b�B30�Ȃ�30�b�B
    #endregion


    
    float resultCount;


    // Start is called before the first frame update
    void Start()
    {
        timeGauge = GameObject.Find("TimeGauge");
        ScoreText = GameObject.Find("Score");
    }

    // Update is called once per frame
    void Update()
    {

  
        if (gameSwitch) //�Q�[���N��
        {

            ScoreRecord();
            EyeEnemy();
            DecreaseTime();

        }
        else
        {

            resultCount = Time.deltaTime;

            if(resultCount >= 3)
            {
                resultCount = 0;
                SceneManager.LoadScene("Result");
            }



        }

        

    }


    public void ScoreRecord()�@//�X�R�A�L�^�p���\�b�h
    {
        Score += Time.deltaTime;
        ScoreText.GetComponent<Text>().text = Score.ToString("F2") + " Km";
    }



    public void DecreaseTime()�@//�Q�[�����ԊǗ��̃��\�b�h
    {
        timeGauge.GetComponent<Image>().fillAmount -= 1.0f / countTime * Time.deltaTime;�@//�Q�[�WUI�̎��ԊǗ�

        endTime -= 1.0f / countTime * Time.deltaTime;�@//�����̎��ԊǗ�

        if (endTime < 0)�@//�������Ԃ��Ȃ��Ȃ�����
        {

            gameSwitch = false;�@//�Q�[�����~�߂�

        }

        Debug.Log(endTime);�@//endTime�̊m�F
    }



    public void EnemyHit()�@//�G��e���̃��\�b�h
    {
        endTime -= 1.0f / countTime;
        timeGauge.GetComponent<Image>().fillAmount -= 1f / countTime;
    }



    private void EyeEnemy()  //EyeEnemy�̐����Ǘ�
    {

        delta += Time.deltaTime;

        if (delta > span)
        {
            delta = 0f;
            rndPosX = Random.Range(-9, 10);
            go = Instantiate(EnemyPrefab);
            go.transform.position = new Vector2(rndPosX, 6);
        }
    }
}
