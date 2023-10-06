using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManeger : MonoBehaviour
{
    // =================================
    // �}�E�X�J�[�\���̈ړ��ɉ����āA�I�u�W�F�N�g���ړ�������X�N���v�g
    // =================================
    // �}�E�X�J�[�\���̈ʒu���W
    private Vector3 position;
    // �X�N���[�����W�����[���h���W�ɕϊ������ʒu���W
    private Vector3 screenToWorldPointPosition;
    // �g���C���G�t�F�N�g��ۑ�����ϐ�
    [SerializeField] private GameObject TrailObj;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MouseMove();

        if (Input.GetMouseButton(0))
        {
            TrailObj.SetActive(true);
        }
        else
        {
            TrailObj.SetActive(false);
        }
    }

    void MouseMove()
    {
        // Vector3�Ń}�E�X�ʒu���W���擾����
        position = Input.mousePosition;
        // Z���C��
        position.z = 10f;
        // �}�E�X�ʒu���W���X�N���[�����W���烏�[���h���W�ɕϊ�����
        screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);
        // ���[���h���W�ɕϊ����ꂽ�}�E�X���W����
        gameObject.transform.position = screenToWorldPointPosition;
    }
}
