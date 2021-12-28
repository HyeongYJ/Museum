using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.Extras;

public class PointerHandler : MonoBehaviour //����Ʈ Ŭ�� �̺�Ʈ ����
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

    //���� ������Ʈ��
    public GameObject startcontroller = null;
    public GameObject room3Button = null;

    // Start is called before the first frame update
    void Start()
    {
        //UI �ֱ�
        guide = GameObject.FindGameObjectWithTag("guideUI");
        ticket = GameObject.FindGameObjectWithTag("ticketUI");
        rightTicket = GameObject.Find("TicketRight");
        startcontroller = GameObject.FindGameObjectWithTag("GameController");
        //����Ʈ ����
        pointer1 = GetComponent<PointerHandler>();
        pointer2 = GetComponent<SteamVR_LaserPointer>();

        laserPointer.PointerIn += PointerInside;    //����Ʈ ���� ��
        laserPointer.PointerOut += PointerOutside;  //����Ʈ ������ ��
        laserPointer.PointerClick += PointerClick;  //Ŭ������ ��

    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {


    }

    public void PointerClick(object sender, PointerEventArgs e) //Ŭ�� �� �Ʒ� �ҽ� �ڵ� ����
    {
        //main��
        if (e.target.CompareTag("guideUI"))
        {
            guide.SetActive(false);
            ticket.SetActive(true);
        }
        if (e.target.CompareTag("ticketUI"))
        {
            rightTicket.GetComponent<DateScript>().Rotate();    //Ŭ���� ����

        }
        //�����
        if (e.target.CompareTag("1roomExUI"))
        {
            e.target.gameObject.SetActive(false);
            startcontroller.GetComponent<StartMain>().noPointerOneRoom();   //����Ʈ ������� �ϱ�
            soundEvent.effect.sound2();
        }
        //�����
        if (e.target.CompareTag("2roomExUI"))
        {
            e.target.gameObject.SetActive(false);
            startcontroller.GetComponent<StartMain>().OnTwoRoom();  //������ �������
            appleUI.gameObject.GetComponent<AppleUI>().startButton();   //���� ���� �غ�
            GameObject.FindGameObjectWithTag("2roomScoreUI").GetComponent<AppleUI>().startButton(); //���� appleUI ������Ʈ�� �ҽ� �ø� �����
            soundEvent.effect.sound2();
        }
        //��帮�ȹ�
        if (e.target.CompareTag("3roomExUI"))
        {
            e.target.gameObject.SetActive(false);
            startcontroller.GetComponent<StartMain>().noPointerOneRoom();   //������ �������
            room3Button = GameObject.FindGameObjectWithTag("3roomButtons");
            room3Button.GetComponent<ChangeM>().startWhile();   //���� ���� �غ�
            room3Button.GetComponent<ChangeM>().PlayTime(); //���� �غ�
            soundEvent.effect.sound2();
        }
        

    }
}
