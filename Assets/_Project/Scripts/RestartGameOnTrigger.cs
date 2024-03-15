using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGameOnTrigger : MonoBehaviour
{
    public float restartDelay = 5f;

    private string currentScene;

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Player"))
        {
            Invoke("RestartGame", restartDelay);
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
