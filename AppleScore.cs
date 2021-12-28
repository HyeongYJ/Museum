using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleScore : MonoBehaviour //사과 점수
{
    public static int score;    //점수
    public int scoreUp; //증가할 점수 -> 책상일 경우 1점 바구니일 경우 2점
    public GameObject targetEffact = null;  //파티클

    private void OnCollisionEnter(Collision collision)
    {
        // 부딪힌 충돌체의 Tag == appler일 때
        if (collision.gameObject.CompareTag("apple") && AppleUI.gameStart)
        {

            score += scoreUp;    //충돌체에 따른 점수 차별 증가
            Destroy(collision.gameObject);  //사과 삭제

            //플레이어 위치에 파티클 생성
            Vector3 success = new Vector3(collision.transform.position.x, collision.transform.position.y, collision.transform.position.z);
            GameObject good = Instantiate(targetEffact, success, Quaternion.Euler(0, 0, 0));
            Destroy(good, 1f);  //1초 뒤 삭제
        }

    }
}
