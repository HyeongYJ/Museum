using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHint : MonoBehaviour   //고흐방에 힌트 관리
{
    public float hintTime = 0;      //3초 동안 볼수 있음
    public int hintNum = 3;         //3번의 기휘
    public bool hint = false;       //클리함
    public GameObject img = null;   //힌트 이미지
    public Text num = null;         //힌트 text

    void Start()
    {
        img = transform.Find("hintImage").gameObject;
        num = transform.Find("num").gameObject.GetComponent<Text>();
    }

    void Update()
    {
        if (hint)   //클리 했다면
        {
            hintTime += Time.deltaTime; //시간초 세리기
            img.SetActive(true);    //이미지 보여주기

            //3초 지나면
            if (hintTime > 3f)
            {
                hintNum--;  //힌트 갯수 줄어듬
                img.SetActive(false);   //이미지 비활성화
                hint = false;   //힌트 끝
                hintTime = 0;   //힌트 시간 0초 초기화
                num.text = "기회 "+hintNum+"번";   //힌트 기회 재정의
            }
        }
    }

    //힌트 사용 시
    public void Hint()
    {
        if (hintNum > 0)    //힌트 갯수가 남아 있다며ㅕㄴ
        {
            hint = true;    //힌트 보여주기
        }
        
    }
}
