using UnityEngine;

public class ScoreZoneCollisionHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem Confetti;
    [SerializeField] private PaperTossGame _paperTossGame;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ScoreObject"))
        {
            Confetti.Play();
            _audioSource.PlayOneShot(_audioClip);
            _paperTossGame.IncrementScore();
            Destroy(other.gameObject);
        }
    }
}
