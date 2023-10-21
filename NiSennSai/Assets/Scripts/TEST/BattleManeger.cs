using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleManeger : MonoBehaviour
{
    // 変数宣言
    // ◆ Maneger系列
    // 経過時間
    [SerializeField] private float NowTime;
    // 制限時間
    [SerializeField] private float MaxTime;

    // ◆ Player系列
    // プレイヤーの体力
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

    // ◆ Enemy系列
    // 敵種類一覧

    // TEST用変数
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    public enum EnemyName
    {
        NoName,
        Golem,
        No2,
        End
    }
    // 敵の種類名の取得用変数
    [SerializeField]private EnemyName EnemyType;
    // 敵の体力
    public static int EnemyHP;
    // 行動待機時間
    [SerializeField] private float ActionCD;
    // 行動内容
    private int ActionType;
    // 攻撃処理を管理する変数
    private bool isAction = false;
    // スタン状態を管理する変数
    private bool isStan;

    //UIについて
    //HPBar系列
    public Image playerHPBar;
    public Image enemyHPBar;

    //時間系列
    public Image TimeCT;

    private void Awake()
    {
        //playerHPBar = GetComponent<Image>();
        //enemyHPBar = gameObject.GetComponent<Image>();
    }

    void Start()
    {
        // プレイヤーの体力初期値設定
        PlayerHP = 100;
        // 敵の体力初期値設定
        switch (EnemyType)
        {
            case EnemyName.Golem:
                EnemyHP = 100;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // スタン(フィーバー)状態ではない場合
        if (!isStan)
        {
            TimeControl();
            EnemyControl();
        }

        PlayerControl();

        //HPUI変化
        playerHPBar.fillAmount = (float)PlayerHP / 100;
        enemyHPBar.fillAmount = (float)EnemyHP / 100;

        //CTUI変化
        TimeCT.fillAmount = (float)ActionCD / 10;
    }

    void TimeControl()
    {
        NowTime += Time.deltaTime;
        // 制限時間を上回った場合
        if (NowTime >= MaxTime)
        {
            // ゲームオーバー処理
        }
    }

    void PlayerControl()
    {

    }

    void EnemyControl()
    {
        // 敵の行動時間を進める
        ActionCD += Time.deltaTime;
        if (ActionCD >= 10)
        {
            // 敵行動のランダム選出機能
            // 攻撃してない場合処理を行う
            // ※ 二回行動をさせないためのセーフティーとして実装
            if (!isAction)
            {
                // 敵の種類を確認する
                switch (EnemyType)
                {
                    // ゴーレムの場合
                    case EnemyName.Golem:
                        // 1～3までのランダムな数値を1つ出す
                        ActionType = Random.Range(1, 4);
                        // 出た数値をもとに攻撃内容を決める
                        switch (ActionType)
                        {
                            // 攻撃処理
                            case 1:
                                // ダメージ処理
                                PlayerHP = PlayerHP - 10;
                                // 演出関係の処理を入れる

                                break;
                            // 強攻撃処理
                            case 2:
                                // ダメージ処理
                                PlayerHP = PlayerHP - 25;
                                // 演出関係の処理を入れる

                                break;
                            case 3:
                                // ダメージ処理
                                PlayerHP = PlayerHP - 5;
                                // 演出関係の処理を入れる

                                break;
                        }
                        Debug.Log("EnemyAttack!! No." + ActionType);
                        Debug.Log("PlayerHp =>" + PlayerHP);
                        textMeshProUGUI.text = ("EnemyAttack!! PlayerHp => " + PlayerHP);
                        break;
                }
                // セーフティをオンにする
                isAction = true;

            }
            else
            { 
                // 行動時間のリセット
                ActionCD = 0;
            }
        }
        else if (ActionCD >= 3)
        {
                textMeshProUGUI.text = ("");
        }
        else
        {
            isAction = false;
        }
    }
}
