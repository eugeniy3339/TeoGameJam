using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            CollectCoin();
        }
    }
    private void CollectCoin()
    {
        Player.Instance.AddCoin(coinValue);
        Destroy(gameObject);
    }
}
