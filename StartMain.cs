using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;
using UnityEngine.SceneManagement;
using Valve.VR;
using UnityEngine.Audio;

public class StartMain : MonoBehaviour  //전반적인 게임 컨트롤러 & 메인 화면에서 다른 방들로 이동 시 실행 될 셋팅들
{
    public AudioClip[] clips;
    private AudioSource audioSource = null;
    private bool audioON = false;   //사운드 ONOFF

    public string goRoomName = null;    //이동 되는 방 이름
    public static int roomNum = 0;      //이동 되는 방 번호
    private GameObject oneRoomA;    //첫번째 방=고흐
    private GameObject twoRoomA;    //두번째 방=세잔
    private GameObject threeRoomA;  //세번째 방=몬드리안
    

    public GameObject mainObject = null;    //메인
    public GameObject canvasStart = null;   //시작티켓 UI
    public GameObject canvasFold = null;    //UI
    public GameObject ticket = null;    //티켓
    public GameObject twoRetry = null;  //2번방 다시 하기

    public GameObject pointer1 = null;  //손
    public GameObject pointer2 = null;  //손

    public GameObject ex1 = null;   //1번방 게임 설명
    public GameObject ex2 = null;   //2번방 게임 설명
    public GameObject ex3 = null;   //3번방 게임 설명

    GameObject a = null;    //포인트
    GameObject b = null;    //포인트 cube

    public GameObject mainCamera = null;    //메인 카메라
    public static StartMain instance = null;    //인스턴스화

    //싱글턴
    private void Awake()
    {
        //처음에 생성 된 오브젝트 유지
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            instance = this;
        }
        DontDestroyOnLoad(gameObject);  //씬 이동 해도 삭제 ㄴㄴ
    }

    // Start is called before the first frame update
    public void Start()
    {
        //방 관리
        oneRoomA = GameObject.FindGameObjectWithTag("room1");
        twoRoomA = GameObject.FindGameObjectWithTag("room2");
        threeRoomA = GameObject.FindGameObjectWithTag("room3");
        //설명 UI
        ex1 = GameObject.FindGameObjectWithTag("1roomExUI");
        ex2 = GameObject.FindGameObjectWithTag("2roomExUI");
        ex3 = GameObject.FindGameObjectWithTag("3roomExUI");
        //서브 방 비활성화
        oneRoomA.SetActive(false);
        twoRoomA.SetActive(false);
        threeRoomA.SetActive(false);
        //필요한 오브젝트들 정의
        mainObject = GameObject.Find("MainRoom");
        canvasStart = GameObject.Find("startCanvas");
        canvasFold = GameObject.Find("Canvas");
        ticket = GameObject.FindGameObjectWithTag("ticketUI");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        audioSource = GetComponent<AudioSource>();
        audioON = true; //소리 ON
        mainAudio();    //메인 소리 켜기
        //UI 활성화 비활성화
        mainObject.SetActive(false);
        canvasStart.SetActive(true);
        canvasFold.SetActive(false);
        ticket.SetActive(false);
        //포인터 큐브 정의
        a = pointer1.transform.Find("New Game Object").gameObject;
        b = pointer2.transform.Find("New Game Object").gameObject;
    }
   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))  //ESC 클릭시 종료
        {
            Application.Quit(); //종료
        }
    }

    void mainAudio()    //메인 화면 소리 켜기
    {
        AudioPlay(clips[0]);
    }

    void subAudio() //서브방 소리 켜기
    {
        AudioPlay(clips[1], 0.3f);
    }

    public void AudioPlay(AudioClip clip, float volume = 0.5f)
    {
        if (audioSource.clip == clip)   //전달 받은 audioCilp이 같은지 바교
            audioSource.Stop(); //재생중인거 중단

        audioSource.clip = clip;    //사운드 정의
        audioSource.volume = volume;    //사운드 크기 정의

        audioSource.Play(); //재생
    }

    //1룸 이동
    public void RoomOne()
    {
        roomNum = 1;
        SubMove();  //서브룸 이동
        Invoke("showOne", 0.1f);    //가끔 메인룸과 충돌이 이러나 시간텀을 줌

    }
    void showOne()
    {
        oneRoomA.SetActive(true);   //1번룸 활성화
    }
    //2룸 이동
    public void RoomTwo()
    {
        roomNum = 2;
        SubMove();  //서브 룸 이동
        twoRoomA.SetActive(true);   //2번룸 활성화
        ex2.SetActive(true);    //설명 창 띄우기
        twoRetry = GameObject.FindGameObjectWithTag("2roomScoreUI");
        twoRetry.gameObject.GetComponent<AppleUI>().RetryRoom2();   //게임을 시작 하기 위한 준비(다시 플레이 하러 들어갔을 때 재부팅)
    }
    //3룸 이동
    public void RoomThree()
    {
        roomNum = 3;
        SubMove();  //서브 룸 이동
        threeRoomA.SetActive(true); //3번룸 활성화
        ex3.SetActive(true);    //설명 창 띄우기
    }

    //서브 룸 이동
    private void SubMove()
    {
        subAudio(); //서브 룸 사운드
        audioON = true; //사운드 On

        //메인 씬 안보이게
        mainObject.SetActive(false);
        canvasFold.SetActive(false);
        
        showControllers.showController = false; //컨트롤러 삭제
        yesPointer();   //포인트 보이게 하기
    }

    //main 시작
    public void MainStart()
    {
        ticket.SetActive(false);    //비활성화
        mainObject.SetActive(true); //메인룸 활성화
        canvasFold.SetActive(true); //메인룸 UI 활성화

        noPointer();    //포인트 안보이게 하기
    }
    //다시 메인룸 입장 시   
    public void ReMain()
    {
        showControllers.showController = true;  //컨트롤로 보이게 하기
        soundEvent.effect.sound0(); //룸 이동 시 나는 사운드 재생
        mainObject.SetActive(true); //메인룸 활성화
        canvasFold.SetActive(true); //메인룸 UI 활성화
        mainAudio();    //소리 플레이
    }

    // 처음에 포인터 사라지기
    public void noPointer()
    {
        //포인터 기능 사라지기
        pointer1.GetComponent<PointerHandler>().enabled = false;
        pointer1.GetComponent<SteamVR_LaserPointer>().enabled = false;
        pointer2.GetComponent<PointerHandler>().enabled = false;
        pointer2.GetComponent<SteamVR_LaserPointer>().enabled = false;
        //선 숨기기
        a.SetActive(false);
        b.SetActive(false);
    }

    //포인터 보이기
    public void yesPointer()
    {
        //기능
        pointer1.GetComponent<PointerHandler>().enabled = true;
        pointer1.GetComponent<SteamVR_LaserPointer>().enabled = true;
        pointer2.GetComponent<PointerHandler>().enabled = true;
        pointer2.GetComponent<SteamVR_LaserPointer>().enabled = true;
        //선 보이게 하기
        a.SetActive(true);
        b.SetActive(true);
    }

    //1room 이동 후 설명 창 클릭 시 손 세팅
    public void noPointerOneRoom()
    {
        noPointer();    //포인트 사라지게 하기

        mainCamera.GetComponent<Room1OX>().GoOneUI();   //ox UI

        pointer1.GetComponent<GrapTest>().enabled = true;   //잡기 기능 활성화
        pointer1.GetComponent<SphereCollider>().enabled = true; //콜라이더 컴포넌트 활성화

        pointer2.GetComponent<GrapTest>().enabled = true;   //잡기 기능 활성화
        pointer2.GetComponent<SphereCollider>().enabled = true; //콜라이더 컴포넌트 활성화
    }
    //2room 이동 후 설명 창 클릭 시 손세팅
    public void OnTwoRoom()
    {
        noPointer();    //포인트 사라지게 하기
        pointer1.GetComponent<SphereCollider>().enabled = true; //콜라이더 컴포넌트 활성화
        pointer2.GetComponent<SphereCollider>().enabled = true; //콜라이더 컴포넌트 활성화
    }
    
    //1room 나갈 때 손 셋팅
    public void PointerOneRoomOut()
    {
        pointer1.GetComponent<GrapTest>().enabled = false;  //잡기 기능 비활성화
        pointer1.GetComponent<SphereCollider>().enabled = false;    //콜라이더 컴포넌트 비활성화

        pointer2.GetComponent<GrapTest>().enabled = false;  //잡기 기능 비활성화
        pointer2.GetComponent<SphereCollider>().enabled = false;    //콜라이더 컴포넌트 비활성화
    }
    //2room 나갈 때 손 셋팅
    public void OutTwoRoom()
    {
        pointer1.GetComponent<SphereCollider>().enabled = false;    //콜라이더 컴포넌트 비활성화
        pointer2.GetComponent<SphereCollider>().enabled = false;    //콜라이더 컴포넌트 비활성화
    }
    //1룸에서 홈으로
    public void OneHome()
    {
        ex1.SetActive(true);    //설명 ui 활성화
        AudioPlay(clips[0]);    //메인 사운드
        oneRoomA.SetActive(false);  //1번룸 비활성화
        PointerOneRoomOut();    //포인터 끄기
        noPointer();    //포인터 끄기
        ReMain();   //메인 재입장
        mainCamera.GetComponent<Room1OX>().OutOneUI();  //ox UI
    }
    //2룸에서 홈으로
    public void TwoHome()
    {
        ex2.SetActive(true);    //설명 ui 활성화
        OutTwoRoom();   //2번룸 나옴으로 손 셋팅
        AudioPlay(clips[0]);    //메인 사운드
        twoRoomA.SetActive(false);  //2번룸 비활성화
        noPointer();    //포인터 끄기
        ReMain();   //메인 재입장
    }
    //3룸에서 홈으로
    public void ThreeHome()
    {
        Invoke("ThreeHome1s", 1f);  //반복문 종료를 위한 시간 타임 기다려 주고 이동
    }
    private void ThreeHome1s()
    {
        noPointer();    //포이터 끄기
        ex3.SetActive(true);    //설명 ui 활성화
        AudioPlay(clips[0]);    //씬 이동 사운드
        threeRoomA.SetActive(false);    //3번룸 비활성화
        ReMain();   //메인 재입장
    }
    //나가기 버튼 클릭시
    public void SeeExitUI()
    {
        Application.Quit(); //종료
    }
}
