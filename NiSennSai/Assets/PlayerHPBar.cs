using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
{
    public PlayerData_SO playerData;
    public int maxHp
    {
        //PlayerのMaxHPをゲットする
        get { if (playerData != null) return playerData.playerMaxHP; else return 0; }

    }
    public int hp
    {
        //PlayerのHPをゲットする
        get { if (playerData != null) return playerData.playerHP; else return 0; }
        set { playerData.playerHP = value; }
    }
    //UIについて
    //HPBar系列
    private Image playerHPBar;
    // Start is called before the first frame update
    void Start()
    {
        playerHPBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHPBar.fillAmount = (float)hp / (float)maxHp;
    }
}
