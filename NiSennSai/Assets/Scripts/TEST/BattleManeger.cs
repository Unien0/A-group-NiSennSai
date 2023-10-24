using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManeger : MonoBehaviour
{
    public CameraShake shake;

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
    // �G�I�u�W�F�N�g
    [SerializeField] private GameObject EnemyObj;

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
    private int EnemyMAXHP;
    // �s���ҋ@����
    [SerializeField] private float ActionCD;
    // �s�����e
    private int ActionType;
    // �U���������Ǘ�����ϐ�
    private bool isAction = false;
    // �X�^����Ԃ��Ǘ�����ϐ�
    private bool isStan;
    private float isStanTime;

    //UI�ɂ���
    //HPBar�n��
    public Image playerHPBar;
    public Image enemyHPBar;

    //���Ԍn��
    public Image TimeCT;

    //EnemyDie
    public GameObject EnemyExplosionEffect;
    //public GameObject diamond;

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
                EnemyMAXHP = EnemyHP;
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

            if ((EnemyMAXHP - EnemyHP) > 30)
            {
                isStan = true;
                EnemyMAXHP = EnemyHP;
                isStanTime = 0;
            }
        }
        else
        {
            if (isStanTime > 5)
            {
                isStan = false;
            }
            isStanTime += Time.deltaTime;
            FindObjectOfType<DrawManeger>().DrowCountRemaining = 15;
        }

        PlayerControl();

        //HPUI�ω�
        playerHPBar.fillAmount = (float)PlayerHP / 100;
        enemyHPBar.fillAmount = (float)EnemyHP / 100;

        //CTUI�ω�
        TimeCT.fillAmount = (float)ActionCD / 10;
    }

    void TimeControl()
    {
        NowTime += Time.deltaTime;
        // �������Ԃ��������ꍇ
        if (NowTime >= MaxTime)
        {
            // �Q�[���I�[�o�[����
        }

        if (EnemyHP <= 0)
        {
            EnemyExplosionEffect.SetActive(true);
            //SceneManager.LoadScene("Clear");
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
                                if (ActionCD >= 11)
                                {
                                    shake.Shake(0.25f, 0.1f);
                                    // �_���[�W����
                                    PlayerHP = PlayerHP - 10;
                                    // �Z�[�t�e�B���I���ɂ���
                                    isAction = true;
                                }

                                EventHandler.CallPlaySoundEvent(SoundName.EnemyAttack1);
                                // ���o�֌W�̏���������
                                EnemyObj.GetComponent<Animator>().SetBool("ACT1", true);
                                Debug.Log("ACT1");
                                break;
                            // ���U������
                            case 2:
                                if (ActionCD >= 11)
                                {
                                    shake.Shake(0.25f, 0.1f);
                                    // �_���[�W����
                                    PlayerHP = PlayerHP - 20;
                                    // �Z�[�t�e�B���I���ɂ���
                                    isAction = true;
                                }
                                EventHandler.CallPlaySoundEvent(SoundName.EnemyAttack2);
                                // ���o�֌W�̏���������
                                EnemyObj.GetComponent<Animator>().SetBool("ACT2", true);
                                Debug.Log("ACT2");
                                break;
                            case 3:
                                if (ActionCD >= 11)
                                {
                                    shake.Shake(0.25f, 0.1f);
                                    // �_���[�W����
                                    PlayerHP = PlayerHP - 5;
                                    // �Z�[�t�e�B���I���ɂ���
                                    isAction = true;
                                }
                                EventHandler.CallPlaySoundEvent(SoundName.EnemyAttack3);
                                // ���o�֌W�̏���������
                                EnemyObj.GetComponent<Animator>().SetBool("ACT1", true);
                                Debug.Log("ACT1_A");
                                break;
                        }
                        Debug.Log("EnemyAttack!! No." + ActionType);
                        Debug.Log("PlayerHp =>" + PlayerHP);
                        textMeshProUGUI.text = ("EnemyAttack!! PlayerHp => " + PlayerHP);
                        break;
                }
            }
            else
            { 
                // �s�����Ԃ̃��Z�b�g
                ActionCD = 0;
                EnemyObj.GetComponent<Animator>().SetBool("ACT1", false);
                EnemyObj.GetComponent<Animator>().SetBool("ACT2", false);
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
