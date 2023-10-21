using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData_SO", menuName = "Player/PlayerDetails")]
public class PlayerData_SO : ScriptableObject
{
    [Header("PlayerData")]
    [Header("ID�^�A�C�R��")]
    public string playerName;//���O
    public Sprite playerIcon;//�A�C�R��
    public Sprite playerOnWorldSprite;//Game���̉摜
    [Multiline] public string playerDescription;//�v���C���[�L�����N�^�[�̏Љ�
    [Space(10)]
    [Header("��{�f�[�^")]
    public int playerMaxHP;
    public int playerHP;
    public float playerSpeed;

}
