using UnityEngine;

public class ScoreZoneCollisionHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem _confetti;
    [SerializeField] private PaperTossGame _paperTossGame;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private AudioSource _audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ScoreObject"))
        {
            _confetti.Play();
            _audioSource.PlayOneShot(_audioClip);
            _paperTossGame.IncrementScore();
            Destroy(other.gameObject);
        }
    }
}
