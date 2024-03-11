using UnityEngine;

public class ScoreZoneCollisionHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem Confetti;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ScoreObject"))
        {
            PaperTossGame paperTossGame = FindObjectOfType<PaperTossGame>();

            if (paperTossGame != null)
            {
                paperTossGame.IncrementScore();
            }
            Destroy(other.gameObject);
            Confetti.Play();
        }
    }
}
