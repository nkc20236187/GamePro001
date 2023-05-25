using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clamp : MonoBehaviour
{
    Vector2 Pos; //�v���C���[�̈ʒu���

    [SerializeField]
    float minX, maxX; //���ړ��̏��

    [SerializeField]
    float minY, maxY; //�c�ړ��̏��

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 Pos = transform.position;

        Pos.x = Mathf.Clamp(Pos.x, minX, maxX);
        Pos.y = Mathf.Clamp(Pos.y, minY, maxY);
    }
}