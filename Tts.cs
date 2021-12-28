using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tts : MonoBehaviour    //��ǰ ���� ���� �о��ִ� tts ����
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
        if (audioSource.clip == clip)   //���� ���� audioCilp�� ������ �ٱ�
            audioSource.Stop(); //������ΰ� �ߴ�

        audioSource.clip = clip;    //���� �ֱ�
        audioSource.volume = volume;    //�Ҹ� ũ�� ����

        audioSource.Play(); //���
    }
}
