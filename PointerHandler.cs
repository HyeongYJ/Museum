using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.Extras;

public class PointerHandler : MonoBehaviour //포인트 클릭 이벤트 관리
{
    //pointer
    public SteamVR_LaserPointer laserPointer;
    
    //UI
    public GameObject guide = null;
    public GameObject ticket = null;
    public GameObject rightTicket;
    public GameObject appleUI;

    //pointer OnOff
    public PointerHandler pointer1 = null;
    public SteamVR_LaserPointer pointer2 = null;

    //관련 오브젝트들
    public GameObject startcontroller = null;
    public GameObject room3Button = null;

    // Start is called before the first frame update
    void Start()
    {
        //UI 넣기
        guide = GameObject.FindGameObjectWithTag("guideUI");
        ticket = GameObject.FindGameObjectWithTag("ticketUI");
        rightTicket = GameObject.Find("TicketRight");
        startcontroller = GameObject.FindGameObjectWithTag("GameController");
        //포인트 정의
        pointer1 = GetComponent<PointerHandler>();
        pointer2 = GetComponent<SteamVR_LaserPointer>();

        laserPointer.PointerIn += PointerInside;    //포인트 들어갔을 때
        laserPointer.PointerOut += PointerOutside;  //포인트 나갔을 때
        laserPointer.PointerClick += PointerClick;  //클릭했을 때

    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {


    }

    public void PointerClick(object sender, PointerEventArgs e) //클릭 시 아래 소스 코드 실행
    {
        //main방
        if (e.target.CompareTag("guideUI"))
        {
            guide.SetActive(false);
            ticket.SetActive(true);
        }
        if (e.target.CompareTag("ticketUI"))
        {
            rightTicket.GetComponent<DateScript>().Rotate();    //클릭시 실행

        }
        //고흐방
        if (e.target.CompareTag("1roomExUI"))
        {
            e.target.gameObject.SetActive(false);
            startcontroller.GetComponent<StartMain>().noPointerOneRoom();   //포인트 사라지게 하기
            soundEvent.effect.sound2();
        }
        //사과방
        if (e.target.CompareTag("2roomExUI"))
        {
            e.target.gameObject.SetActive(false);
            startcontroller.GetComponent<StartMain>().OnTwoRoom();  //포인터 사라지기
            appleUI.gameObject.GetComponent<AppleUI>().startButton();   //게임 시작 준비
            GameObject.FindGameObjectWithTag("2roomScoreUI").GetComponent<AppleUI>().startButton(); //만약 appleUI 오브젝트를 소실 시를 대비함
            soundEvent.effect.sound2();
        }
        //몬드리안방
        if (e.target.CompareTag("3roomExUI"))
        {
            e.target.gameObject.SetActive(false);
            startcontroller.GetComponent<StartMain>().noPointerOneRoom();   //포인터 사라지기
            room3Button = GameObject.FindGameObjectWithTag("3roomButtons");
            room3Button.GetComponent<ChangeM>().startWhile();   //게임 시작 준비
            room3Button.GetComponent<ChangeM>().PlayTime(); //게임 준비
            soundEvent.effect.sound2();
        }
        

    }
}
