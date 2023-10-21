using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleManeger : MonoBehaviour
{
    // �ϐ��錾
    // �� Maneger�n��
    // �o�ߎ���
    [SerializeField] private float NowTime;
    // ��������
    [SerializeField] private float MaxTime;

    // �� Player�n��
    // �v���C���[�̗̑�
    public PlayerData_SO playerData;
    public int maxHp
    {
        //Player��MaxHP���Q�b�g����
        get { if (playerData != null) return playerData.playerMaxHP; else return 0; }

    }
    public int PlayerHP
    {
        //Player��HP���Q�b�g����
        get { if (playerData != null) return playerData.playerHP; else return 0; }
        set { playerData.playerHP = value; }
    }

    // �� Enemy�n��
    // �G��ވꗗ

    // TEST�p�ϐ�
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    public enum EnemyName
    {
        NoName,
        Golem,
        No2,
        End
    }
    // �G�̎�ޖ��̎擾�p�ϐ�
    [SerializeField]private EnemyName EnemyType;
    // �G�̗̑�
    public static int EnemyHP;
    // �s���ҋ@����
    [SerializeField] private float ActionCD;
    // �s�����e
    private int ActionType;
    // �U���������Ǘ�����ϐ�
    private bool isAction = false;
    // �X�^����Ԃ��Ǘ�����ϐ�
    private bool isStan;

    //UI�ɂ���
    //HPBar�n��
    public Image playerHPBar;
    public Image enemyHPBar;

    private void Awake()
    {
        //playerHPBar = GetComponent<Image>();
        //enemyHPBar = gameObject.GetComponent<Image>();
    }

    void Start()
    {
        // �v���C���[�̗̑͏����l�ݒ�
        PlayerHP = 100;
        // �G�̗̑͏����l�ݒ�
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
        // �X�^��(�t�B�[�o�[)��Ԃł͂Ȃ��ꍇ
        if (!isStan)
        {
            TimeControl();
            EnemyControl();
        }

        PlayerControl();

        //HPUI�ω�
        playerHPBar.fillAmount = (float)PlayerHP / 100;
        enemyHPBar.fillAmount = (float)EnemyHP / 100;
    }

    void TimeControl()
    {
        NowTime += Time.deltaTime;
        // �������Ԃ��������ꍇ
        if (NowTime >= MaxTime)
        {
            // �Q�[���I�[�o�[����
        }
    }

    void PlayerControl()
    {

    }

    void EnemyControl()
    {
        // �G�̍s�����Ԃ�i�߂�
        ActionCD += Time.deltaTime;
        if (ActionCD >= 10)
        {
            // �G�s���̃����_���I�o�@�\
            // �U�����ĂȂ��ꍇ�������s��
            // �� ���s���������Ȃ����߂̃Z�[�t�e�B�[�Ƃ��Ď���
            if (!isAction)
            {
                // �G�̎�ނ��m�F����
                switch (EnemyType)
                {
                    // �S�[�����̏ꍇ
                    case EnemyName.Golem:
                        // 1�`3�܂ł̃����_���Ȑ��l��1�o��
                        ActionType = Random.Range(1, 4);
                        // �o�����l�����ƂɍU�����e�����߂�
                        switch (ActionType)
                        {
                            // �U������
                            case 1:
                                // �_���[�W����
                                PlayerHP = PlayerHP - 10;
                                // ���o�֌W�̏���������

                                break;
                            // ���U������
                            case 2:
                                // �_���[�W����
                                PlayerHP = PlayerHP - 25;
                                // ���o�֌W�̏���������

                                break;
                            case 3:
                                // �_���[�W����
                                PlayerHP = PlayerHP - 5;
                                // ���o�֌W�̏���������

                                break;
                        }
                        Debug.Log("EnemyAttack!! No." + ActionType);
                        Debug.Log("PlayerHp =>" + PlayerHP);
                        textMeshProUGUI.text = ("EnemyAttack!! PlayerHp => " + PlayerHP);
                        break;
                }
                // �Z�[�t�e�B���I���ɂ���
                isAction = true;

            }
            else
            { 
                // �s�����Ԃ̃��Z�b�g
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
