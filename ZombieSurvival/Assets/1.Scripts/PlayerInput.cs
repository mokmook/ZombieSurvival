using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]private string moveAxisName = "Vertical";  //앞뒤 움직임을 위한 입력축 이름
    [SerializeField]private string rotateAxisName = "Horizontal"; //좌우 회전을 위한 입력축 이름
    [SerializeField]private string fireButtonName = "Fire1"; //발사를 위한 입력 버튼 이름
    [SerializeField]private string reloadButtonName = "Reload"; //재장전을 위한 입력 버튼 이름

    public float move { get; private set; } //감지된 움직임 입력값
    public float rotate { get; private set; } //감지된 회전 입력값
    public bool fire { get; private set; } //감지된 발사 입력값
    public bool reload { get; private set; } //감지된 재장전 입력값

    //매프레임 사용자 입력을 감지
    private void Update()
    {
        //게임오버 상태에서는 사용자 입력을 감지하지 않음
        if (GameManager.instance!=null&&GameManager.instance.isGameover)
        {
            move = 0;
            rotate = 0;
            fire = false;
            reload = false;
            return;
        }
        //move에 관한 입력 감지
        move =Input.GetAxis(moveAxisName);
        //rotate에 관한 입력 감지
        rotate =Input.GetAxis(rotateAxisName);
        //fire에 관한 입력 감지
        fire =Input.GetButton(fireButtonName);
        //reload에 관한 입력 감지
        reload = Input.GetButtonDown(reloadButtonName);
    }
}
