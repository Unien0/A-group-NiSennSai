using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤー状態に関するスクリプト
public class PlayerStat : MonoBehaviour
{
    public PlayerData_SO playerData;
    public int hp
    {
        //PlayerのHPをゲットする
        get { if (playerData != null) return playerData.playerHP; else return 0; }
        set { playerData.playerHP = value; }
    }
    private float speed
    {
        //Playerのスピード値を取得する
        get { if (playerData != null) return playerData.playerSpeed; else return 0; }

    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
