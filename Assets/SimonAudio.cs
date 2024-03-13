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
            simonGame.IsButtonPressed += PlaySound;
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
}
