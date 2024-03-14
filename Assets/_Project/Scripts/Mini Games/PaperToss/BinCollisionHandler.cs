using UnityEngine;

public class ScoreZoneCollisionHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem Confetti;

    private PaperTossGame _paperTossGame;
    private AudioSource _audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ScoreObject"))
        {
            _paperTossGame.IncrementScore();
            Destroy(other.gameObject);
            Confetti.Play();
            _audioSource.Play();
        }
    }
}
