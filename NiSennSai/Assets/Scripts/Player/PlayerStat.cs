using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�v���C���[��ԂɊւ���X�N���v�g
public class PlayerStat : MonoBehaviour
{
    public PlayerData_SO playerData;
    public int hp
    {
        //Player��HP���Q�b�g����
        get { if (playerData != null) return playerData.playerHP; else return 0; }
        set { playerData.playerHP = value; }
    }
    private float speed
    {
        //Player�̃X�s�[�h�l���擾����
        get { if (playerData != null) return playerData.playerSpeed; else return 0; }

    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
