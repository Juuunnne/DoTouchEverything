using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField]
    private BoxCollider _respawnZone;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("ScoreObject"))
        {
            collision.transform.position = new Vector3(
                Random.Range(_respawnZone.bounds.min.x, _respawnZone.bounds.max.x),
                Random.Range(_respawnZone.bounds.min.y, _respawnZone.bounds.max.y),
                Random.Range(_respawnZone.bounds.min.z, _respawnZone.bounds.max.z));
        }
    }
}
