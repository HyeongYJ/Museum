using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1OX : MonoBehaviour    // 눈 앞의 UI 관리
{
    public GameObject success = null;   //성공
    public GameObject room1OX = null;   //Ox 관리
    public GameObject room1O = null;    //O
    public GameObject room1X = null;    //X
    // Start is called before the first frame update
    void Start()
    {
        room1OX = transform.Find("GoghCanvas").gameObject;  //UI 총 관리
        room1O = room1OX.transform.Find("O").gameObject;
        room1X = room1OX.transform.Find("X").gameObject;
        success = room1OX.transform.Find("Success").gameObject;
        success.SetActive(false);   //비활성화
        room1OX.SetActive(false);   //비활성화
        room1O.SetActive(false);    //비활성화
        room1X.SetActive(false);    //비활성화
    }
    public void OnSuccess() //활성화 비활성화 관리
    {
        success.SetActive(!success.activeSelf);
    }
    public void GoOneUI()   //활성화
    {
        room1OX.SetActive(true);
    }
    public void OutOneUI()  //비활성화
    {
        room1OX.SetActive(false);
    }
    public void OneYesUI()  //활성화 비활성화 관리
    {
        room1O.SetActive(!room1O.activeSelf);
        if (room1O) //활성화 된다면
        {
            soundEvent.effect.sound7(); //사운드
        }
    }
    public void OneNoUI()   //활성화 비활성화 관리
    {
        room1X.SetActive(!room1X.activeSelf);
        if (room1X) //활성화 된다면
        {
            soundEvent.effect.sound8(); //사운드
        }
    }
}
