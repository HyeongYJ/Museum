using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ChangeM : MonoBehaviour    //머터리얼 바꾸기 및 몬드리안 방 게임 구현
{
    public SteamVR_Input_Sources leftHand = SteamVR_Input_Sources.LeftHand; //왼쪽 손
    public SteamVR_Input_Sources rightHand = SteamVR_Input_Sources.RightHand;   //오른쪽 손
    public SteamVR_Action_Boolean grabAction = SteamVR_Actions.default_GrabPinch;   //손 동작

    public bool playTime = false;   //초 세리기 ㄱㄱ
    private float time = 0;         //시간초
    public static bool gaming = false;    //조각 버튼 누르기 가능
    private bool playing = false;   //문제 풀기
    private bool guide = false;     //문제
    private int step = 0;   //문제 단계
    public bool stopTimer = false;   //반복문 종료

    private int i = 0;  //for문 관리
    public GameObject[] levelLight; //레벨 UI 관리
    public GameObject fail; //실패 UI
    public GameObject success;  //성공 UI
    
    public LightMaterial[] pie; //머터리얼 바꿀 오브젝트들의 소스 컴포넌트

    // Start is called before the first frame update
    void Start()
    {
        //머터리얼 바꾸기 위해 컴포넌트 불러오기
        pie = new LightMaterial[8];
        //조각들
        pie[0] = GameObject.FindGameObjectWithTag("pie1").GetComponent<LightMaterial>();
        pie[1] = GameObject.FindGameObjectWithTag("pie2").GetComponent<LightMaterial>();
        pie[2] = GameObject.FindGameObjectWithTag("pie3").GetComponent<LightMaterial>(); 
        pie[3] = GameObject.FindGameObjectWithTag("pie4").GetComponent<LightMaterial>();
        pie[4] = GameObject.FindGameObjectWithTag("pie5").GetComponent<LightMaterial>();
        pie[5] = GameObject.FindGameObjectWithTag("pie6").GetComponent<LightMaterial>();
        pie[6] = GameObject.FindGameObjectWithTag("pie7").GetComponent<LightMaterial>();
        pie[7] = GameObject.FindGameObjectWithTag("pie8").GetComponent<LightMaterial>();

        //라운드 UI 아이콘 비활성화
        for (int i = 0; i<levelLight.Length; i++)
        {
            levelLight[i].SetActive(false); //라운드 UI
        }
        fail.SetActive(false);  //실패 UI 비활성화
        success.SetActive(false);   //성공 UI 비활성화
    }

    public void startWhile()    //게임 시작
    {
        StartCoroutine(Timer());    //카운트 다운 시작 호출
    }

    public void restartHome()   //홈 화면으로 돌아가기
    {
        for (int i = 0; i < levelLight.Length; i++) //라운드 UI 비활성화
        {
            levelLight[i].SetActive(false);
        }
        for (i = 0; i < pie.Length; i++)    //빛나는 조각들 빛 끄기
        {
            StartCoroutine(pie[i].ResetMaterial(0f));   //머터리얼 변환
        }
        stopTimer = true;   //시간 정지
        fail.SetActive(false);  //실패 Ui 비활성화
        success.SetActive(false);   //성공 UI 비활성화
        LightMaterial.a = null; //정답 초기화
        step = 0;   //레벨 초기화
        time = 0;   //시간 초기화
        playTime = false;   //플레이 정지
        gaming = false; //조각 버튼 정지
        guide = false;  //가이드 정지
        playing = false;    //플레이 정지
    }

    public void PlayTime()  //시작 버튼
    {
        step++; //레벨 증가
        time = 0;   //시간 0초
        playTime = true;    //플레이 시작
        guide = true;   //가이드 기다림

    }

    public void RePlay()    //게임 다시 시작
    {
        step = 0;   //레벨 초기화
        PlayTime(); //다시 게임 시작
    }
    

    IEnumerator Timer() //몬드리안 방 게임 시작 관련
    {
        while (true)
        {
            if (playTime)   //게임 시작 누름
            {
                if (guide)  //안내 가능 시간
                {
                    //3초 후 버튼 가이드 시작
                    if (time == 3)     //1단계
                    {
                        //첫번째 불 들어오기
                        pie[6].LightChange();
                    }
                    else if (time == 4)
                    {
                        //두번째 불 켜짐
                        pie[1].LightChange();
                    }
                    else if (time == 5)
                    {
                        //세번째 불 켜심
                        pie[0].LightChange();
                        if (step == 1)  //레벨 1이라면
                        {
                            time = 0;   //시간 초기화
                            guide = false;  //가이드 끝
                            playing = true; //플레이 시작
                        }
                    }
                    else if (time == 6)          //2단계
                    {
                        //네번째 불 켜짐
                        pie[3].LightChange();
                        if (step == 2)  //레벨 2라면
                        {
                            time = 0;   //시간 초기화
                            guide = false;  //가이드 끝
                            playing = true; //플레이 시작
                        }
                    }
                    else if (time == 7)      //3단계
                    {
                        //다섯번째 불 켜짐
                        pie[7].LightChange();   
                        if (step == 3)  //레벨 3이라면
                        {
                            time = 0;   //시간 초기화
                            guide = false;  //가이드 끝
                            playing = true; //플레이 시작
                        }
                    }
                    else if (time == 8)      //4단계
                    {
                        //6번째 불 켜짐
                        pie[2].LightChange();
                        if (step == 4)  //레벨 4라면
                        {
                            time = 0;   //시간 초기화
                            guide = false;  //가이드 끝
                            playing = true; //플레이 시작
                        }
                    }
                    else if (time == 9)      //5단계
                    {
                        //7번째 불 켜짐
                        pie[5].LightChange();
                        if (step == 5)  //레벨 5라면
                        {
                            time = 0;   //시간 초기화
                            guide = false;  //가이드 끝
                            playing = true; //플레이 시작
                        }
                    }
                    else if (time == 10)      //6단계
                    {
                        //8번째 불 켜짐
                        pie[4].LightChange();
                        if (step == 6)
                        {
                            time = 0;   //시간 초기화
                            guide = false;  //가이드 끝
                            playing = true; //플레이 시작
                        }
                    }
                }

                if (playing)    //플레이어 게임 시작
                {
                    if (time == 1)  //1초 텀 주고 시작
                    {
                        soundEvent.effect.sound12();    //소리
                        gaming = true;  //버튼 누르기 가능
                    }
                    //버튼 누르기
                    else if (time == 11)    //11초 되면 버튼 누르기 종료
                    {
                        time = 0;   //시간 초기화
                        gaming = false; //버튼 누르기 불가능
                        //머터리얼 초기화
                        for (i = 0; i < pie.Length; i++)
                        {
                            StartCoroutine(pie[i].ResetMaterial(0f));   //머터리얼 변환
                        }
                        
                        playing = false;    //플레이 중지
                        playTime = false;   //시간 멈춤
                        Answer(); //정답 확인
                    }
                }

                time += 1;  //시간 1초씩 증가
                yield return new WaitForSeconds(1f);    //1초에 한번 호출
                
            }
            else
            {
                yield return null;
            }
            if (stopTimer)  //나가기
            {
                stopTimer = false;  //호출 했으니 비활성화
                yield break;    //호출 끝내기
            }
        }
    }

    //버튼 안내 후 사라지기
    IEnumerator ReMaterialCo(int a, float t)
    {
        yield return new WaitForSeconds(t); //t초 후에 실행
        pie[a].ResetMaterial(0f);   //빛나는 버튼 사라지기
    }

    private void Answer()   //정답 확인
    {
        if(step == 1)   //1단계일때     ====================== 아래 소스코드들 동일 시 (6단계만 조금 다름) ======================
        {
            if (LightMaterial.a == "610")   //정답 동일 시
            {
                LightMaterial.a = null; //정답 초기화
                soundEvent.effect.sound12(); ;  //사운드
                PlayTime();     //게임 시간 진행
                levelLight[0].SetActive(true);  //1라우드 UI 활성화
            }
            else
            {
                step = 0;   //0단계로 초기화
                LightMaterial.a = null; //정답 초기화
                fail.SetActive(true);   //실패 Ui
                soundEvent.effect.sound8(); //사운드
                //라운드 UI 비활성화
                for (int i = 0; i < levelLight.Length; i++)
                {
                    levelLight[i].SetActive(false);
                }
            }
        }
        else if (step == 2)
        {
            if (LightMaterial.a == "6103")
            {
                LightMaterial.a = null;
                soundEvent.effect.sound12(); ;
                PlayTime();
                levelLight[1].SetActive(true);
            }
            else
            {
                step = 0;
                LightMaterial.a = null;
                fail.SetActive(true);
                soundEvent.effect.sound8();
                for (int i = 0; i < levelLight.Length; i++)
                {
                    levelLight[i].SetActive(false);
                }
            }
        }
        else if (step == 3)
        {
            if (LightMaterial.a == "61037")
            {
                LightMaterial.a = null;
                soundEvent.effect.sound12(); ;
                PlayTime();
                levelLight[2].SetActive(true);
            }
            else
            {
                step = 0;
                LightMaterial.a = null;
                fail.SetActive(true);
                soundEvent.effect.sound8();
                for (int i = 0; i < levelLight.Length; i++)
                {
                    levelLight[i].SetActive(false);
                }
            }
        }
        else if (step == 4)
        {
            if (LightMaterial.a == "610372")
            {
                LightMaterial.a = null;
                soundEvent.effect.sound12(); ;
                PlayTime();
                levelLight[3].SetActive(true);
            }
            else
            {
                step = 0;
                LightMaterial.a = null;
                fail.SetActive(true);
                soundEvent.effect.sound8();
                for (int i = 0; i < levelLight.Length; i++)
                {
                    levelLight[i].SetActive(false);
                }
            }
        }
        else if (step == 5)
        {
            if (LightMaterial.a == "6103725")
            {
                LightMaterial.a = null;
                soundEvent.effect.sound12(); ;
                PlayTime();
                levelLight[4].SetActive(true);
            }
            else
            {
                step = 0;
                LightMaterial.a = null;
                fail.SetActive(true);
                soundEvent.effect.sound8();
                for (int i = 0; i < levelLight.Length; i++)
                {
                    levelLight[i].SetActive(false);
                }
            }
        }
        else if (step == 6)
        {
            if (LightMaterial.a == "61037254")
            {
                LightMaterial.a = null;
                soundEvent.effect.sound15(); ;
                levelLight[5].SetActive(true);
                success.SetActive(true);    //최종 성공
            }
            else
            {
                step = 0;
                LightMaterial.a = null;
                fail.SetActive(true);
                soundEvent.effect.sound8();
                for (int i = 0; i < levelLight.Length; i++)
                {
                    levelLight[i].SetActive(false);
                }
            }
        }

    }
}
