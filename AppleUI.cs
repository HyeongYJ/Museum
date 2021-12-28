using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;
using Valve.VR.Extras;


public class AppleUI : MonoBehaviour    //사과 UI 관리 및 게임 구현 코드
{
    public AudioClip timerSound;    //제촉 시간 노래
    public AudioSource audioSource = null;
    private bool soundS = false;    //사운드 시작

    public SteamVR_Input_Sources leftHand = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Input_Sources rightHand = SteamVR_Input_Sources.RightHand;
    public SteamVR_Action_Boolean trigger = SteamVR_Actions.default_GrabGrip;

    //Apple Ui 캠버스 안에 컴포넘트로 존재함
    private float countDown; //카운드 시간 설정
    private bool goCount = false;   //카운드 다운 OnOff
    private int countUiNum = 0;     //시간에 다른 카운트 UI

    public static bool gameStart = false;  //게임 시작
    private int level = 0;      //게임 레벨

    //카운트 UI들
    public GameObject readyUI = null;
    public GameObject startUI = null;
    public GameObject successUI = null;
    public GameObject roundUI = null;
    public GameObject failUI = null;
    public GameObject retryUI = null;

    //UI 텍스트들
    public Text score, cutline, times, round;   

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //UI들 비활성화
        roundUI.SetActive(false);
        successUI.SetActive(false);
        startUI.SetActive(false);
        readyUI.SetActive(false);
        failUI.SetActive(false);
        retryUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        startCount();   //시간 관리
        PlayGame(); //시간 제한
        ScoreUI();  //점수 UI
    }

    public void RetryRoom2()    //다시 시작 시
    {
        level = 0;  //레벨 초기화
        cutline.text = "3"; //텍스트 초기화
        round.text = "1 Round"; //텍스트 초기화
        goCount = false;    //시간초 시작 컨트롤
        AppleScore.score = 0;   //점수 초기화
        countUiNum = 0; //UI 점수 초기화
        countDown = 0;  //시간 초기화
    }

    public void startButton()  //카운드 시작
    {
        retryUI.SetActive(false);   //다시 시작 버튼 비활성화
        goCount = true; //카운트 활성화
        readyUI.SetActive(true);    //ready
        soundEvent.effect.sound5(); //사운드
    }

    void startCount()   //시작 시간초
    {
        if (goCount)    //카운트 시작
        {
            countDown += Time.deltaTime;    //초 세리기
            if (countDown > 1f) //1초 지나면
            {
                countDown = 0;  //초기화
                UiChan();   //UI 활성화 하는 함수
                
            }
        }
    }

    
    private void UiChan()   //UI 3,2,1와 GameTime 시작
    {
        
        countUiNum ++;  //UI 관련 카운트 증가
        switch (countUiNum)
        {
            case 1:
                break;
            case 2:
                readyUI.SetActive(false);   //read UI 비활성화
                startUI.SetActive(true);    //start UI 활성화 
                break;
            default:
                startUI.SetActive(false);   //start UI 비활성화
                countDown = 10; //10초 시간제한
                countUiNum = 0; //UI 관련 숫자 초기화
                gameStart = true;   //게임 시작
                soundS = true;  //소리 On
                goCount = false;    //초 세리기 중기
                break;
        }
    }

    private void PlayGame()     //게임 제한 초
    {
        if (gameStart)  //게임 시작시
        {
            if (soundS) //사운드 호출
            {
                audioSource.PlayOneShot(timerSound);    //똑딱똑딱
                soundS = false; //사운드 호출 끝
            }
            countDown -= Time.deltaTime;    //시간 감소
            times.text = Mathf.Round(countDown).ToString(); //시간 초 보여주기
            if (countDown <= 0) //게임 멈춰
            {
                gameStop(); //게임 멈춤
            }            
        }
    }



    private void gameStop()     //게임 멈춤
    {
        gameStart = false;  //게임초 멈춤
        countDown = 0; //타이머 리셋
        level++;    //난이도 레벨

        switch (level)
        {
            case 1: //레벨 1일 경우
                if (AppleScore.score >= 3)  //3점 이상일 경우 성공
                {
                    onOffUI();//라운드 성공 UI
                    soundEvent.effect.sound12();    //소리
                    Invoke("onOffUI", 2f);  //2초 후 UI 사라짐
                    round.text = "2 Round"; //2라운드로 텍스트 바꿈
                    cutline.text = "9"; //조건 점수 9로 증가
                    Invoke("startButton", 3f);  //3초 후 다음 라운드 시작
                }
                else
                {
                    //실패 UI
                    failUI.SetActive(true); //실패 UI
                    soundEvent.effect.sound6(); //소리
                    Invoke("fail", 2f); // UI 비활성화
                    round.text = "1 Round"; //1라운드 초기화
                    cutline.text = "3"; //성공 조건 점수 3점
                    retryUI.SetActive(true);    //다시 시작 버튼
                    StartMain.instance.yesPointer();    //포인트 보이게 하기
                    AppleScore.score = 0;   //점수 초기화
                    level = 0;  //레벨 0으로 초기화
                }
                break;
            case 2: //레벨 2일 경우
                if (AppleScore.score >= 9)  //9점 이상일 경우 성공
                {
                    onOffUI();//성공 UI
                    soundEvent.effect.sound12();    //소리
                    Invoke("onOffUI", 2f);  //2추 후 UI 사라짐
                    round.text = "3 Round"; //3라운드로 텍스트 바꿈
                    cutline.text = "18"; //조건 점수 18로 증가
                    Invoke("startButton", 3f);  //3초 후 다음 라운드 시작
                }
                else
                {
                    //실패 UI
                    failUI.SetActive(true); //실패 UI
                    soundEvent.effect.sound6(); //소리
                    Invoke("fail", 2f); //UI 비활성화
                    retryUI.SetActive(true);    //다시 시작 버튼
                    round.text = "1 Round"; //1라운드 초기화
                    cutline.text = "3"; //성공 조건 점수 3점 초기화
                    StartMain.instance.yesPointer();    //포인트 보이게 하기
                    AppleScore.score = 0;   //점수 초기화
                    level = 0;  //레벨 0으로 초기화
                }
                break;
            case 3: //레벨 3일 경우
                if (AppleScore.score >= 18) //18점 이상일 경우 성공
                {
                    Clear();    //성공 UI
                    soundEvent.effect.sound15();   //소리 
                    Invoke("Clear", 5f);    //클리어 UI 비활성화
                    cutline.text = "3"; //레벨 초기화
                    AppleScore.score = 0;   //점수 초기화
                    level = 0;  //레벨 0으로 초기화
                }
                else
                {
                    failUI.SetActive(true); //실패 UI
                    soundEvent.effect.sound6(); //소리
                    round.text = "1 Round"; //1라운드로 초기화
                    cutline.text = "3"; //3점으로 초기화
                    Invoke("fail", 2f); //실패 UI 비활성화
                    retryUI.SetActive(true);    //다시하기 버튼 활성화
                    StartMain.instance.yesPointer();    //보인트 보이게 하기
                    AppleScore.score = 0;   //점수 초기화
                    level = 0;  //레벨 초기화
                }
                break;
        }
    }
    void onOffUI()  //라운드 성공 UI
    {
        roundUI.SetActive(!roundUI.activeSelf); //활성화 비활성화 관리
    }

    private void fail()
    {
        failUI.SetActive(false);    //실패 UI 비활성화
    }

    void Clear()    //성공 UI
    {
        successUI.SetActive(!successUI.activeSelf); //성공 활성화 비활성화 관리

    }

    private void ScoreUI()  //Text 표기
    {
        score.text = AppleScore.score.ToString();   //문자로 표기
    }

}
