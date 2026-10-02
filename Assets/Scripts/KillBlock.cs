using UnityEngine;
using UnityEngine.SceneManagement;

public class KillBlock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TryToKillPlayer(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryToKillPlayer(collision.collider);
    }

    private void TryToKillPlayer(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        SceneManager.LoadScene(0);
    }
}
