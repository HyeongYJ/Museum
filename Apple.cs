using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour  //사과와 바닦이 충돌 시 이벤트 처리
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("2roomFloor"))  //벽과 충돌시
        {
            soundEvent.effect.sound4(); //사운드
            Destroy(gameObject, 3f);    //3초 후 사과 삭제
        }
    }
}
