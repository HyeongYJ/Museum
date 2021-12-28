using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubRoomCreat : MonoBehaviour   //현재 사용 되고 있지 않는 소스 코드이며 이는 앞으로 게임 개발이 진행 되어 
                                            //게임이 확장 되었을 때 필요한 관리를 씬 이동으로 하여 작성 해보았음
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
