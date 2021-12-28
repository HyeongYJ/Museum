using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleScore : MonoBehaviour //��� ����
{
    public static int score;    //����
    public int scoreUp; //������ ���� -> å���� ��� 1�� �ٱ����� ��� 2��
    public GameObject targetEffact = null;  //��ƼŬ

    private void OnCollisionEnter(Collision collision)
    {
        // �ε��� �浹ü�� Tag == appler�� ��
        if (collision.gameObject.CompareTag("apple") && AppleUI.gameStart)
        {

            score += scoreUp;    //�浹ü�� ���� ���� ���� ����
            Destroy(collision.gameObject);  //��� ����

            //�÷��̾� ��ġ�� ��ƼŬ ����
            Vector3 success = new Vector3(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z);
            GameObject good = Instantiate(targetEffact, success, Quaternion.Euler(0, 0, 0));
            Destroy(good, 1f);  //1�� �� ����
        }

    }
}
