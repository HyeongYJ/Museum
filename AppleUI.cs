using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;
using Valve.VR.Extras;


public class AppleUI : MonoBehaviour    //��� UI ���� �� ���� ���� �ڵ�
{
    public AudioClip timerSound;    //���� �ð� �뷡
    public AudioSource audioSource = null;
    private bool soundS = false;    //���� ����

    public SteamVR_Input_Sources leftHand = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Input_Sources rightHand = SteamVR_Input_Sources.RightHand;
    public SteamVR_Action_Boolean trigger = SteamVR_Actions.default_GrabGrip;

    //Apple Ui ķ���� �ȿ� ������Ʈ�� ������
    private float countDown; //ī��� �ð� ����
    private bool goCount = false;   //ī��� �ٿ� OnOff
    private int countUiNum = 0;     //�ð��� �ٸ� ī��Ʈ UI

    public static bool gameStart = false;  //���� ����
    private int level = 0;      //���� ����

    //ī��Ʈ UI��
    public GameObject readyUI = null;
    public GameObject startUI = null;
    public GameObject successUI = null;
    public GameObject roundUI = null;
    public GameObject failUI = null;
    public GameObject retryUI = null;

    //UI �ؽ�Ʈ��
    public Text score, cutline, times, round;   

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //UI�� ��Ȱ��ȭ
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
        startCount();   //�ð� ����
        PlayGame(); //�ð� ����
        ScoreUI();  //���� UI
    }

    public void RetryRoom2()    //�ٽ� ���� ��
    {
        level = 0;  //���� �ʱ�ȭ
        cutline.text = "3"; //�ؽ�Ʈ �ʱ�ȭ
        round.text = "1 Round"; //�ؽ�Ʈ �ʱ�ȭ
        goCount = false;    //�ð��� ���� ��Ʈ��
        AppleScore.score = 0;   //���� �ʱ�ȭ
        countUiNum = 0; //UI ���� �ʱ�ȭ
        countDown = 0;  //�ð� �ʱ�ȭ
    }

    public void startButton()  //ī��� ����
    {
        retryUI.SetActive(false);   //�ٽ� ���� ��ư ��Ȱ��ȭ
        goCount = true; //ī��Ʈ Ȱ��ȭ
        readyUI.SetActive(true);    //ready
        soundEvent.effect.sound5(); //����
    }

    void startCount()   //���� �ð���
    {
        if (goCount)    //ī��Ʈ ����
        {
            countDown += Time.deltaTime;    //�� ������
            if (countDown > 1f) //1�� ������
            {
                countDown = 0;  //�ʱ�ȭ
                UiChan();   //UI Ȱ��ȭ �ϴ� �Լ�
                
            }
        }
    }

    
    private void UiChan()   //UI 3,2,1�� GameTime ����
    {
        
        countUiNum ++;  //UI ���� ī��Ʈ ����
        switch (countUiNum)
        {
            case 1:
                break;
            case 2:
                readyUI.SetActive(false);   //read UI ��Ȱ��ȭ
                startUI.SetActive(true);    //start UI Ȱ��ȭ 
                break;
            default:
                startUI.SetActive(false);   //start UI ��Ȱ��ȭ
                countDown = 10; //10�� �ð�����
                countUiNum = 0; //UI ���� ���� �ʱ�ȭ
                gameStart = true;   //���� ����
                soundS = true;  //�Ҹ� On
                goCount = false;    //�� ������ �߱�
                break;
        }
    }

    private void PlayGame()     //���� ���� ��
    {
        if (gameStart)  //���� ���۽�
        {
            if (soundS) //���� ȣ��
            {
                audioSource.PlayOneShot(timerSound);    //�ȵ��ȵ�
                soundS = false; //���� ȣ�� ��
            }
            countDown -= Time.deltaTime;    //�ð� ����
            times.text = Mathf.Round(countDown).ToString(); //�ð� �� �����ֱ�
            if (countDown <= 0) //���� ����
            {
                gameStop(); //���� ����
            }            
        }
    }



    private void gameStop()     //���� ����
    {
        gameStart = false;  //������ ����
        countDown = 0; //Ÿ�̸� ����
        level++;    //���̵� ����

        switch (level)
        {
            case 1: //���� 1�� ���
                if (AppleScore.score >= 3)  //3�� �̻��� ��� ����
                {
                    onOffUI();//���� ���� UI
                    soundEvent.effect.sound12();    //�Ҹ�
                    Invoke("onOffUI", 2f);  //2�� �� UI �����
                    round.text = "2 Round"; //2����� �ؽ�Ʈ �ٲ�
                    cutline.text = "9"; //���� ���� 9�� ����
                    Invoke("startButton", 3f);  //3�� �� ���� ���� ����
                }
                else
                {
                    //���� UI
                    failUI.SetActive(true); //���� UI
                    soundEvent.effect.sound6(); //�Ҹ�
                    Invoke("fail", 2f); // UI ��Ȱ��ȭ
                    round.text = "1 Round"; //1���� �ʱ�ȭ
                    cutline.text = "3"; //���� ���� ���� 3��
                    retryUI.SetActive(true);    //�ٽ� ���� ��ư
                    StartMain.instance.yesPointer();    //����Ʈ ���̰� �ϱ�
                    AppleScore.score = 0;   //���� �ʱ�ȭ
                    level = 0;  //���� 0���� �ʱ�ȭ
                }
                break;
            case 2: //���� 2�� ���
                if (AppleScore.score >= 9)  //9�� �̻��� ��� ����
                {
                    onOffUI();//���� UI
                    soundEvent.effect.sound12();    //�Ҹ�
                    Invoke("onOffUI", 2f);  //2�� �� UI �����
                    round.text = "3 Round"; //3����� �ؽ�Ʈ �ٲ�
                    cutline.text = "18"; //���� ���� 18�� ����
                    Invoke("startButton", 3f);  //3�� �� ���� ���� ����
                }
                else
                {
                    //���� UI
                    failUI.SetActive(true); //���� UI
                    soundEvent.effect.sound6(); //�Ҹ�
                    Invoke("fail", 2f); //UI ��Ȱ��ȭ
                    retryUI.SetActive(true);    //�ٽ� ���� ��ư
                    round.text = "1 Round"; //1���� �ʱ�ȭ
                    cutline.text = "3"; //���� ���� ���� 3�� �ʱ�ȭ
                    StartMain.instance.yesPointer();    //����Ʈ ���̰� �ϱ�
                    AppleScore.score = 0;   //���� �ʱ�ȭ
                    level = 0;  //���� 0���� �ʱ�ȭ
                }
                break;
            case 3: //���� 3�� ���
                if (AppleScore.score >= 18) //18�� �̻��� ��� ����
                {
                    Clear();    //���� UI
                    soundEvent.effect.sound15();   //�Ҹ� 
                    Invoke("Clear", 5f);    //Ŭ���� UI ��Ȱ��ȭ
                    cutline.text = "3"; //���� �ʱ�ȭ
                    AppleScore.score = 0;   //���� �ʱ�ȭ
                    level = 0;  //���� 0���� �ʱ�ȭ
                }
                else
                {
                    failUI.SetActive(true); //���� UI
                    soundEvent.effect.sound6(); //�Ҹ�
                    round.text = "1 Round"; //1����� �ʱ�ȭ
                    cutline.text = "3"; //3������ �ʱ�ȭ
                    Invoke("fail", 2f); //���� UI ��Ȱ��ȭ
                    retryUI.SetActive(true);    //�ٽ��ϱ� ��ư Ȱ��ȭ
                    StartMain.instance.yesPointer();    //����Ʈ ���̰� �ϱ�
                    AppleScore.score = 0;   //���� �ʱ�ȭ
                    level = 0;  //���� �ʱ�ȭ
                }
                break;
        }
    }
    void onOffUI()  //���� ���� UI
    {
        roundUI.SetActive(!roundUI.activeSelf); //Ȱ��ȭ ��Ȱ��ȭ ����
    }

    private void fail()
    {
        failUI.SetActive(false);    //���� UI ��Ȱ��ȭ
    }

    void Clear()    //���� UI
    {
        successUI.SetActive(!successUI.activeSelf); //���� Ȱ��ȭ ��Ȱ��ȭ ����

    }

    private void ScoreUI()  //Text ǥ��
    {
        score.text = AppleScore.score.ToString();   //���ڷ� ǥ��
    }

}
