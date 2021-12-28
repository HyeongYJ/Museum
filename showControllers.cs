using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.SceneManagement;

public class showControllers : MonoBehaviour    //��Ʈ�ѷ� ���̴� ���� ����
{
    public static bool showController = true;   //��Ʈ�ѷ� ���̰� �ϱ�
    private static showControllers playerOne;   //�÷��̾� �ϳ��� �����

    private void Awake()    //�̱���
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
        foreach (var hand in Player.instance.hands) //�� �ν��Ͻ� ����
        {
            if (showController) //��Ʈ�ѷ� ���̱�
            {
                hand.Show();
                hand.ShowController();
                hand.SetSkeletonRangeOfMotion(Valve.VR.EVRSkeletalMotionRange.WithController);
            }
            else
            {   //��Ʈ�ѷ� ������ �ʰ� �ϱ�
                hand.HideController();
                hand.SetSkeletonRangeOfMotion(Valve.VR.EVRSkeletalMotionRange.WithoutController);
            }
        }
    }
}
