using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CreatApple : MonoBehaviour //사과 생성
{

    public GameObject[] apple;  //랜덤 사과 담기
    private Transform newapple = null;  //사과 위치

    public string a = null; //사과
    // Start is called before the first frame update
    void Start()
    {
        newapple = GetComponent<Transform>();   //나자신 위치에

    }

    public void AppleC()
    {
        if (a != "apple")   //사과가 생성 되어 있지 않다면=사과와 충돌 중이지 않다면
        {
            int num = Random.Range(0, apple.Length);    //사과 랜덤 선택
            soundEvent.effect.sound3(); //사운드
            GameObject Apple = Instantiate(apple[num], newapple.position, Quaternion.Euler(0, -90f, 0));    //생성하기
        }
    }

    private void OnTriggerStay(Collider other)  //충돌 중인 물체가
    {
        if(other.gameObject.tag == "apple") //사과라면
        {
            a = other.gameObject.tag.ToString();    //a에 사과 태그 문자 넣기
        }
    }

    private void OnTriggerExit(Collider other)  //충돌이 멈춘 물체가
    {
        if(other.gameObject.tag == "apple") //사과라면
        {
            a = null;   //a에 사과 태그 문자 삭제
        }
    }

}
