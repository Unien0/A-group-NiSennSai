using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        Rhombus,
        Star,
        END
    }
    [SerializeField] private DrowShape SelectShape;

    // 一度だけ処理を行う為の変数
    private bool fix;

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
    // 書ける回数
    [SerializeField] private int DrowCountRemaining;
    [SerializeField] private float DrowCountFixing;

    // ◆ Shape系列
    // 四角形
    [SerializeField] private GameObject Square;
    [SerializeField] private GameObject Triangle;
    [SerializeField] private GameObject Rhombus;
    [SerializeField] private GameObject Star;

    // TEST用変数
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    void Start()
    {
        col2D = GetComponent<Collider2D>();
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
        DrowCountFixing += Time.deltaTime;
        // 残数が残っていて
        if (DrowCountRemaining > 0)
        {
            // マウス左クリックをすると、記述する
            if (Input.GetMouseButton(0))
            {
                TrailObj.SetActive(true);
            }
            else
            {
                TrailObj.SetActive(false);
            }
            if (DrowCountFixing > 2)
            {
                DrowCountFixing = 0;
                DrowCountRemaining += 1;
            }
        }
        else
        {
            TrailObj.SetActive(false);
            DrowCountRemaining = 0;
            if (DrowCountFixing > 4)
            {
                DrowCountFixing = 0;
                DrowCountRemaining += 5;
            }
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

        // 処理確認 = 1度だけ処理を行う
        if (!fix)
        {
            // 図形の表記を変更する
            // また、表示中の図形に応じてルート処理を変更する
            switch (SelectShape)
            {
                case DrowShape.None:
                    // 全図形の非表示
                    Square.SetActive(false);
                    Triangle.SetActive(false);
                    Rhombus.SetActive(false);
                    Star.SetActive(false);
                    break;
                case DrowShape.Square:
                    // 四角形の表示
                    Square.SetActive(true);
                    Triangle.SetActive(false);
                    Rhombus.SetActive(false);
                    Star.SetActive(false);

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
                case DrowShape.Triangle:
                    // 3角形の表示
                    Square.SetActive(false);
                    Triangle.SetActive(true);
                    Rhombus.SetActive(false);
                    Star.SetActive(false);
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
                                    if (DrowCount == 2)
                                    {
                                        TurnRight = true;
                                        RootCount = 2;
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
                                    if (DrowCount == 1)
                                    {
                                        TurnRight = true;
                                        RootCount = 4;
                                    }
                                    if (DrowCount == 1)
                                    {
                                        RootCount = 2;
                                        TurnRight = false;
                                    }

                                    Drowing = true;
                                }
                            }
                            break;

                            #endregion
                    }
                    break;

                case DrowShape.Rhombus:
                    // ひし形の表示
                    Square.SetActive(false);
                    Triangle.SetActive(false);
                    Rhombus.SetActive(true);
                    Star.SetActive(false);
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
                case DrowShape.Star:
                    // 星形の表示
                    Square.SetActive(false);
                    Triangle.SetActive(false);
                    Rhombus.SetActive(false);
                    Star.SetActive(true);

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
                                    if (DrowCount == 5)
                                    {
                                        // 次の書き順に進む
                                        RootCount = 5;
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
                                    if (DrowCount == 5)
                                    {
                                        RootCount = 5;
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
                                    if (DrowCount == 5)
                                    {
                                        // 次の書き順に進む
                                        RootCount = 5;
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
                                    if (DrowCount == 5)
                                    {
                                        TurnRight = true;
                                        RootCount = 5;
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
                        case 5:
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
                                    if (DrowCount == 1)
                                    {
                                        TurnRight = true;
                                        RootCount = 1;
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
                            #endregion
                    }
                    break;
            }

            fix = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        switch(col.gameObject.tag)
        {
            case "Square":
                DrowCountRemaining -= 1;
                StrokeOrder += 1;
                // 全て書き切ったら
                if (StrokeOrder > 4)
                {
                    textMeshProUGUI.text = ("10Point Damege !!");
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
            case "Triangle":
                StrokeOrder += 1;
                DrowCountRemaining -= 1;
                // 全て書き切ったら
                if (StrokeOrder > 3)
                {
                    textMeshProUGUI.text = ("8Point Damege !!");
                    // ダメージを与える
                    BattleManeger.EnemyHP -= 8;
                    Debug.Log("Player Attack!");
                    Debug.Log("8Point Damege !!");
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
                }
                // 最初に触れた位置を保存する
                if (!FirstTouch)
                {
                    FirstTouch = true;
                    RootCount = DrowCount;
                    StrokeOrder = 1;
                }
                break;
            case "Rhombus":
                StrokeOrder += 1;
                DrowCountRemaining -= 1;
                // 全て書き切ったら
                if (StrokeOrder > 4)
                {
                    textMeshProUGUI.text = ("12Point Damege !!");
                    // ダメージを与える
                    BattleManeger.EnemyHP -= 12;
                    Debug.Log("Player Attack!");
                    Debug.Log("12Point Damege !!");
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
            case "Star":
                StrokeOrder += 1;
                DrowCountRemaining -= 1;
                // 全て書き切ったら
                if (StrokeOrder > 5)
                {
                    textMeshProUGUI.text = ("25Point Damege !!");
                    // ダメージを与える
                    BattleManeger.EnemyHP -= 25;
                    Debug.Log("Player Attack!");
                    Debug.Log("25Point Damege !!");
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
                    case "5":
                        DrowCount = 5;
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 再度処理を行えるようにする
        fix = false;
        textMeshProUGUI.text = ("");
    }
}
