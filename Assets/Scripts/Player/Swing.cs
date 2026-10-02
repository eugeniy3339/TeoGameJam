using UnityEngine;

[RequireComponent(typeof(DistanceJoint2D))]
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

    public static Swing SpawnSwing(Vector2 end, Rigidbody2D connectedBody)
    {
        Swing swing = Instantiate(swingPrefab).GetComponent<Swing>();

        swing.transform.position = end;
        swing.distance = Vector2.Distance(connectedBody.transform.position, end);

        swing.connectedRigidbody = connectedBody;
        swing.distanceJoint.connectedBody = connectedBody;

        swing.lineRenderer.positionCount = 2;
        swing.lineRenderer.SetPosition(0, end);
        swing.lineRenderer.SetPosition(1, connectedBody.transform.position);

        return swing;
    }

    [SerializeField] private DistanceJoint2D _distanceJoint;
    private DistanceJoint2D distanceJoint
    {
        get
        {
            if (_distanceJoint == null)
                _distanceJoint = GetComponent<DistanceJoint2D>();
            return _distanceJoint;
        }
    }

    [SerializeField] private LineRenderer _lineRenderer;
    private LineRenderer lineRenderer
    {
        get
        {
            if (_lineRenderer == null)
                _lineRenderer = GetComponentInChildren<LineRenderer>();
            return _lineRenderer;
        }
    }

    private Rigidbody2D connectedRigidbody;
    private float _d;
    private float distance
    {
        get { return _d; }
        set
        {
            _d = value;
            distanceJoint.distance = value;
        }
    }

    private void LateUpdate()
    {
        SetUpGFX();
    }

    private void SetUpGFX()
    {
        lineRenderer.SetPosition(1, connectedRigidbody.transform.position);
    }
}
