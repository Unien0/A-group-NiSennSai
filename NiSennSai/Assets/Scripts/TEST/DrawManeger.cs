using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    private Collider2D col2D;

    // �� Drow�n��
    // �L�q�L��
    public enum DrowShape
    {
        None,
        Square,
        Triangle,
        Rhombus,
        Star,
        END
    }
    [SerializeField] private DrowShape SelectShape;

    // ��x�����������s���ׂ̕ϐ�
    private bool fix;

    // �������Z�o�p�ϐ�
    private bool FirstTouch;

    // �������ۑ�(true = �E���)
    [SerializeField] private bool TurnRight;

    // ���[�g�ۑ��p�ϐ�
    [SerializeField] private int RootCount;
    // �G�ꂽ�����蔻��̒l
    [SerializeField] private int DrowCount;

    // �L�q��
    [SerializeField] private bool Drowing;
    // �������G�ꂽ��
    [SerializeField] private int StrokeOrder;
    // �������
    [SerializeField] private int DrowCountRemaining;
    [SerializeField] private float DrowCountFixing;

    // �� Shape�n��
    // �l�p�`
    [SerializeField] private GameObject Square;
    [SerializeField] private GameObject Triangle;
    [SerializeField] private GameObject Rhombus;
    [SerializeField] private GameObject Star;

    //UI�q���g
    [SerializeField] private GameObject SquareHint;
    [SerializeField] private GameObject TriangleHint;
    [SerializeField] private GameObject RhombusHint;
    [SerializeField] private GameObject StarHint;

    // TEST�p�ϐ�
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    void Start()
    {
        col2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // �}�E�X����
        MouseMove();
        // �}�`�ύX
        ShapeChange();


    }

    void MouseMove()
    {
        DrowCountFixing += Time.deltaTime;
        // �c�����c���Ă���
        if (DrowCountRemaining > 0)
        {
            // �}�E�X���N���b�N������ƁA�L�q����
            if (Input.GetMouseButton(0))
            {
                TrailObj.SetActive(true);
            }
            else
            {
                TrailObj.SetActive(false);
            }
            if (DrowCountFixing > 2)
            {
                DrowCountFixing = 0;
                DrowCountRemaining += 1;
            }
        }
        else
        {
            TrailObj.SetActive(false);
            DrowCountRemaining = 0;
            if (DrowCountFixing > 4)
            {
                DrowCountFixing = 0;
                DrowCountRemaining += 5;
            }
        }


        // Vector3�Ń}�E�X�ʒu���W���擾����
        position = Input.mousePosition;
        // Z���C��
        position.z = 10f;
        // �}�E�X�ʒu���W���X�N���[�����W���烏�[���h���W�ɕϊ�����
        screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);
        // ���[���h���W�ɕϊ����ꂽ�}�E�X���W����
        gameObject.transform.position = screenToWorldPointPosition;
    }

    public void SelectSquare()
    {
        SelectShape = DrowShape.Square;
    }

    public void SelectTriangle()
    {
        SelectShape = DrowShape.Triangle;
    }

    public void SelectRhombus()
    {
        SelectShape = DrowShape.Rhombus;
    }

    public void SelectStar()
    {
        SelectShape = DrowShape.Star;
    }

    void ShapeChange()
    {

        // �����m�F = 1�x�����������s��
        if (!fix)
        {
            // �}�`�̕\�L��ύX����
            // �܂��A�\�����̐}�`�ɉ����ă��[�g������ύX����
            switch (SelectShape)
            {
                case DrowShape.None:
                    // �S�}�`�̔�\��
                    Square.SetActive(false);
                    Triangle.SetActive(false);
                    Rhombus.SetActive(false);
                    Star.SetActive(false);

                    SquareHint.SetActive(false);
                    TriangleHint.SetActive(false);
                    RhombusHint.SetActive(false);
                    StarHint.SetActive(false);
                    break;
                case DrowShape.Square:
                    // �l�p�`�̕\��
                    Square.SetActive(true);
                    Triangle.SetActive(false);
                    Rhombus.SetActive(false);
                    Star.SetActive(false);

                    SquareHint.SetActive(true);
                    TriangleHint.SetActive(false);
                    RhombusHint.SetActive(false);
                    StarHint.SetActive(false);

                    // ���[�g����
                    switch (RootCount)
                    {
                        #region Root
                        case 1:
                            // 3��ڈȍ~�̔�������ꍇ
                            if (Drowing)
                            {
                                // �E���̏ꍇ
                                if (TurnRight)
                                {
                                    if (DrowCount == 2)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 2;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                        StrokeOrder = 0;
                                    }
                                }
                                else
                                {
                                    if (DrowCount == 4)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 4;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                        StrokeOrder = 0;
                                    }
                                }
                            }
                            else
                            {
                                Drowing = true;
                                if (FirstTouch)
                                {
                                    if (DrowCount == 2)
                                    {
                                        TurnRight = true;
                                        RootCount = 2;
                                    }
                                    if (DrowCount == 4)
                                    {
                                        RootCount = 4;
                                        TurnRight = false;
                                    }

                                    Drowing = true;
                                }
                            }
                            break;
                        case 2:
                            // 3��ڈȍ~�̔�������ꍇ
                            if (Drowing)
                            {
                                // �E���̏ꍇ
                                if (TurnRight)
                                {
                                    if (DrowCount == 3)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 3;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                        StrokeOrder = 0;
                                    }
                                }
                                else
                                {
                                    if (DrowCount == 1)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 1;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                        StrokeOrder = 0;
                                    }
                                }
                            }
                            else
                            {
                                Drowing = true;
                                if (FirstTouch)
                                {
                                    if (DrowCount == 3)
                                    {
                                        TurnRight = true;
                                        RootCount = 3;
                                    }
                                    if (DrowCount == 1)
                                    {
                                        RootCount = 1;
                                        TurnRight = false;
                                    }

                                    Drowing = true;
                                }
                            }
                            break;
                        case 3:
                            // 3��ڈȍ~�̔�������ꍇ
                            if (Drowing)
                            {
                                // �E���̏ꍇ
                                if (TurnRight)
                                {
                                    if (DrowCount == 4)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 4;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                        StrokeOrder = 0;
                                    }
                                }
                                else
                                {
                                    if (DrowCount == 2)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 2;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                    }
                                }
                            }
                            else
                            {
                                Drowing = true;
                                if (FirstTouch)
                                {
                                    if (DrowCount == 4)
                                    {
                                        TurnRight = true;
                                        RootCount = 4;
                                    }
                                    if (DrowCount == 2)
                                    {
                                        RootCount = 2;
                                        TurnRight = false;
                                    }

                                    Drowing = true;
                                }
                            }
                            break;
                        case 4:
                            // 3��ڈȍ~�̔�������ꍇ
                            if (Drowing)
                            {
                                // �E���̏ꍇ
                                if (TurnRight)
                                {
                                    if (DrowCount == 1)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 1;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                    }
                                }
                                else
                                {
                                    if (DrowCount == 3)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 3;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                        StrokeOrder = 0;
                                    }
                                }
                            }
                            else
                            {
                                Drowing = true;
                                if (FirstTouch)
                                {
                                    if (DrowCount == 1)
                                    {
                                        TurnRight = true;
                                        RootCount = 1;
                                    }
                                    if (DrowCount == 3)
                                    {
                                        RootCount = 3;
                                        TurnRight = false;
                                    }

                                    Drowing = true;
                                }
                            }
                            break;
                            #endregion
                    }
                    break;
                case DrowShape.Triangle:
                    // 3�p�`�̕\��
                    Square.SetActive(false);
                    Triangle.SetActive(true);
                    Rhombus.SetActive(false);
                    Star.SetActive(false);

                    SquareHint.SetActive(false);
                    TriangleHint.SetActive(true);
                    RhombusHint.SetActive(false);
                    StarHint.SetActive(false);
                    // ���[�g����
                    switch (RootCount)
                    {
                        #region Root
                        case 1:
                            // 3��ڈȍ~�̔�������ꍇ
                            if (Drowing)
                            {
                                // �E���̏ꍇ
                                if (TurnRight)
                                {
                                    if (DrowCount == 2)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 2;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                        StrokeOrder = 0;
                                    }
                                }
                                else
                                {
                                    if (DrowCount == 3)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 3;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                        StrokeOrder = 0;
                                    }
                                }
                            }
                            else
                            {
                                Drowing = true;
                                if (FirstTouch)
                                {
                                    if (DrowCount == 2)
                                    {
                                        TurnRight = true;
                                        RootCount = 2;
                                    }
                                    if (DrowCount == 3)
                                    {
                                        RootCount = 3;
                                        TurnRight = false;
                                    }

                                    Drowing = true;
                                }
                            }
                            break;
                        case 2:
                            // 3��ڈȍ~�̔�������ꍇ
                            if (Drowing)
                            {
                                // �E���̏ꍇ
                                if (TurnRight)
                                {
                                    if (DrowCount == 3)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 3;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                        StrokeOrder = 0;
                                    }
                                }
                                else
                                {
                                    if (DrowCount == 1)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 1;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                        StrokeOrder = 0;
                                    }
                                }
                            }
                            else
                            {
                                Drowing = true;
                                if (FirstTouch)
                                {
                                    if (DrowCount == 3)
                                    {
                                        TurnRight = true;
                                        RootCount = 3;
                                    }
                                    if (DrowCount == 1)
                                    {
                                        RootCount = 1;
                                        TurnRight = false;
                                    }

                                    Drowing = true;
                                }
                            }
                            break;
                        case 3:
                            // 3��ڈȍ~�̔�������ꍇ
                            if (Drowing)
                            {
                                // �E���̏ꍇ
                                if (TurnRight)
                                {
                                    if (DrowCount == 1)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 1;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                        StrokeOrder = 0;
                                    }
                                }
                                else
                                {
                                    if (DrowCount == 2)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 2;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                    }
                                }
                            }
                            else
                            {
                                Drowing = true;
                                if (FirstTouch)
                                {
                                    if (DrowCount == 1)
                                    {
                                        TurnRight = true;
                                        RootCount = 4;
                                    }
                                    if (DrowCount == 1)
                                    {
                                        RootCount = 2;
                                        TurnRight = false;
                                    }

                                    Drowing = true;
                                }
                            }
                            break;

                            #endregion
                    }
                    break;

                case DrowShape.Rhombus:
                    // �Ђ��`�̕\��
                    Square.SetActive(false);
                    Triangle.SetActive(false);
                    Rhombus.SetActive(true);
                    Star.SetActive(false);

                    SquareHint.SetActive(false);
                    TriangleHint.SetActive(false);
                    RhombusHint.SetActive(true);
                    StarHint.SetActive(false);
                    // ���[�g����
                    switch (RootCount)
                    {
                        #region Root
                        case 1:
                            // 3��ڈȍ~�̔�������ꍇ
                            if (Drowing)
                            {
                                // �E���̏ꍇ
                                if (TurnRight)
                                {
                                    if (DrowCount == 2)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 2;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                        StrokeOrder = 0;
                                    }
                                }
                                else
                                {
                                    if (DrowCount == 4)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 4;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                        StrokeOrder = 0;
                                    }
                                }
                            }
                            else
                            {
                                Drowing = true;
                                if (FirstTouch)
                                {
                                    if (DrowCount == 2)
                                    {
                                        TurnRight = true;
                                        RootCount = 2;
                                    }
                                    if (DrowCount == 4)
                                    {
                                        RootCount = 4;
                                        TurnRight = false;
                                    }

                                    Drowing = true;
                                }
                            }
                            break;
                        case 2:
                            // 3��ڈȍ~�̔�������ꍇ
                            if (Drowing)
                            {
                                // �E���̏ꍇ
                                if (TurnRight)
                                {
                                    if (DrowCount == 3)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 3;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                        StrokeOrder = 0;
                                    }
                                }
                                else
                                {
                                    if (DrowCount == 1)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 1;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                        StrokeOrder = 0;
                                    }
                                }
                            }
                            else
                            {
                                Drowing = true;
                                if (FirstTouch)
                                {
                                    if (DrowCount == 3)
                                    {
                                        TurnRight = true;
                                        RootCount = 3;
                                    }
                                    if (DrowCount == 1)
                                    {
                                        RootCount = 1;
                                        TurnRight = false;
                                    }

                                    Drowing = true;
                                }
                            }
                            break;
                        case 3:
                            // 3��ڈȍ~�̔�������ꍇ
                            if (Drowing)
                            {
                                // �E���̏ꍇ
                                if (TurnRight)
                                {
                                    if (DrowCount == 4)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 4;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                        StrokeOrder = 0;
                                    }
                                }
                                else
                                {
                                    if (DrowCount == 2)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 2;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                    }
                                }
                            }
                            else
                            {
                                Drowing = true;
                                if (FirstTouch)
                                {
                                    if (DrowCount == 4)
                                    {
                                        TurnRight = true;
                                        RootCount = 4;
                                    }
                                    if (DrowCount == 2)
                                    {
                                        RootCount = 2;
                                        TurnRight = false;
                                    }

                                    Drowing = true;
                                }
                            }
                            break;
                        case 4:
                            // 3��ڈȍ~�̔�������ꍇ
                            if (Drowing)
                            {
                                // �E���̏ꍇ
                                if (TurnRight)
                                {
                                    if (DrowCount == 1)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 1;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                    }
                                }
                                else
                                {
                                    if (DrowCount == 3)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 3;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                        StrokeOrder = 0;
                                    }
                                }
                            }
                            else
                            {
                                Drowing = true;
                                if (FirstTouch)
                                {
                                    if (DrowCount == 1)
                                    {
                                        TurnRight = true;
                                        RootCount = 1;
                                    }
                                    if (DrowCount == 3)
                                    {
                                        RootCount = 3;
                                        TurnRight = false;
                                    }

                                    Drowing = true;
                                }
                            }
                            break;
                            #endregion
                    }
                    break;
                case DrowShape.Star:
                    // ���`�̕\��
                    Square.SetActive(false);
                    Triangle.SetActive(false);
                    Rhombus.SetActive(false);
                    Star.SetActive(true);

                    SquareHint.SetActive(false);
                    TriangleHint.SetActive(false);
                    RhombusHint.SetActive(false);
                    StarHint.SetActive(true);

                    // ���[�g����
                    switch (RootCount)
                    {
                        #region Root
                        case 1:
                            // 3��ڈȍ~�̔�������ꍇ
                            if (Drowing)
                            {
                                // �E���̏ꍇ
                                if (TurnRight)
                                {
                                    if (DrowCount == 2)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 2;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                        StrokeOrder = 0;
                                    }
                                }
                                else
                                {
                                    if (DrowCount == 5)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 5;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                        StrokeOrder = 0;
                                    }
                                }
                            }
                            else
                            {
                                Drowing = true;
                                if (FirstTouch)
                                {
                                    if (DrowCount == 2)
                                    {
                                        TurnRight = true;
                                        RootCount = 2;
                                    }
                                    if (DrowCount == 5)
                                    {
                                        RootCount = 5;
                                        TurnRight = false;
                                    }

                                    Drowing = true;
                                }
                            }
                            break;
                        case 2:
                            // 3��ڈȍ~�̔�������ꍇ
                            if (Drowing)
                            {
                                // �E���̏ꍇ
                                if (TurnRight)
                                {
                                    if (DrowCount == 3)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 3;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                        StrokeOrder = 0;
                                    }
                                }
                                else
                                {
                                    if (DrowCount == 1)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 1;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                        StrokeOrder = 0;
                                    }
                                }
                            }
                            else
                            {
                                Drowing = true;
                                if (FirstTouch)
                                {
                                    if (DrowCount == 3)
                                    {
                                        TurnRight = true;
                                        RootCount = 3;
                                    }
                                    if (DrowCount == 1)
                                    {
                                        RootCount = 1;
                                        TurnRight = false;
                                    }

                                    Drowing = true;
                                }
                            }
                            break;
                        case 3:
                            // 3��ڈȍ~�̔�������ꍇ
                            if (Drowing)
                            {
                                // �E���̏ꍇ
                                if (TurnRight)
                                {
                                    if (DrowCount == 4)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 4;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                        StrokeOrder = 0;
                                    }
                                }
                                else
                                {
                                    if (DrowCount == 2)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 2;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                    }
                                }
                            }
                            else
                            {
                                Drowing = true;
                                if (FirstTouch)
                                {
                                    if (DrowCount == 4)
                                    {
                                        TurnRight = true;
                                        RootCount = 4;
                                    }
                                    if (DrowCount == 2)
                                    {
                                        RootCount = 2;
                                        TurnRight = false;
                                    }

                                    Drowing = true;
                                }
                            }
                            break;
                        case 4:
                            // 3��ڈȍ~�̔�������ꍇ
                            if (Drowing)
                            {
                                // �E���̏ꍇ
                                if (TurnRight)
                                {
                                    if (DrowCount == 5)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 5;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                    }
                                }
                                else
                                {
                                    if (DrowCount == 3)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 3;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                        StrokeOrder = 0;
                                    }
                                }
                            }
                            else
                            {
                                Drowing = true;
                                if (FirstTouch)
                                {
                                    if (DrowCount == 5)
                                    {
                                        TurnRight = true;
                                        RootCount = 5;
                                    }
                                    if (DrowCount == 3)
                                    {
                                        RootCount = 3;
                                        TurnRight = false;
                                    }

                                    Drowing = true;
                                }
                            }
                            break;
                        case 5:
                            // 3��ڈȍ~�̔�������ꍇ
                            if (Drowing)
                            {
                                // �E���̏ꍇ
                                if (TurnRight)
                                {
                                    if (DrowCount == 1)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 1;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                    }
                                }
                                else
                                {
                                    if (DrowCount == 4)
                                    {
                                        // ���̏������ɐi��
                                        RootCount = 4;
                                    }
                                    else
                                    {
                                        // ������
                                        FirstTouch = false;
                                        RootCount = 0;
                                        Drowing = false;
                                        StrokeOrder = 0;
                                    }
                                }
                            }
                            else
                            {
                                Drowing = true;
                                if (FirstTouch)
                                {
                                    if (DrowCount == 1)
                                    {
                                        TurnRight = true;
                                        RootCount = 1;
                                    }
                                    if (DrowCount == 4)
                                    {
                                        RootCount = 4;
                                        TurnRight = false;
                                    }

                                    Drowing = true;
                                }
                            }
                            break;
                            #endregion
                    }
                    break;
            }

            fix = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        switch(col.gameObject.tag)
        {
            case "Square":
                DrowCountRemaining -= 1;
                StrokeOrder += 1;
                // �S�ď����؂�����
                if (StrokeOrder > 4)
                {
                    textMeshProUGUI.text = ("10Point Damege !!");
                    // �_���[�W��^����
                    BattleManeger.EnemyHP -= 10;
                    Debug.Log("Player Attack!");
                    Debug.Log("10Point Damege !!");
                    // ����������
                    FirstTouch = false;
                    RootCount = 0;
                    Drowing = false;
                    StrokeOrder = 0;
                }
                switch (col.gameObject.name)
                {                   

                    case "1":
                        DrowCount = 1;
                        break;
                    case "2":
                        DrowCount = 2;
                        break;
                    case "3":
                        DrowCount = 3;
                        break;
                    case "4":
                        DrowCount = 4;
                        break;
                }
                // �ŏ��ɐG�ꂽ�ʒu��ۑ�����
                if (!FirstTouch)
                {
                    FirstTouch = true;
                    RootCount = DrowCount;
                    StrokeOrder = 1;
                }
                break;
            case "Triangle":
                StrokeOrder += 1;
                DrowCountRemaining -= 1;
                // �S�ď����؂�����
                if (StrokeOrder > 3)
                {
                    textMeshProUGUI.text = ("8Point Damege !!");
                    // �_���[�W��^����
                    BattleManeger.EnemyHP -= 8;
                    Debug.Log("Player Attack!");
                    Debug.Log("8Point Damege !!");
                    // ����������
                    FirstTouch = false;
                    RootCount = 0;
                    Drowing = false;
                    StrokeOrder = 0;
                }
                switch (col.gameObject.name)
                {
                    case "1":
                        DrowCount = 1;
                        break;
                    case "2":
                        DrowCount = 2;
                        break;
                    case "3":
                        DrowCount = 3;
                        break;
                }
                // �ŏ��ɐG�ꂽ�ʒu��ۑ�����
                if (!FirstTouch)
                {
                    FirstTouch = true;
                    RootCount = DrowCount;
                    StrokeOrder = 1;
                }
                break;
            case "Rhombus":
                StrokeOrder += 1;
                DrowCountRemaining -= 1;
                // �S�ď����؂�����
                if (StrokeOrder > 4)
                {
                    textMeshProUGUI.text = ("12Point Damege !!");
                    // �_���[�W��^����
                    BattleManeger.EnemyHP -= 12;
                    Debug.Log("Player Attack!");
                    Debug.Log("12Point Damege !!");
                    // ����������
                    FirstTouch = false;
                    RootCount = 0;
                    Drowing = false;
                    StrokeOrder = 0;
                }
                switch (col.gameObject.name)
                {

                    case "1":
                        DrowCount = 1;
                        break;
                    case "2":
                        DrowCount = 2;
                        break;
                    case "3":
                        DrowCount = 3;
                        break;
                    case "4":
                        DrowCount = 4;
                        break;
                }
                // �ŏ��ɐG�ꂽ�ʒu��ۑ�����
                if (!FirstTouch)
                {
                    FirstTouch = true;
                    RootCount = DrowCount;
                    StrokeOrder = 1;
                }
                break;
            case "Star":
                StrokeOrder += 1;
                DrowCountRemaining -= 1;
                // �S�ď����؂�����
                if (StrokeOrder > 5)
                {
                    textMeshProUGUI.text = ("25Point Damege !!");
                    // �_���[�W��^����
                    BattleManeger.EnemyHP -= 25;
                    Debug.Log("Player Attack!");
                    Debug.Log("25Point Damege !!");
                    // ����������
                    FirstTouch = false;
                    RootCount = 0;
                    Drowing = false;
                    StrokeOrder = 0;
                }
                switch (col.gameObject.name)
                {

                    case "1":
                        DrowCount = 1;
                        break;
                    case "2":
                        DrowCount = 2;
                        break;
                    case "3":
                        DrowCount = 3;
                        break;
                    case "4":
                        DrowCount = 4;
                        break;
                    case "5":
                        DrowCount = 5;
                        break;
                }
                // �ŏ��ɐG�ꂽ�ʒu��ۑ�����
                if (!FirstTouch)
                {
                    FirstTouch = true;
                    RootCount = DrowCount;
                    StrokeOrder = 1;
                }
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // �ēx�������s����悤�ɂ���
        fix = false;
        textMeshProUGUI.text = ("");
    }
}
