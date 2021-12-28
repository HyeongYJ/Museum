using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DateScript : MonoBehaviour //���� �� Ƽ�� ��¥ �ҷ����� �� ��Ʈ�� ����
{
    public GameObject rotate = null;    //������ Ƽ�� �κ�
    private bool r = false; //Ƽ�� �����̱�
    public Text yy, mm, dd, tt, m;  //��¥
    public GameObject gameC = null; //���� ��Ʈ�ѷ�

    Animator animator;  //Ƽ�� ������

    // Start is called before the first frame update
    void Start()
    {
        //���۽� ���� ��¥�� �ð��� �ҷ���
        yy.text = DateTime.Now.ToString("yyyy");
        mm.text = DateTime.Now.ToString("MM");
        dd.text = DateTime.Now.ToString("dd");
        tt.text = DateTime.Now.ToString("hh");
        m.text = DateTime.Now.ToString("mm");

        rotate = GameObject.Find("TicketRight");

        animator = GetComponent<Animator>();    //Ƽ�� �ִϸ��̼� ����
        gameC = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        if (r)  //Ƽ���� Ŭ�� �Ǿ��� ��
        {
            animator.SetBool("go", true);   //�ִϸ��̼� ����
            
            Invoke("noAction", 2f); //2�� �� ����ȭ�� ����
        }
    }

    public void Rotate()    //Ƽ�� �����̱�
    {
        soundEvent.effect.sound1(); //�Ҹ�
        r = true;   //Ƽ�� ���� ����
    }

    void noAction() //���� ȭ������ ��ȯ
    {
        gameC.GetComponent<StartMain>().MainStart();
    }



}
