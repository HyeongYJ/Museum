using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.SceneManagement;

public class showControllers : MonoBehaviour    //콘트롤러 보이는 유무 관리
{
    public static bool showController = true;   //컨트롤러 보이게 하기
    private static showControllers playerOne;   //플레이어 하나만 만들기

    private void Awake()    //싱글턴
    {
        if (playerOne == null)
        {
            playerOne = this;
        }
        else if (playerOne != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var hand in Player.instance.hands) //손 인스턴스 관리
        {
            if (showController) //컨트롤러 보이기
            {
                hand.Show();
                hand.ShowController();
                hand.SetSkeletonRangeOfMotion(Valve.VR.EVRSkeletalMotionRange.WithController);
            }
            else
            {   //컨트롤러 보이지 않게 하기
                hand.HideController();
                hand.SetSkeletonRangeOfMotion(Valve.VR.EVRSkeletalMotionRange.WithoutController);
            }
        }
    }
}
