using UnityEngine;

public class ScoreZoneCollisionHandler : MonoBehaviour
{
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
        }
    }
}
