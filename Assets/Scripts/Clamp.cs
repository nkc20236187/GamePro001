using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clamp : MonoBehaviour
{
    Vector2 Pos; //プレイヤーの位置情報

    [SerializeField]
    float minX, maxX; //横移動の上限

    [SerializeField]
    float minY, maxY; //縦移動の上限

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
