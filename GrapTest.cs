using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GrapTest : MonoBehaviour   //고흐방에서 물체 잡고 게임 플레이 구현 코드
{
    public SteamVR_Input_Sources leftHand = SteamVR_Input_Sources.LeftHand;  //왼손
    public SteamVR_Input_Sources rightHand = SteamVR_Input_Sources.RightHand;   //오른손
    public SteamVR_Behaviour_Pose controllerPose;   //컨트롤러 정보
    public SteamVR_Action_Boolean grabAction = SteamVR_Actions.default_GrabPinch;   //그랩 액션
    public SteamVR_Action_Boolean resultAction = SteamVR_Actions.default_Teleport;  //정답 표기 액션

    private GameObject collidingObject;     //현재 충돌중인 객체
    private GameObject objectInHand;    //플레이어가 잡은 객체

    public static bool showHand = true; //손 컨트롤거 보여주기
    private int oneRoomQ = 0;   //맞춘 정답 갯수

    private bool answer = false;    //정답 확인 가능한 타이밍

    public GameObject mainCamera = null;    //카메라 찾기

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");    //메인 카메라 찾기
        
    }

    // Start is called before the first frame update
    private void Update()
    {
        //물체잡기
        if (GetGrab() || GetGrab2())
        {
            if (collidingObject)
            {
                GrabObject();
            }
        }
        //물체 놓기
        if (GetGrabUp() || GetGrabUp2())
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }

        //손 보여주고 사라지기
        foreach (var hand in Player.instance.hands)
        {
            if (showHand)//손 보여주기
            {
                hand.Show();
                hand.SetSkeletonRangeOfMotion(Valve.VR.EVRSkeletalMotionRange.WithController);  
            }
            else
            {   //손 사라짐
                hand.Hide();
                hand.SetSkeletonRangeOfMotion(Valve.VR.EVRSkeletalMotionRange.WithoutController);
            }
        }

        //정답 맞추기
        if (answer)
        {
            if (resultAction.GetStateDown(leftHand) || resultAction.GetStateDown(rightHand))
            {
                if (objectInHand.CompareTag("OneRoomYes"))      //정답이라면
                {
                    oneRoomQ++; //정답 갯수 증가
                    //정답
                    if (oneRoomQ < 3)   //정답을 모두 찾지 못했을 때
                    {
                        OUI();  //정답 UI 
                        soundEvent.effect.sound7(); //사운드
                        Invoke("OUI", 1f);  //정답 Ui 비활성화
                    }
                    
                    Destroy(objectInHand);  //손 안에 있는 물체 삭제
                    ReleaseObject();    //놓아줌

                    if (oneRoomQ == 3)  //정답 갯수가 3개라면 성공
                    {
                        SuccessUI();    //성공 UI
                        soundEvent.effect.sound15();    //사운드  
                        Invoke("SuccessUI", 3f);    //UI 비활성화
                    }
                }
                else if (!objectInHand.CompareTag("OneRoomYes"))     //정답이 아니라면
                {
                    XUI();  //정답 아님 UI
                    soundEvent.effect.sound8(); //사운드
                    Invoke("XUI", 1f);  //UI 비활성화
                }
            }
        }
        
    }

    void OUI()
    {
        mainCamera.GetComponent<Room1OX>().OneYesUI();
    }
    void XUI()
    {
        mainCamera.GetComponent<Room1OX>().OneNoUI();
    }
    void SuccessUI()
    {
        mainCamera.GetComponent<Room1OX>().OnSuccess();
    }
    public bool GetGrab()
    {
        return grabAction.GetStateDown(leftHand);
    }

    public bool GetGrab2()
    {
        return grabAction.GetStateDown(rightHand);
    }

    public bool GetGrabUp()
    {
        return grabAction.GetStateUp(leftHand);
    }

    public bool GetGrabUp2()
    {
        return grabAction.GetStateUp(rightHand);
    }

    //충돌이 시작되는 순간
    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    //충돌중일 때 
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    //손 생김
    //충돌이 끝나는 순간
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)   //충돌중인 오브젝가 없다면 함수 나감
        {
            return;
        }
        
        collidingObject = null; //충돌 중인 물체 없음
        
    }
    //충돌중인 객체로 설정
    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>())  //잡고 있는 물게가 있거나 충돌체가 리지드바디를 지니지 않았다면 리턴
        {
            return;
        }
        collidingObject = col.gameObject;   //충돌 중인 물체로 지정
        
        
    }

    //객체를 잡음
    private void GrabObject()
    {
        
        objectInHand = collidingObject;  //잡은 객체로 설정
        collidingObject = null; //충돌 객체 해제

        var joint = AddFixedJoint();    //고정 시키기
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();

        //잡았는 물체가 정답이라면
        answer = true;

    }

    //손에 고정
     private FixedJoint AddFixedJoint()
     {
        soundEvent.effect.sound9(); //사운드
        showHand = false;   //손 사라짐
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();  //FixedJoint =>객체들을 하나로 묶어 고정시켜줌
        fx.breakForce = 20000;      //breakForce =>조인트가 제거되도록 하기 위한 필요한 힘의 크기
        fx.breakTorque = 20000;     //breakTorque =>조인트가 제거되도록 하기 위한 필요한 토크
        return fx;
     }

    //객체를 놓음
    private void ReleaseObject()
    {
        soundEvent.effect.sound10();    //사운드
        showHand = true;    //손 보임
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;    //자식 오브젝트 연결 해제
            Destroy(GetComponent<FixedJoint>());    //자식 오브젝트 넣은 컴포넌트 삭제

            //controllerPose.GetVeloecity() => 컨트롤러의 속도
            objectInHand.GetComponent<Rigidbody>().velocity = controllerPose.GetVelocity();
            //controllerPose.GetAngularVelocity() => 컨트롤러의 각속도
            objectInHand.GetComponent<Rigidbody>().angularVelocity = controllerPose.GetAngularVelocity();
        }
        objectInHand = null;    //손에 아무것도 없음
        answer = false; //정답 찾기 불가능
    }


}
