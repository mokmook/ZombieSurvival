using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]private string moveAxisName = "Vertical";  //�յ� �������� ���� �Է��� �̸�
    [SerializeField]private string rotateAxisName = "Horizontal"; //�¿� ȸ���� ���� �Է��� �̸�
    [SerializeField]private string fireButtonName = "Fire1"; //�߻縦 ���� �Է� ��ư �̸�
    [SerializeField]private string reloadButtonName = "Reload"; //�������� ���� �Է� ��ư �̸�

    public float move { get; private set; } //������ ������ �Է°�
    public float rotate { get; private set; } //������ ȸ�� �Է°�
    public bool fire { get; private set; } //������ �߻� �Է°�
    public bool reload { get; private set; } //������ ������ �Է°�

    //�������� ����� �Է��� ����
    private void Update()
    {
        //���ӿ��� ���¿����� ����� �Է��� �������� ����
        if (GameManager.instance!=null&&GameManager.instance.isGameover)
        {
            move = 0;
            rotate = 0;
            fire = false;
            reload = false;
            return;
        }
        //move�� ���� �Է� ����
        move =Input.GetAxis(moveAxisName);
        //rotate�� ���� �Է� ����
        rotate =Input.GetAxis(rotateAxisName);
        //fire�� ���� �Է� ����
        fire =Input.GetButton(fireButtonName);
        //reload�� ���� �Է� ����
        reload = Input.GetButtonDown(reloadButtonName);
    }
}
