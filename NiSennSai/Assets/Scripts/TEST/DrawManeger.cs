using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManeger : MonoBehaviour
{
    // =================================
    // マウスカーソルの移動に応じて、オブジェクトを移動させるスクリプト
    // =================================
    // マウスカーソルの位置座標
    private Vector3 position;
    // スクリーン座標をワールド座標に変換した位置座標
    private Vector3 screenToWorldPointPosition;
    // トレイルエフェクトを保存する変数
    [SerializeField] private GameObject TrailObj;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MouseMove();

        if (Input.GetMouseButton(0))
        {
            TrailObj.SetActive(true);
        }
        else
        {
            TrailObj.SetActive(false);
        }
    }

    void MouseMove()
    {
        // Vector3でマウス位置座標を取得する
        position = Input.mousePosition;
        // Z軸修正
        position.z = 10f;
        // マウス位置座標をスクリーン座標からワールド座標に変換する
        screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);
        // ワールド座標に変換されたマウス座標を代入
        gameObject.transform.position = screenToWorldPointPosition;
    }
}
