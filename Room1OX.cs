using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1OX : MonoBehaviour    // �� ���� UI ����
{
    public GameObject success = null;   //����
    public GameObject room1OX = null;   //Ox ����
    public GameObject room1O = null;    //O
    public GameObject room1X = null;    //X
    // Start is called before the first frame update
    void Start()
    {
        room1OX = transform.Find("GoghCanvas").gameObject;  //UI �� ����
        room1O = room1OX.transform.Find("O").gameObject;
        room1X = room1OX.transform.Find("X").gameObject;
        success = room1OX.transform.Find("Success").gameObject;
        success.SetActive(false);   //��Ȱ��ȭ
        room1OX.SetActive(false);   //��Ȱ��ȭ
        room1O.SetActive(false);    //��Ȱ��ȭ
        room1X.SetActive(false);    //��Ȱ��ȭ
    }
    public void OnSuccess() //Ȱ��ȭ ��Ȱ��ȭ ����
    {
        success.SetActive(!success.activeSelf);
    }
    public void GoOneUI()   //Ȱ��ȭ
    {
        room1OX.SetActive(true);
    }
    public void OutOneUI()  //��Ȱ��ȭ
    {
        room1OX.SetActive(false);
    }
    public void OneYesUI()  //Ȱ��ȭ ��Ȱ��ȭ ����
    {
        room1O.SetActive(!room1O.activeSelf);
        if (room1O) //Ȱ��ȭ �ȴٸ�
        {
            soundEvent.effect.sound7(); //����
        }
    }
    public void OneNoUI()   //Ȱ��ȭ ��Ȱ��ȭ ����
    {
        room1X.SetActive(!room1X.activeSelf);
        if (room1X) //Ȱ��ȭ �ȴٸ�
        {
            soundEvent.effect.sound8(); //����
        }
    }
}
