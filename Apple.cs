using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour  //����� �ٴ��� �浹 �� �̺�Ʈ ó��
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("2roomFloor"))  //���� �浹��
        {
            soundEvent.effect.sound4(); //����
            Destroy(gameObject, 3f);    //3�� �� ��� ����
        }
    }
}
