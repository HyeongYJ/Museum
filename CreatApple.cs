using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CreatApple : MonoBehaviour //��� ����
{

    public GameObject[] apple;  //���� ��� ���
    private Transform newapple = null;  //��� ��ġ

    public string a = null; //���
    // Start is called before the first frame update
    void Start()
    {
        newapple = GetComponent<Transform>();   //���ڽ� ��ġ��

    }

    public void AppleC()
    {
        if (a != "apple")   //����� ���� �Ǿ� ���� �ʴٸ�=����� �浹 ������ �ʴٸ�
        {
            int num = Random.Range(0, apple.Length);    //��� ���� ����
            soundEvent.effect.sound3(); //����
            GameObject Apple = Instantiate(apple[num], newapple.position, Quaternion.Euler(0, -90f, 0));    //�����ϱ�
        }
    }

    private void OnTriggerStay(Collider other)  //�浹 ���� ��ü��
    {
        if(other.gameObject.tag == "apple") //������
        {
            a = other.gameObject.tag.ToString();    //a�� ��� �±� ���� �ֱ�
        }
    }

    private void OnTriggerExit(Collider other)  //�浹�� ���� ��ü��
    {
        if(other.gameObject.tag == "apple") //������
        {
            a = null;   //a�� ��� �±� ���� ����
        }
    }

}
