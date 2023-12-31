using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    public EnemyData_SO enemyData;
    public int maxHp
    {
        //PlayerのMaxHPをゲットする
        get { if (enemyData != null) return enemyData.enemyMaxHP; else return 0; }

    }
    public int hp
    {
        //PlayerのHPをゲットする
        get { if (enemyData != null) return enemyData.enemyHP; else return 0; }
        set { enemyData.enemyHP = value; }
    }
    private Image enemyHPBar;

    void Start()
    {
        enemyHPBar = GetComponent<Image>();
    }

    void Update()
    {
        enemyHPBar.fillAmount = (float)hp / (float)maxHp;
    }
}
