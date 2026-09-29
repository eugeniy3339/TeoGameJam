using UnityEngine;
using UnityEngine.InputSystem;

public class SwingingManager : MonoBehaviour
{
    [SerializeField] private float maxSwingRaycastDistance = 10f;
    [SerializeField] private LayerMask layerMask;
    private RaycastHit2D curSwingHit;

    private Swing curSwing;

    public void swing(bool start)
    {
        if (start)
            StartSwinging();
        else
            StopSwinging();
    }

    private void StartSwinging()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector2(Mouse.current.position.x.value, Mouse.current.position.y.value));
        Vector2 dirToMouse = mousePos - transform.position;
        curSwingHit = Physics2D.Raycast(transform.position, dirToMouse, maxSwingRaycastDistance, layerMask);

        if (!curSwingHit) return;
        if(curSwingHit.collider.tag == "Grappable")
            StartSwinging(curSwingHit);
    }

    private void StartSwinging(RaycastHit2D swingHit)
    {
        if (!swingHit) return;
        if (curSwing != null)
            StopSwinging();

        curSwing = Swing.SpawnSwing(swingHit.point);
        print(curSwing);
    }

    private void StopSwinging()
    {
        if (curSwing == null) return;
        Destroy(curSwing.gameObject);
    }
}
