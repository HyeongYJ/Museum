using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class soundEvent : MonoBehaviour //이펙트 사운드 관리
{
    public AudioClip[] clips;
    public AudioSource audioSource = null;
    public bool SoundEffect = false;
    public static soundEvent effect = null;

    public void Awake()
    {
        effect = this;  //인스턴스화 함
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //방이동시 나오는 소리O
    public void sound0()
    {
        AudioPlay(clips[0]);
    }
    //티켓0
    public void sound1()
    {
        AudioPlay(clips[1]);
    }
    //클릭0
    public void sound2()
    {
        AudioPlay(clips[2]);
    }
    //사과 뽕0
    public void sound3()
    {
        AudioPlay(clips[3]);
    }
    //사과 넣어서 카운트 올라갈 때
    public void sound4()
    {
        AudioPlay(clips[4]);
    }
    //레디
    public void sound5()
    {
        AudioPlay(clips[5]);
    }
    //스타트
    public void sound6()
    {
        AudioPlay(clips[6]);
    }
    //고흐방 정답
    public void sound7()
    {
        AudioPlay(clips[7]);
    }
    //고흐방 오답
    public void sound8()
    {
        AudioPlay(clips[8]);
    }
    //물건 들고 놓고
    public void sound9()
    {
        AudioPlay(clips[9]);
    }
    //물건 들고 놓고
    public void sound10()
    {
        AudioPlay(clips[10]);
    }
    //세잔 시간 초 10초
    public void sound11()
    {
        AudioPlay(clips[11]);
    }
    //세잔 통과 / 몬드리안 통과
    public void sound12()
    {
        AudioPlay(clips[12]);
    }
    //몬드리안 버튼 클릭
    public void sound13()
    {
        AudioPlay(clips[13]);
    }
    //폭죽소리대신
    public void sound14()
    {
        AudioPlay(clips[14]);
    }
    //모든 클리어
    public void sound15()
    {
        AudioPlay(clips[15]);
    }
    public void AudioPlay(AudioClip clip, float volume = 1f)
    {
        if (audioSource.clip == clip)   //전달 받은 audioCilp이 같은지 바교
            audioSource.Stop(); //재생중인거 중단

        audioSource.clip = clip;    //사운드 넣기
        audioSource.volume = volume;     //소리 크기 정의

        audioSource.Play(); //재생
    }
}
