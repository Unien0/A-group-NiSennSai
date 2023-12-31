using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DrawManeger : MonoBehaviour
{
    public CameraShake shake;

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
    [SerializeField] private bool TurnCheck;

    // ルート保存用変数
    [SerializeField] private int RootCount;
    // 触れた当たり判定の値
    [SerializeField] private int DrowCount;

    // 記述中
    [SerializeField] private bool Drowing;
    // 正しく触れた回数
    [SerializeField] private int StrokeOrder;
    // 書ける回数
    public int DrowCountRemaining;
    // 回復までの時間
    [SerializeField] private float DrowCountFixing;

    // ◆ Shape系列
    // 四角形
    [SerializeField] private GameObject Square;
    [SerializeField] private GameObject Square_Efect;
    // 三角形
    [SerializeField] private GameObject Triangle;
    [SerializeField] private GameObject Triangle_Efect;
    // ひし形
    [SerializeField] private GameObject Rhombus;
    [SerializeField] private GameObject Rhombus_Efect;
    // 星型
    [SerializeField] private GameObject Star;
    [SerializeField] private GameObject Star_Efect;

    //UIヒント
    [SerializeField] private GameObject SquareHint;
    [SerializeField] private GameObject TriangleHint;
    [SerializeField] private GameObject RhombusHint;
    [SerializeField] private GameObject StarHint;

    public PlayerData_SO playerData;
    public int maxHp
    {
        //PlayerのMaxHPをゲットする
        get { if (playerData != null) return playerData.playerMaxHP; else return 0; }

    }
    public int PlayerHP
    {
        //PlayerのHPをゲットする
        get { if (playerData != null) return playerData.playerHP; else return 0; }
        set { playerData.playerHP = value; }
    }

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
        // 例外処理
        FunctionAction();

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
                // 初期化する
                TurnCheck = false;
                FirstTouch = false;
                RootCount = 0;
                Drowing = false;
                StrokeOrder = 0;

                TrailObj.SetActive(false);
            }
            if (DrowCountFixing > 2)
            {
                if (!(DrowCountRemaining >= 8))
                {
                    DrowCountRemaining += 1;
                }
                else
                {
                    DrowCountFixing = 15;
                }
                DrowCountFixing = 0;
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

    public void SelectSquare()
    {
        SelectShape = DrowShape.Square;
    }

    public void SelectTriangle()
    {
        SelectShape = DrowShape.Triangle;
    }

    public void SelectRhombus()
    {
        SelectShape = DrowShape.Rhombus;
    }

    public void SelectStar()
    {
        SelectShape = DrowShape.Star;
    }

    public void FixChange()
    {
        fix = false;
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

                    SquareHint.SetActive(false);
                    TriangleHint.SetActive(false);
                    RhombusHint.SetActive(false);
                    StarHint.SetActive(false);
                    break;
                case DrowShape.Square:
                    // 四角形の表示
                    Square.SetActive(true);
                    Triangle.SetActive(false);
                    Rhombus.SetActive(false);
                    Star.SetActive(false);

                    SquareHint.SetActive(true);
                    TriangleHint.SetActive(false);
                    RhombusHint.SetActive(false);
                    StarHint.SetActive(false);

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
                                if (FirstTouch)
                                {
                                    if (DrowCount == 2)
                                    {
                                        TurnRight = true;
                                        RootCount = 2;
                                        TurnCheck = true;
                                    }
                                    if (DrowCount == 4)
                                    {
                                        RootCount = 4;
                                        TurnRight = false;
                                        TurnCheck = true;
                                    }
                                    if (TurnCheck)
                                    {
                                        Drowing = true;
                                    }

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
                                if (FirstTouch)
                                {
                                    if (DrowCount == 3)
                                    {
                                        TurnCheck = true;
                                        TurnRight = true;
                                        RootCount = 3;
                                    }
                                    if (DrowCount == 1)
                                    {
                                        RootCount = 1;
                                        TurnRight = false;
                                        TurnCheck = true;
                                    }
                                    if (TurnCheck)
                                    {
                                        Drowing = true;
                                    }
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
                                if (FirstTouch)
                                {
                                    if (DrowCount == 4)
                                    {
                                        TurnRight = true;
                                        RootCount = 4;
                                        TurnCheck = true;
                                    }
                                    if (DrowCount == 2)
                                    {
                                        RootCount = 2;
                                        TurnRight = false;
                                        TurnCheck = true;
                                    }

                                    if (TurnCheck)
                                    {
                                        Drowing = true;
                                    }
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
                                if (FirstTouch)
                                {
                                    if (DrowCount == 1)
                                    {
                                        TurnCheck = true;
                                        TurnRight = true;
                                        RootCount = 1;
                                    }
                                    if (DrowCount == 3)
                                    {
                                        RootCount = 3;
                                        TurnRight = false;
                                        TurnCheck = true;
                                    }

                                    if (TurnCheck)
                                    {
                                        Drowing = true;
                                    }
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

                    SquareHint.SetActive(false);
                    TriangleHint.SetActive(true);
                    RhombusHint.SetActive(false);
                    StarHint.SetActive(false);
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
                                if (FirstTouch)
                                {
                                    if (DrowCount == 2)
                                    {
                                        TurnCheck = true;
                                        TurnRight = true;
                                        RootCount = 2;
                                    }
                                    if (DrowCount == 3)
                                    {
                                        TurnCheck = true;
                                        RootCount = 3;
                                        TurnRight = false;
                                    }

                                    if (TurnCheck)
                                    {
                                        Drowing = true;
                                    }
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
                                if (FirstTouch)
                                {
                                    if (DrowCount == 3)
                                    {
                                        TurnCheck = true;
                                        TurnRight = true;
                                        RootCount = 3;
                                    }
                                    if (DrowCount == 1)
                                    {
                                        TurnCheck = true;
                                        RootCount = 1;
                                        TurnRight = false;
                                    }
                                    if (TurnCheck)
                                    {
                                        Drowing = true;
                                    }
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
                                if (FirstTouch)
                                {
                                    if (DrowCount == 1)
                                    {
                                        TurnCheck = true;
                                        TurnRight = true;
                                        RootCount = 4;
                                    }
                                    if (DrowCount == 1)
                                    {
                                        TurnCheck = true;
                                        RootCount = 2;
                                        TurnRight = false;
                                    }
                                    if (TurnCheck)
                                    {
                                        Drowing = true;
                                    }
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

                    SquareHint.SetActive(false);
                    TriangleHint.SetActive(false);
                    RhombusHint.SetActive(true);
                    StarHint.SetActive(false);
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
                                if (FirstTouch)
                                {
                                    if (DrowCount == 2)
                                    {
                                        TurnCheck = true;
                                        TurnRight = true;
                                        RootCount = 2;
                                    }
                                    if (DrowCount == 4)
                                    {
                                        TurnCheck = true;
                                        RootCount = 4;
                                        TurnRight = false;
                                    }

                                    if (TurnCheck)
                                    {
                                        Drowing = true;
                                    }
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
                                if (FirstTouch)
                                {
                                    if (DrowCount == 3)
                                    {
                                        TurnCheck = true;
                                        TurnRight = true;
                                        RootCount = 3;
                                    }
                                    if (DrowCount == 1)
                                    {
                                        TurnCheck = true;
                                        RootCount = 1;
                                        TurnRight = false;
                                    }
                                    if (TurnCheck)
                                    {
                                        Drowing = true;
                                    }
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
                                if (FirstTouch)
                                {
                                    if (DrowCount == 4)
                                    {
                                        TurnCheck = true;
                                        TurnRight = true;
                                        RootCount = 4;
                                    }
                                    if (DrowCount == 2)
                                    {
                                        TurnCheck = true;
                                        RootCount = 2;
                                        TurnRight = false;
                                    }
                                    if (TurnCheck)
                                    {
                                        Drowing = true;
                                    }
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
                                if (FirstTouch)
                                {
                                    if (DrowCount == 1)
                                    {
                                        TurnCheck = true;
                                        TurnRight = true;
                                        RootCount = 1;
                                    }
                                    if (DrowCount == 3)
                                    {
                                        TurnCheck = true;
                                        RootCount = 3;
                                        TurnRight = false;
                                    }

                                    if (TurnCheck)
                                    {
                                        Drowing = true;
                                    }
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

                    SquareHint.SetActive(false);
                    TriangleHint.SetActive(false);
                    RhombusHint.SetActive(false);
                    StarHint.SetActive(true);

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
                                if (FirstTouch)
                                {
                                    if (DrowCount == 2)
                                    {
                                        TurnCheck = true;
                                        TurnRight = true;
                                        RootCount = 2;
                                    }
                                    if (DrowCount == 5)
                                    {
                                        TurnCheck = true;
                                        RootCount = 5;
                                        TurnRight = false;
                                    }

                                    if (TurnCheck)
                                    {
                                        Drowing = true;
                                    }
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
                                if (FirstTouch)
                                {
                                    if (DrowCount == 3)
                                    {
                                        TurnCheck = true;
                                        TurnRight = true;
                                        RootCount = 3;
                                    }
                                    if (DrowCount == 1)
                                    {
                                        TurnCheck = true;
                                        RootCount = 1;
                                        TurnRight = false;
                                    }

                                    if (TurnCheck)
                                    {
                                        Drowing = true;
                                    }
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
                                if (FirstTouch)
                                {
                                    if (DrowCount == 4)
                                    {
                                        TurnCheck = true;
                                        TurnRight = true;
                                        RootCount = 4;
                                    }
                                    if (DrowCount == 2)
                                    {
                                        TurnCheck = true;
                                        RootCount = 2;
                                        TurnRight = false;
                                    }

                                    if (TurnCheck)
                                    {
                                        Drowing = true;
                                    }
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
                                if (FirstTouch)
                                {
                                    if (DrowCount == 5)
                                    {
                                        TurnCheck = true;
                                        TurnRight = true;
                                        RootCount = 5;
                                    }
                                    if (DrowCount == 3)
                                    {
                                        TurnCheck = true;
                                        RootCount = 3;
                                        TurnRight = false;
                                    }

                                    if (TurnCheck)
                                    {
                                        Drowing = true;
                                    }
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
                                if (FirstTouch)
                                {
                                    if (DrowCount == 1)
                                    {
                                        TurnCheck = true;
                                        TurnRight = true;
                                        RootCount = 1;
                                    }
                                    if (DrowCount == 4)
                                    {
                                        TurnCheck = true;
                                        RootCount = 4;
                                        TurnRight = false;
                                    }
                                    if (TurnCheck)
                                    {
                                        Drowing = true;
                                    }
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
                    Instantiate(Square_Efect);
                    shake.Shake(0.25f, 0.1f);
                    // ダメージを与える
                    BattleManeger.EnemyHP -= 10;
                    EventHandler.CallPlaySoundEvent(SoundName.Square);
                    Debug.Log("Player Attack!");
                    Debug.Log("10Point Damege !!");
                    // 初期化する
                    TurnCheck = false;
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
                    Instantiate(Triangle_Efect);
                    shake.Shake(0.25f, 0.1f);
                    // ダメージを与える
                    BattleManeger.EnemyHP -= 17;
                    EventHandler.CallPlaySoundEvent(SoundName.Triangle);
                    Debug.Log("Player Attack!");
                    Debug.Log("8Point Damege !!");
                    // 初期化する
                    TurnCheck = false;
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
                    Instantiate(Rhombus_Efect);
                    shake.Shake(0.25f, 0.1f);
                    // ダメージを与える
                    BattleManeger.EnemyHP -= 8;
                    PlayerHP += 10;
                    EventHandler.CallPlaySoundEvent(SoundName.Rhombus);
                    Debug.Log("Player Attack!");
                    Debug.Log("12Point Damege !!");
                    // 初期化する
                    TurnCheck = false;
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
                    Instantiate(Star_Efect);
                    shake.Shake(0.25f, 0.1f);
                    // ダメージを与える
                    BattleManeger.EnemyHP -= 30;
                    EventHandler.CallPlaySoundEvent(SoundName.Star);
                    Debug.Log("Player Attack!");
                    Debug.Log("25Point Damege !!");
                    // 初期化する
                    TurnCheck = false;
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
    }
    void FunctionAction()
    {
        // エスケープ = 強制終了
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        // F1 = リロード
        if (Input.GetKeyDown(KeyCode.F1))
        {

        }
    }
}
