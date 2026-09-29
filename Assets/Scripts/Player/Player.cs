using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public Rigidbody2D rigidbody { get; private set; }

    private void Awake()
    {
        Instance = this;

        rigidbody = GetComponent<Rigidbody2D>();
    }
}
