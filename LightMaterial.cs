using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.Audio;

public class LightMaterial : MonoBehaviour  //��帮�ȹ� ���� ���͸��� �ٲٱ�
{
    public AudioClip piano; //����
    public AudioSource audioSource = null;

    public SteamVR_Input_Sources leftHand = SteamVR_Input_Sources.LeftHand; //�޼�
    public SteamVR_Input_Sources rightHand = SteamVR_Input_Sources.RightHand;   //������
    public SteamVR_Action_Boolean grabAction = SteamVR_Actions.default_GrabPinch;   //��ư ������


    public Material[] materials;    //���͸��� ����
    private Renderer rend;  //�� ���̱�
    public Animator buttonMove; //�ִϸ��̼�
    public int num = 0; //�ڽ��� ���� ���ϱ�
    public bool isa = false;    //���� �Է�

    public static string a = null;  //����

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        buttonMove = GameObject.Find("3rdRoomObjectButton").GetComponent<Animator>();
        audioSource = GameObject.FindGameObjectWithTag("3roomButtons").GetComponent<AudioSource>();
        rend.enabled = true;
        rend.sharedMaterial = materials[0]; //ó������ ������ �ʴ� ���͸���
    }
    
    //player ���� ������
    private void OnCollisionStay(Collision collision)
    {
        if (ChangeM.gaming) //���� ���� ��
        {
            if (grabAction.GetStateDown(leftHand) || (grabAction.GetStateDown(rightHand)))  //�׸� �׼� ������ ��
            {
                if (collision.gameObject.tag == "hand")
                {
                    //��Ƽ���� �ٲ��
                    rend.sharedMaterial = materials[1];
                    
                    //�ִϸ��̼�
                    buttonMove.SetInteger("moveButton", num);
                    isa = true;
                }
            }
            if (grabAction.GetStateUp(leftHand) || (grabAction.GetStateUp(rightHand)))  //�׸� �׼� ������ ��
            {
                if (isa)    //���� �强 �ѹ��� �ϱ� ����
                {
                    audioSource.clip = piano;   //�ش� �ǾƳ� �뷡 ����
                    audioSource.Play(); //�ش� �ǾƳ� ���� �÷���
                    a += num.ToString();    //���� �Է�
                    isa = false;    //���� �ۼ������� ���� �ۼ� ���ϰ� ����
                }
            }
        }
     }
    
    //�� �ѱ�
    public void LightChange()
    {
        audioSource.clip = piano;   //�ڽ��� �ǾƳ� ���� ����
        audioSource.Play(); //�ڽ��� ���� �÷���
        rend.sharedMaterial = materials[1]; //���͸��� ��ȯ
        buttonMove.SetInteger("moveButton", num);   //�ִϸ��̼�
        StartCoroutine(ResetMaterial(0.8f));    //0.8�� �� �� ����
    }
    //�� ����
   public IEnumerator ResetMaterial(float x)
    {
        yield return new WaitForSeconds(x); //x�� �� ����
        
        rend.sharedMaterial = materials[0]; //���͸��� ��ȯ
        buttonMove.SetInteger("moveButton", 10);     //�ִϸ��̼�
    }

}
