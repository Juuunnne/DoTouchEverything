using UnityEngine;

public class ScoreZoneCollisionHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem Confetti;
    [SerializeField] private PaperTossGame _paperTossGame;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ScoreObject"))
        {
            _paperTossGame.IncrementScore();
            Destroy(other.gameObject);
            Confetti.Play();
        }
    }
}
