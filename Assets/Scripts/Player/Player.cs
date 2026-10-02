using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public Rigidbody2D rigidbody { get; private set; }

    [SerializeField] private TMP_Text coinsText;
    private int coinsCount;

    private void Awake()
    {
        Instance = this;

        rigidbody = GetComponent<Rigidbody2D>();

        SetCoinsCount(coinsCount);
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
