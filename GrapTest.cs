using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GrapTest : MonoBehaviour   //����濡�� ��ü ��� ���� �÷��� ���� �ڵ�
{
    public SteamVR_Input_Sources leftHand = SteamVR_Input_Sources.LeftHand;  //�޼�
    public SteamVR_Input_Sources rightHand = SteamVR_Input_Sources.RightHand;   //������
    public SteamVR_Behaviour_Pose controllerPose;   //��Ʈ�ѷ� ����
    public SteamVR_Action_Boolean grabAction = SteamVR_Actions.default_GrabPinch;   //�׷� �׼�
    public SteamVR_Action_Boolean resultAction = SteamVR_Actions.default_Teleport;  //���� ǥ�� �׼�

    private GameObject collidingObject;     //���� �浹���� ��ü
    private GameObject objectInHand;    //�÷��̾ ���� ��ü

    public static bool showHand = true; //�� ��Ʈ�Ѱ� �����ֱ�
    private int oneRoomQ = 0;   //���� ���� ����

    private bool answer = false;    //���� Ȯ�� ������ Ÿ�̹�

    public GameObject mainCamera = null;    //ī�޶� ã��

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");    //���� ī�޶� ã��
        
    }

    // Start is called before the first frame update
    private void Update()
    {
        //��ü���
        if (GetGrab() || GetGrab2())
        {
            if (collidingObject)
            {
                GrabObject();
            }
        }
        //��ü ����
        if (GetGrabUp() || GetGrabUp2())
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }

        //�� �����ְ� �������
        foreach (var hand in Player.instance.hands)
        {
            if (showHand)//�� �����ֱ�
            {
                hand.Show();
                hand.SetSkeletonRangeOfMotion(Valve.VR.EVRSkeletalMotionRange.WithController);  
            }
            else
            {   //�� �����
                hand.Hide();
                hand.SetSkeletonRangeOfMotion(Valve.VR.EVRSkeletalMotionRange.WithoutController);
            }
        }

        //���� ���߱�
        if (answer)
        {
            if (resultAction.GetStateDown(leftHand) || resultAction.GetStateDown(rightHand))
            {
                if (objectInHand.CompareTag("OneRoomYes"))      //�����̶��
                {
                    oneRoomQ++; //���� ���� ����
                    //����
                    if (oneRoomQ < 3)   //������ ��� ã�� ������ ��
                    {
                        OUI();  //���� UI 
                        soundEvent.effect.sound7(); //����
                        Invoke("OUI", 1f);  //���� Ui ��Ȱ��ȭ
                    }
                    
                    Destroy(objectInHand);  //�� �ȿ� �ִ� ��ü ����
                    ReleaseObject();    //������

                    if (oneRoomQ == 3)  //���� ������ 3����� ����
                    {
                        SuccessUI();    //���� UI
                        soundEvent.effect.sound15();    //����  
                        Invoke("SuccessUI", 3f);    //UI ��Ȱ��ȭ
                    }
                }
                else if (!objectInHand.CompareTag("OneRoomYes"))     //������ �ƴ϶��
                {
                    XUI();  //���� �ƴ� UI
                    soundEvent.effect.sound8(); //����
                    Invoke("XUI", 1f);  //UI ��Ȱ��ȭ
                }
            }
        }
        
    }

    void OUI()
    {
        mainCamera.GetComponent<Room1OX>().OneYesUI();
    }
    void XUI()
    {
        mainCamera.GetComponent<Room1OX>().OneNoUI();
    }
    void SuccessUI()
    {
        mainCamera.GetComponent<Room1OX>().OnSuccess();
    }
    public bool GetGrab()
    {
        return grabAction.GetStateDown(leftHand);
    }

    public bool GetGrab2()
    {
        return grabAction.GetStateDown(rightHand);
    }

    public bool GetGrabUp()
    {
        return grabAction.GetStateUp(leftHand);
    }

    public bool GetGrabUp2()
    {
        return grabAction.GetStateUp(rightHand);
    }

    //�浹�� ���۵Ǵ� ����
    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    //�浹���� �� 
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    //�� ����
    //�浹�� ������ ����
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)   //�浹���� �������� ���ٸ� �Լ� ����
        {
            return;
        }
        
        collidingObject = null; //�浹 ���� ��ü ����
        
    }
    //�浹���� ��ü�� ����
    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>())  //��� �ִ� ���԰� �ְų� �浹ü�� ������ٵ� ������ �ʾҴٸ� ����
        {
            return;
        }
        collidingObject = col.gameObject;   //�浹 ���� ��ü�� ����
        
        
    }

    //��ü�� ����
    private void GrabObject()
    {
        
        objectInHand = collidingObject;  //���� ��ü�� ����
        collidingObject = null; //�浹 ��ü ����

        var joint = AddFixedJoint();    //���� ��Ű��
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();

        //��Ҵ� ��ü�� �����̶��
        answer = true;

    }

    //�տ� ����
     private FixedJoint AddFixedJoint()
     {
        soundEvent.effect.sound9(); //����
        showHand = false;   //�� �����
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();  //FixedJoint =>��ü���� �ϳ��� ���� ����������
        fx.breakForce = 20000;      //breakForce =>����Ʈ�� ���ŵǵ��� �ϱ� ���� �ʿ��� ���� ũ��
        fx.breakTorque = 20000;     //breakTorque =>����Ʈ�� ���ŵǵ��� �ϱ� ���� �ʿ��� ��ũ
        return fx;
     }

    //��ü�� ����
    private void ReleaseObject()
    {
        soundEvent.effect.sound10();    //����
        showHand = true;    //�� ����
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;    //�ڽ� ������Ʈ ���� ����
            Destroy(GetComponent<FixedJoint>());    //�ڽ� ������Ʈ ���� ������Ʈ ����

            //controllerPose.GetVeloecity() => ��Ʈ�ѷ��� �ӵ�
            objectInHand.GetComponent<Rigidbody>().velocity = controllerPose.GetVelocity();
            //controllerPose.GetAngularVelocity() => ��Ʈ�ѷ��� ���ӵ�
            objectInHand.GetComponent<Rigidbody>().angularVelocity = controllerPose.GetAngularVelocity();
        }
        objectInHand = null;    //�տ� �ƹ��͵� ����
        answer = false; //���� ã�� �Ұ���
    }


}
