using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData_SO", menuName = "Enemy/EnemyDetails")]
public class EnemyData_SO : ScriptableObject
{
    [Header("EnemyData")]
    [Header("ID与アイコン")]
    public string enemyName;//名前
    public Sprite enemyIcon;//アイコン
    public Sprite enemyOnWorldSprite;//Game内の画像
    [Multiline] public string enemyDescription;//プレイヤーキャラクターの紹介
    [Space(10)]
    [Header("基本データ")]
    public int enemyMaxHP;
    public int enemyHP;
    public float enemySpeed;
}
