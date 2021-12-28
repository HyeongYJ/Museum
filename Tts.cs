using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tts : MonoBehaviour    //작품 설명 글을 읽어주는 tts 관리
{
    public AudioClip[] tts;
    public AudioSource audioSource = null;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void room11()
    {
        AudioPlay(tts[0]);
    }
    public void room12()
    {
        AudioPlay(tts[1]);
    }
    public void room21()
    {
        AudioPlay(tts[2]);
    }
    public void room22()
    {
        AudioPlay(tts[3]);
    }
    public void room31()
    {
        AudioPlay(tts[4]);
    }
    public void room32()
    {
        AudioPlay(tts[5]);
    }
    public void stopTTS()
    {
        audioSource.Stop();
    }
    public void AudioPlay(AudioClip clip, float volume = 1f)
    {
        if (audioSource.clip == clip)   //전달 받은 audioCilp이 같은지 바교
            audioSource.Stop(); //재생중인거 중단

        audioSource.clip = clip;    //사운드 넣기
        audioSource.volume = volume;    //소리 크기 정의

        audioSource.Play(); //재생
    }
}
