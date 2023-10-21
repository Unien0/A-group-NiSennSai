using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData_SO", menuName = "Enemy/EnemyDetails")]
public class EnemyData_SO : ScriptableObject
{
    [Header("EnemyData")]
    [Header("ID�^�A�C�R��")]
    public string enemyName;//���O
    public Sprite enemyIcon;//�A�C�R��
    public Sprite enemyOnWorldSprite;//Game���̉摜
    [Multiline] public string enemyDescription;//�v���C���[�L�����N�^�[�̏Љ�
    [Space(10)]
    [Header("��{�f�[�^")]
    public int enemyMaxHP;
    public int enemyHP;
    public float enemySpeed;
}
