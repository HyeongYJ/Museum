using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ChangeM : MonoBehaviour    //���͸��� �ٲٱ� �� ��帮�� �� ���� ����
{
    public SteamVR_Input_Sources leftHand = SteamVR_Input_Sources.LeftHand; //���� ��
    public SteamVR_Input_Sources rightHand = SteamVR_Input_Sources.RightHand;   //������ ��
    public SteamVR_Action_Boolean grabAction = SteamVR_Actions.default_GrabPinch;   //�� ����

    public bool playTime = false;   //�� ������ ����
    private float time = 0;         //�ð���
    public static bool gaming = false;    //���� ��ư ������ ����
    private bool playing = false;   //���� Ǯ��
    private bool guide = false;     //����
    private int step = 0;   //���� �ܰ�
    public bool stopTimer = false;   //�ݺ��� ����

    private int i = 0;  //for�� ����
    public GameObject[] levelLight; //���� UI ����
    public GameObject fail; //���� UI
    public GameObject success;  //���� UI
    
    public LightMaterial[] pie; //���͸��� �ٲ� ������Ʈ���� �ҽ� ������Ʈ

    // Start is called before the first frame update
    void Start()
    {
        //���͸��� �ٲٱ� ���� ������Ʈ �ҷ�����
        pie = new LightMaterial[8];
        //������
        pie[0] = GameObject.FindGameObjectWithTag("pie1").GetComponent<LightMaterial>();
        pie[1] = GameObject.FindGameObjectWithTag("pie2").GetComponent<LightMaterial>();
        pie[2] = GameObject.FindGameObjectWithTag("pie3").GetComponent<LightMaterial>(); 
        pie[3] = GameObject.FindGameObjectWithTag("pie4").GetComponent<LightMaterial>();
        pie[4] = GameObject.FindGameObjectWithTag("pie5").GetComponent<LightMaterial>();
        pie[5] = GameObject.FindGameObjectWithTag("pie6").GetComponent<LightMaterial>();
        pie[6] = GameObject.FindGameObjectWithTag("pie7").GetComponent<LightMaterial>();
        pie[7] = GameObject.FindGameObjectWithTag("pie8").GetComponent<LightMaterial>();

        //���� UI ������ ��Ȱ��ȭ
        for (int i = 0; i<levelLight.Length; i++)
        {
            levelLight[i].SetActive(false); //���� UI
        }
        fail.SetActive(false);  //���� UI ��Ȱ��ȭ
        success.SetActive(false);   //���� UI ��Ȱ��ȭ
    }

    public void startWhile()    //���� ����
    {
        StartCoroutine(Timer());    //ī��Ʈ �ٿ� ���� ȣ��
    }

    public void restartHome()   //Ȩ ȭ������ ���ư���
    {
        for (int i = 0; i < levelLight.Length; i++) //���� UI ��Ȱ��ȭ
        {
            levelLight[i].SetActive(false);
        }
        for (i = 0; i < pie.Length; i++)    //������ ������ �� ����
        {
            StartCoroutine(pie[i].ResetMaterial(0f));   //���͸��� ��ȯ
        }
        stopTimer = true;   //�ð� ����
        fail.SetActive(false);  //���� Ui ��Ȱ��ȭ
        success.SetActive(false);   //���� UI ��Ȱ��ȭ
        LightMaterial.a = null; //���� �ʱ�ȭ
        step = 0;   //���� �ʱ�ȭ
        time = 0;   //�ð� �ʱ�ȭ
        playTime = false;   //�÷��� ����
        gaming = false; //���� ��ư ����
        guide = false;  //���̵� ����
        playing = false;    //�÷��� ����
    }

    public void PlayTime()  //���� ��ư
    {
        step++; //���� ����
        time = 0;   //�ð� 0��
        playTime = true;    //�÷��� ����
        guide = true;   //���̵� ��ٸ�

    }

    public void RePlay()    //���� �ٽ� ����
    {
        step = 0;   //���� �ʱ�ȭ
        PlayTime(); //�ٽ� ���� ����
    }
    

    IEnumerator Timer() //��帮�� �� ���� ���� ����
    {
        while (true)
        {
            if (playTime)   //���� ���� ����
            {
                if (guide)  //�ȳ� ���� �ð�
                {
                    //3�� �� ��ư ���̵� ����
                    if (time == 3)     //1�ܰ�
                    {
                        //ù��° �� ������
                        pie[6].LightChange();
                    }
                    else if (time == 4)
                    {
                        //�ι�° �� ����
                        pie[1].LightChange();
                    }
                    else if (time == 5)
                    {
                        //����° �� �ѽ�
                        pie[0].LightChange();
                        if (step == 1)  //���� 1�̶��
                        {
                            time = 0;   //�ð� �ʱ�ȭ
                            guide = false;  //���̵� ��
                            playing = true; //�÷��� ����
                        }
                    }
                    else if (time == 6)          //2�ܰ�
                    {
                        //�׹�° �� ����
                        pie[3].LightChange();
                        if (step == 2)  //���� 2���
                        {
                            time = 0;   //�ð� �ʱ�ȭ
                            guide = false;  //���̵� ��
                            playing = true; //�÷��� ����
                        }
                    }
                    else if (time == 7)      //3�ܰ�
                    {
                        //�ټ���° �� ����
                        pie[7].LightChange();   
                        if (step == 3)  //���� 3�̶��
                        {
                            time = 0;   //�ð� �ʱ�ȭ
                            guide = false;  //���̵� ��
                            playing = true; //�÷��� ����
                        }
                    }
                    else if (time == 8)      //4�ܰ�
                    {
                        //6��° �� ����
                        pie[2].LightChange();
                        if (step == 4)  //���� 4���
                        {
                            time = 0;   //�ð� �ʱ�ȭ
                            guide = false;  //���̵� ��
                            playing = true; //�÷��� ����
                        }
                    }
                    else if (time == 9)      //5�ܰ�
                    {
                        //7��° �� ����
                        pie[5].LightChange();
                        if (step == 5)  //���� 5���
                        {
                            time = 0;   //�ð� �ʱ�ȭ
                            guide = false;  //���̵� ��
                            playing = true; //�÷��� ����
                        }
                    }
                    else if (time == 10)      //6�ܰ�
                    {
                        //8��° �� ����
                        pie[4].LightChange();
                        if (step == 6)
                        {
                            time = 0;   //�ð� �ʱ�ȭ
                            guide = false;  //���̵� ��
                            playing = true; //�÷��� ����
                        }
                    }
                }

                if (playing)    //�÷��̾� ���� ����
                {
                    if (time == 1)  //1�� �� �ְ� ����
                    {
                        soundEvent.effect.sound12();    //�Ҹ�
                        gaming = true;  //��ư ������ ����
                    }
                    //��ư ������
                    else if (time == 11)    //11�� �Ǹ� ��ư ������ ����
                    {
                        time = 0;   //�ð� �ʱ�ȭ
                        gaming = false; //��ư ������ �Ұ���
                        //���͸��� �ʱ�ȭ
                        for (i = 0; i < pie.Length; i++)
                        {
                            StartCoroutine(pie[i].ResetMaterial(0f));   //���͸��� ��ȯ
                        }
                        
                        playing = false;    //�÷��� ����
                        playTime = false;   //�ð� ����
                        Answer(); //���� Ȯ��
                    }
                }

                time += 1;  //�ð� 1�ʾ� ����
                yield return new WaitForSeconds(1f);    //1�ʿ� �ѹ� ȣ��
                
            }
            else
            {
                yield return null;
            }
            if (stopTimer)  //������
            {
                stopTimer = false;  //ȣ�� ������ ��Ȱ��ȭ
                yield break;    //ȣ�� ������
            }
        }
    }

    //��ư �ȳ� �� �������
    IEnumerator ReMaterialCo(int a, float t)
    {
        yield return new WaitForSeconds(t); //t�� �Ŀ� ����
        pie[a].ResetMaterial(0f);   //������ ��ư �������
    }

    private void Answer()   //���� Ȯ��
    {
        if(step == 1)   //1�ܰ��϶�     ====================== �Ʒ� �ҽ��ڵ�� ���� �� (6�ܰ踸 ���� �ٸ�) ======================
        {
            if (LightMaterial.a == "610")   //���� ���� ��
            {
                LightMaterial.a = null; //���� �ʱ�ȭ
                soundEvent.effect.sound12(); ;  //����
                PlayTime();     //���� �ð� ����
                levelLight[0].SetActive(true);  //1���� UI Ȱ��ȭ
            }
            else
            {
                step = 0;   //0�ܰ�� �ʱ�ȭ
                LightMaterial.a = null; //���� �ʱ�ȭ
                fail.SetActive(true);   //���� Ui
                soundEvent.effect.sound8(); //����
                //���� UI ��Ȱ��ȭ
                for (int i = 0; i < levelLight.Length; i++)
                {
                    levelLight[i].SetActive(false);
                }
            }
        }
        else if (step == 2)
        {
            if (LightMaterial.a == "6103")
            {
                LightMaterial.a = null;
                soundEvent.effect.sound12(); ;
                PlayTime();
                levelLight[1].SetActive(true);
            }
            else
            {
                step = 0;
                LightMaterial.a = null;
                fail.SetActive(true);
                soundEvent.effect.sound8();
                for (int i = 0; i < levelLight.Length; i++)
                {
                    levelLight[i].SetActive(false);
                }
            }
        }
        else if (step == 3)
        {
            if (LightMaterial.a == "61037")
            {
                LightMaterial.a = null;
                soundEvent.effect.sound12(); ;
                PlayTime();
                levelLight[2].SetActive(true);
            }
            else
            {
                step = 0;
                LightMaterial.a = null;
                fail.SetActive(true);
                soundEvent.effect.sound8();
                for (int i = 0; i < levelLight.Length; i++)
                {
                    levelLight[i].SetActive(false);
                }
            }
        }
        else if (step == 4)
        {
            if (LightMaterial.a == "610372")
            {
                LightMaterial.a = null;
                soundEvent.effect.sound12(); ;
                PlayTime();
                levelLight[3].SetActive(true);
            }
            else
            {
                step = 0;
                LightMaterial.a = null;
                fail.SetActive(true);
                soundEvent.effect.sound8();
                for (int i = 0; i < levelLight.Length; i++)
                {
                    levelLight[i].SetActive(false);
                }
            }
        }
        else if (step == 5)
        {
            if (LightMaterial.a == "6103725")
            {
                LightMaterial.a = null;
                soundEvent.effect.sound12(); ;
                PlayTime();
                levelLight[4].SetActive(true);
            }
            else
            {
                step = 0;
                LightMaterial.a = null;
                fail.SetActive(true);
                soundEvent.effect.sound8();
                for (int i = 0; i < levelLight.Length; i++)
                {
                    levelLight[i].SetActive(false);
                }
            }
        }
        else if (step == 6)
        {
            if (LightMaterial.a == "61037254")
            {
                LightMaterial.a = null;
                soundEvent.effect.sound15(); ;
                levelLight[5].SetActive(true);
                success.SetActive(true);    //���� ����
            }
            else
            {
                step = 0;
                LightMaterial.a = null;
                fail.SetActive(true);
                soundEvent.effect.sound8();
                for (int i = 0; i < levelLight.Length; i++)
                {
                    levelLight[i].SetActive(false);
                }
            }
        }

    }
}
