using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public Rigidbody2D rigidbody { get; private set; }

    [SerializeField] private TMP_Text coinsText;
    public static int coinsCount { get; private set; }

    private void Awake()
    {
        Instance = this;

        rigidbody = GetComponent<Rigidbody2D>();

        SetCoinsCount(0);
    }

    public void AddCoin(int coinValue)
    {
        SetCoinsCount(coinsCount + coinValue);
    }
    
    private void SetCoinsCount(int newCount)
    {
        coinsCount = newCount;
        if (coinsText != null)
            coinsText.text = newCount.ToString() + "X";
    }
}
