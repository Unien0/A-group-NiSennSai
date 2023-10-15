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

    private Collider2D col2D;

    // ◆ Drow系列
    // 記述記号
    public enum DrowShape
    {
        None,
        Square,
        Triangle,
        END
    }
    [SerializeField] private DrowShape SelectShape;

    // 書き順算出用変数
    private bool FirstTouch;

    // 書き順保存(true = 右回り)
    [SerializeField] private bool TurnRight;

    // ルート保存用変数
    [SerializeField] private int RootCount;
    // 触れた当たり判定の値
    [SerializeField] private int DrowCount;

    // 記述中
    [SerializeField] private bool Drowing;
    // 正しく触れた回数
    [SerializeField] private int StrokeOrder;

    // ◆ Shape系列
    // 四角形
    [SerializeField] private GameObject Square;

    void Start()
    {
        col2D = GetComponent<Collider2D>();
        col2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // マウス操作
        MouseMove();
        // 図形変更
        ShapeChange();


    }

    void MouseMove()
    {
        // マウス左クリックをすると、記述する
        if (Input.GetMouseButton(0))
        {
            TrailObj.SetActive(true);
            col2D.enabled = true;
        }
        else
        {
            TrailObj.SetActive(false);
            col2D.enabled = false;   
        }

        // Vector3でマウス位置座標を取得する
        position = Input.mousePosition;
        // Z軸修正
        position.z = 10f;
        // マウス位置座標をスクリーン座標からワールド座標に変換する
        screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);
        // ワールド座標に変換されたマウス座標を代入
        gameObject.transform.position = screenToWorldPointPosition;
    }

    void ShapeChange()
    {
        // 図形の表記を変更する
        // また、表示中の図形に応じてルート処理を変更する
        switch (SelectShape)
        {
            case DrowShape.None:
                // 全図形の非表示
                Square.SetActive(false);
                break;
            case DrowShape.Square:
                // 四角形の表示
                Square.SetActive(true);
                // ルート処理
                switch (RootCount)
                {
                    #region Root
                    case 1:
                        // 3回目以降の判定を取る場合
                        if (Drowing)
                        {
                            // 右回りの場合
                            if (TurnRight)
                            {
                                if (DrowCount == 2)
                                {
                                    // 次の書き順に進む
                                    RootCount = 2;
                                }
                                else
                                {
                                    // 初期化
                                    FirstTouch = false;
                                    RootCount = 0;
                                    Drowing = false;
                                    StrokeOrder = 0;
                                }
                            }
                            else
                            {
                                if (DrowCount == 4)
                                {
                                    // 次の書き順に進む
                                    RootCount = 4;
                                }
                                else
                                {
                                    // 初期化
                                    FirstTouch = false;
                                    RootCount = 0;
                                    Drowing = false;
                                    StrokeOrder = 0;
                                }
                            }
                        }
                        else
                        {
                            Drowing = true;
                            if (FirstTouch)
                            {
                                if (DrowCount == 2)
                                {
                                    TurnRight = true;
                                    RootCount = 2;
                                }
                                if (DrowCount == 4)
                                {
                                    RootCount = 4; 
                                    TurnRight = false;
                                }

                                Drowing = true;
                            }
                        }
                        break;
                    case 2:
                        // 3回目以降の判定を取る場合
                        if (Drowing)
                        {
                            // 右回りの場合
                            if (TurnRight)
                            {
                                if (DrowCount == 3)
                                {
                                    // 次の書き順に進む
                                    RootCount = 3;
                                }
                                else
                                {
                                    // 初期化
                                    FirstTouch = false;
                                    RootCount = 0;
                                    Drowing = false;
                                    StrokeOrder = 0;
                                }
                            }
                            else
                            {
                                if (DrowCount == 1)
                                {
                                    // 次の書き順に進む
                                    RootCount = 1;                                    
                                }
                                else
                                {
                                    // 初期化
                                    FirstTouch = false;
                                    RootCount = 0;
                                    Drowing = false;
                                    StrokeOrder = 0;
                                }
                            }
                        }
                        else
                        {
                            Drowing = true;
                            if (FirstTouch)
                            {
                                if (DrowCount == 3)
                                {
                                    TurnRight = true;
                                    RootCount = 3;
                                }
                                if (DrowCount == 1)
                                {
                                    RootCount = 1;
                                    TurnRight = false;
                                }

                                Drowing = true;
                            }
                        }
                        break;
                    case 3:
                        // 3回目以降の判定を取る場合
                        if (Drowing)
                        {
                            // 右回りの場合
                            if (TurnRight)
                            {
                                if (DrowCount == 4)
                                {
                                    // 次の書き順に進む
                                    RootCount = 4;
                                }
                                else
                                {
                                    // 初期化
                                    FirstTouch = false;
                                    RootCount = 0;
                                    Drowing = false;
                                    StrokeOrder = 0;
                                }
                            }
                            else
                            {
                                if (DrowCount == 2)
                                {
                                    // 次の書き順に進む
                                    RootCount = 2;
                                }
                                else
                                {
                                    // 初期化
                                    FirstTouch = false;
                                    RootCount = 0;
                                    Drowing = false;
                                }
                            }
                        }
                        else
                        {
                            Drowing = true;
                            if (FirstTouch)
                            {
                                if (DrowCount == 4)
                                {
                                    TurnRight = true;
                                    RootCount = 4;
                                }
                                if (DrowCount == 2)
                                {
                                    RootCount = 2;
                                    TurnRight = false;
                                }

                                Drowing = true;
                            }
                        }
                        break;
                    case 4:
                        // 3回目以降の判定を取る場合
                        if (Drowing)
                        {
                            // 右回りの場合
                            if (TurnRight)
                            {
                                if (DrowCount == 1)
                                {
                                    // 次の書き順に進む
                                    RootCount = 1;
                                }
                                else
                                {
                                    // 初期化
                                    FirstTouch = false;
                                    RootCount = 0;
                                    Drowing = false;
                                }
                            }
                            else
                            {
                                if (DrowCount == 3)
                                {
                                    // 次の書き順に進む
                                    RootCount = 3;
                                }
                                else
                                {
                                    // 初期化
                                    FirstTouch = false;
                                    RootCount = 0;
                                    Drowing = false;
                                    StrokeOrder = 0;
                                }
                            }
                        }
                        else
                        {
                            Drowing = true;
                            if (FirstTouch)
                            {
                                if (DrowCount == 1)
                                {
                                    TurnRight = true;
                                    RootCount = 1;
                                }
                                if (DrowCount == 3)
                                {
                                    RootCount = 3;
                                    TurnRight = false;
                                }

                                Drowing = true;
                            }
                        }
                        break;
                        #endregion
                }
                break;
        }



    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        switch(col.gameObject.tag)
        {
            case "Square":
                StrokeOrder += 1;
                // 全て書き切ったら
                if (StrokeOrder >= 4)
                {
                    // ダメージを与える
                    BattleManeger.EnemyHP -= 10;
                    Debug.Log("Player Attack!");
                    Debug.Log("10Point Damege !!");
                    // 初期化する
                    FirstTouch = false;
                    RootCount = 0;
                    Drowing = false;
                    StrokeOrder = 0;
                }
                switch (col.gameObject.name)
                {                   

                    case "1":
                        DrowCount = 1;
                        break;
                    case "2":
                        DrowCount = 2;
                        break;
                    case "3":
                        DrowCount = 3;
                        break;
                    case "4":
                        DrowCount = 4;
                        break;
                }
                // 最初に触れた位置を保存する
                if (!FirstTouch)
                {
                    FirstTouch = true;
                    RootCount = DrowCount;
                    StrokeOrder = 1;
                }
                break;
        }
    }
}
