using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonAudio : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] sounds;

    private SimonGame simonGame;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        simonGame = transform.parent.GetComponent<SimonGame>();

        if (simonGame != null)
        {
            simonGame.OnGameStarted += PlayStartGameSound;
            simonGame.IsButtonPressed += PlaySound;
            simonGame.SequenceWin += PlayWinSequenceSound;
            simonGame.OnGameLost += PlayGameLostSound;
        }
        else
        {
            Debug.LogError("SimonGame n'a pas été trouvé ou n'a pas été correctement assigné.");
        }
    }
    private void PlaySound(ButtonType buttonType)
    {
        audioSource.clip = sounds[(int)buttonType];
        audioSource.Play();
    }
    private void PlayStartGameSound()
    {
        audioSource.clip = sounds[0];
        audioSource.Play();
    }
    private void PlayWinSequenceSound()
    {
        audioSource.clip = sounds[5];
        audioSource.Play();
    }
    private void PlayGameLostSound()
    {
        audioSource.clip = sounds[5];
        audioSource.Play();
    }
}
