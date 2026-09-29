using UnityEngine;

[RequireComponent(typeof(SpringJoint2D))]
public class Swing : MonoBehaviour
{
    private static GameObject _sP;
    private static GameObject swingPrefab
    {
        get
        {
            if (_sP == null)
                _sP = Resources.Load<GameObject>("Swing");
            return _sP;
        }
    }

    public static Swing SpawnSwing(Vector2 end)
    {
        Swing swing = Instantiate(swingPrefab).GetComponent<Swing>();
        swing.transform.position = end;
        swing.distance = Vector2.Distance(Player.Instance.transform.position, end);
        return swing;
    }

    [SerializeField] private Transform gfx;

    private SpringJoint2D spring;

    private float distance;

    private void Start()
    {
        spring = GetComponent<SpringJoint2D>();
        spring.distance = distance;
        spring.connectedBody = Player.Instance.rigidbody;
    }

    private void LateUpdate()
    {
        SetUpGFX();
    }

    private void SetUpGFX()
    {
        Vector3 dirToThePlayer = Player.Instance.transform.position - transform.position;
        gfx.position = transform.position + dirToThePlayer / 2f;
        gfx.right = dirToThePlayer;
        gfx.localScale = new Vector3(dirToThePlayer.magnitude, gfx.localScale.y, gfx.localScale.z);
    }
}
