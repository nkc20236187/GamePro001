using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    //�Q�[���N���Ȃǂ̃V�X�e���Ǘ�
    #region
    //�Q�[���̃I���I�t�̊Ǘ�
    bool gameSwitch = true;
    bool EndSwitch = false;
    float SwichCount = 0;//�X�^�[�g�̃J�E���g�_�E��
    GameObject MessageText;//�Q�[���I�����̃e�L�X�g
    float resultCount;//���̃V�[���Ɉڂ�܂ł̎��Ԓ���
    #endregion


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
    int ResultScore;
    GameObject ScoreText;//�X�R�A�̕\���pText�̏��

    public int killScore = 0;
    #endregion


    //���ԊǗ�
    #region

    float endTime = 1;//�������ԁi������K�v�͍��̂Ƃ���K�v�Ȃ��j

    [Header("�������Ԃ̐ݒ�")]
    [SerializeField]
    float countTime;//���Ԓ����p�@�����̒l�Ő������Ԃ������B100�Ȃ�100�b�B30�Ȃ�30�b�B

    [SerializeField]
    float HitTime;
    #endregion




    // Start is called before the first frame update
    void Start()
    {
        timeGauge = GameObject.Find("TimeGauge");
        ScoreText = GameObject.Find("Score");
        MessageText = GameObject.Find("Message");
        EndSwitch = false;
    }

    // Update is called once per frame
    void Update()
    {

        GameStart();

        if (gameSwitch && SwichCount > 5) //�Q�[���N��
        {

            ScoreRecord();
            EyeEnemy();
            DecreaseTime();

        }

        else if (!gameSwitch && EndSwitch)
        {
            GameEnd();
        }

    }



    private void GameStart()
    {
        #region
        if (!EndSwitch)
        {
            SwichCount += Time.deltaTime;

            if (SwichCount > 1 && SwichCount <=  4)
            {
                MessageText.GetComponent<Text>().text = "Ready...";
            }
            else if (SwichCount > 4 && SwichCount <= 5)
            {
                MessageText.GetComponent<Text>().text = "Start!!";
            }
            else if(SwichCount > 5)
            {
                MessageText.GetComponent<Text>().text = " ";
            }
        }
        #endregion
    }



    private void GameEnd()
    {
        #region
        MessageText.GetComponent<Text>().text = "Finish!!";
        resultCount += Time.deltaTime;

        if (resultCount >= 3)
        {
            SceneManager.LoadScene("Result");
            resultCount = 0;
        }
        #endregion
    }

    public void ScoreRecord()�@//�X�R�A�L�^�p���\�b�h
    {
        #region
        Score += Time.deltaTime;
        ScoreText.GetComponent<Text>().text = Score.ToString("F2") + " Km";

        ResultScore = (int)Score;

        PlayerPrefs.SetInt("SCORE", ResultScore);
        PlayerPrefs.Save();
        #endregion
    }

    public void BlastHit()
    {
        killScore++;
        Debug.Log(killScore);
    }



    public void DecreaseTime()�@//�Q�[�����ԊǗ��̃��\�b�h
    {
        #region
        timeGauge.GetComponent<Image>().fillAmount -= 1.0f / countTime * Time.deltaTime;�@//�Q�[�WUI�̎��ԊǗ�

        endTime -= 1.0f / countTime * Time.deltaTime;�@//�����̎��ԊǗ�

        if (endTime < 0)�@//�������Ԃ��Ȃ��Ȃ�����
        {
            gameSwitch = false;�@//�Q�[�����~�߂�
            EndSwitch = true;//GameEnd()�̋N��
        }

        Debug.Log(endTime); //endTime�̊m�F
        #endregion
    }



    public void EnemyHit()�@//�G��e���̃��\�b�h
    {
        endTime -= HitTime / countTime;
        timeGauge.GetComponent<Image>().fillAmount -= HitTime / countTime;
    }



    private void EyeEnemy()  //EyeEnemy�̐����Ǘ�
    {
        #region
        delta += Time.deltaTime;

        if (delta > span)
        {
            delta = 0f;
            rndPosX = Random.Range(-9, 10);
            go = Instantiate(EnemyPrefab);
            go.transform.position = new Vector2(rndPosX, 6);
        }
        #endregion
    }
}
