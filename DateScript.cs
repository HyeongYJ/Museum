using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DateScript : MonoBehaviour //시작 시 티켓 날짜 불러오기 및 인트로 관리
{
    public GameObject rotate = null;    //오른쪽 티켓 부분
    private bool r = false; //티켓 움직이기
    public Text yy, mm, dd, tt, m;  //날짜
    public GameObject gameC = null; //게임 컨트롤러

    Animator animator;  //티켓 움직임

    // Start is called before the first frame update
    void Start()
    {
        //시작시 지금 날짜와 시간을 불러옴
        yy.text = DateTime.Now.ToString("yyyy");
        mm.text = DateTime.Now.ToString("MM");
        dd.text = DateTime.Now.ToString("dd");
        tt.text = DateTime.Now.ToString("hh");
        m.text = DateTime.Now.ToString("mm");

        rotate = GameObject.Find("TicketRight");

        animator = GetComponent<Animator>();    //티켓 애니메이션 정의
        gameC = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        if (r)  //티켓이 클릭 되었을 때
        {
            animator.SetBool("go", true);   //애니메이션 실행
            
            Invoke("noAction", 2f); //2초 후 메인화면 시작
        }
    }

    public void Rotate()    //티켓 움직이기
    {
        soundEvent.effect.sound1(); //소리
        r = true;   //티켓 입장 시작
    }

    void noAction() //메인 화면으로 전환
    {
        gameC.GetComponent<StartMain>().MainStart();
    }



}
