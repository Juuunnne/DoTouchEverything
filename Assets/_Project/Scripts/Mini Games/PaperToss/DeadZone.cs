using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DeadZone : MonoBehaviour
{
    [SerializeField]
    private BoxCollider _respawnZone;

    [Header("Variables")]
    [SerializeField]
    private float _cooldownTime = 5f;

    private BoxCollider _deadZone;

    private void Start()
    {
        _deadZone = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("ScoreObject"))
        {
            StartCoroutine(ReplaceObject(collision.gameObject));
        }
    }

    private IEnumerator ReplaceObject(GameObject objToReplace)
    {
        yield return new WaitForSeconds(_cooldownTime);
        if (_deadZone.bounds.Contains(objToReplace.transform.position))
        {
            yield break;
        }

        objToReplace.transform.position = new Vector3(
            Random.Range(_respawnZone.bounds.min.x, _respawnZone.bounds.max.x),
            Random.Range(_respawnZone.bounds.min.y, _respawnZone.bounds.max.y),
            Random.Range(_respawnZone.bounds.min.z, _respawnZone.bounds.max.z));
    }
}
