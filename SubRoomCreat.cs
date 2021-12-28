using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubRoomCreat : MonoBehaviour   //���� ��� �ǰ� ���� �ʴ� �ҽ� �ڵ��̸� �̴� ������ ���� ������ ���� �Ǿ� 
                                            //������ Ȯ�� �Ǿ��� �� �ʿ��� ������ �� �̵����� �Ͽ� �ۼ� �غ�����
{
    public GameObject gameController = null;
    
    void start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    public void HomeMove1()
    {
        gameController.GetComponent<StartMain>().OneHome();
    }
    public void HomeMove2()
    {
        gameController.GetComponent<StartMain>().TwoHome();
    }
    public void HomeMove3()
    {
        gameController.GetComponent<StartMain>().ThreeHome();
    }
}
