using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.Audio;

public class LightMaterial : MonoBehaviour  //몬드리안방 조각 머터리얼 바꾸기
{
    public AudioClip piano; //사운드
    public AudioSource audioSource = null;

    public SteamVR_Input_Sources leftHand = SteamVR_Input_Sources.LeftHand; //왼손
    public SteamVR_Input_Sources rightHand = SteamVR_Input_Sources.RightHand;   //오른손
    public SteamVR_Action_Boolean grabAction = SteamVR_Actions.default_GrabPinch;   //버튼 누르기


    public Material[] materials;    //머터리얼 색상
    private Renderer rend;  //색 보이기
    public Animator buttonMove; //애니메이션
    public int num = 0; //자신의 숫자 지니기
    public bool isa = false;    //정답 입력

    public static string a = null;  //정답

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        buttonMove = GameObject.Find("3rdRoomObjectButton").GetComponent<Animator>();
        audioSource = GameObject.FindGameObjectWithTag("3roomButtons").GetComponent<AudioSource>();
        rend.enabled = true;
        rend.sharedMaterial = materials[0]; //처음에는 빛나지 않는 머터리얼
    }
    
    //player 정답 누르기
    private void OnCollisionStay(Collision collision)
    {
        if (ChangeM.gaming) //게임 중일 때
        {
            if (grabAction.GetStateDown(leftHand) || (grabAction.GetStateDown(rightHand)))  //그립 액션 눌렀을 때
            {
                if (collision.gameObject.tag == "hand")
                {
                    //머티리얼 바뀌기
                    rend.sharedMaterial = materials[1];
                    
                    //애니메이션
                    buttonMove.SetInteger("moveButton", num);
                    isa = true;
                }
            }
            if (grabAction.GetStateUp(leftHand) || (grabAction.GetStateUp(rightHand)))  //그립 액션 때었을 때
            {
                if (isa)    //정답 장성 한번만 하기 위해
                {
                    audioSource.clip = piano;   //해당 피아노 노래 정의
                    audioSource.Play(); //해당 피아노 사운드 플레이
                    a += num.ToString();    //정답 입력
                    isa = false;    //정답 작성했으니 이제 작성 못하게 막기
                }
            }
        }
     }
    
    //불 켜기
    public void LightChange()
    {
        audioSource.clip = piano;   //자신의 피아노 사운드 정의
        audioSource.Play(); //자신의 사운드 플레이
        rend.sharedMaterial = materials[1]; //머터리얼 변환
        buttonMove.SetInteger("moveButton", num);   //애니메이션
        StartCoroutine(ResetMaterial(0.8f));    //0.8초 후 불 끄기
    }
    //불 끄기
   public IEnumerator ResetMaterial(float x)
    {
        yield return new WaitForSeconds(x); //x초 후 실행
        
        rend.sharedMaterial = materials[0]; //머터리얼 변환
        buttonMove.SetInteger("moveButton", 10);     //애니메이션
    }

}
