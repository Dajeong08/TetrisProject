using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SoundClip
{
    Drop,
    Explo,
    GameStart,
    GameOver,
    Touch
}
public class SoundController : MonoBehaviour
{
    public AudioClip A_Drop, A_Explo, A_Touch, A_GameStart, A_GameOver;
    public AudioSource myAudio;

    public void PlaySound(SoundClip audioType)
    {
        switch (audioType)
        {
            case SoundClip.Drop:
                myAudio.clip = A_Drop;
                break;
            case SoundClip.Explo:
                myAudio.clip = A_Explo;
                break;
            case SoundClip.Touch:
                myAudio.clip = A_Touch;
                break;
            case SoundClip.GameStart:
                myAudio.clip = A_GameStart;
                break;
            case SoundClip.GameOver:
                myAudio.clip = A_GameOver;
                break;
        }

        myAudio.Play();
    }
}