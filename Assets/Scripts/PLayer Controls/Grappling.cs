using UnityEngine;

public class Grappling : MonoBehaviour
{
    [Header("Grappling")]
    private LineRenderer lineRenderer;
    private Vector3 grapplePoint;
    private Vector3 currentGrapplePos;
    public LayerMask grappleable;
    public float maxDistance = 75f;
    private SpringJoint joint;

    [Header("Grapple prediction")]
    public RaycastHit predictionHit;
    public float predictionSphereCastRadius;

    [Header("Input")]
    public KeyCode grappleKey = KeyCode.Mouse1;

    [Header("External references")]
    public Transform grappleStart;
    public Transform playerCamera;
    public Transform player;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        CheckForSwingPoints();

        if (Input.GetKeyDown(grappleKey))
        {
            StartGrapple();
        }
        else if (Input.GetKeyUp(grappleKey))
        {
            EndGrapple();
        }
    }

    private void LateUpdate()
    {
        DrawGrapple();
    }

    private void CheckForSwingPoints()
    {
        if (joint != null) return;

        RaycastHit sphereCastHit;

        Physics.SphereCast(playerCamera.position, predictionSphereCastRadius, playerCamera.forward, out sphereCastHit, maxDistance, grappleable);

        RaycastHit raycastHit;
        Physics.Raycast(playerCamera.position, playerCamera.forward, out raycastHit, maxDistance, grappleable);

        Vector3 realHitPoint;

        if (raycastHit.point != Vector3.zero)
        {
            realHitPoint = raycastHit.point;
        }
        else if (sphereCastHit.point != Vector3.zero)
        {
            realHitPoint = sphereCastHit.point;
        }
        else
        {
            realHitPoint = Vector3.zero;
        }

        predictionHit = raycastHit.point == Vector3.zero ? sphereCastHit : raycastHit;
    }

    void StartGrapple()
    {
        if (predictionHit.point == Vector3.zero) return;

        grapplePoint = predictionHit.point;
        joint = player.gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = grapplePoint;

        float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

        //ideal distance from grappled object
        joint.maxDistance = distanceFromPoint * 0.3f;
        joint.minDistance = distanceFromPoint * 0.025f;

        //feel of the grapple
        joint.spring = 4.5f;
        joint.damper = 7f;
        joint.massScale = 4.5f;

        lineRenderer.positionCount = 2;
        
    }

    void DrawGrapple()
    {
        //not grappling, no rope
        if (!joint) return;

        //draws the rope while you are grappling.
        currentGrapplePos = Vector3.Lerp(currentGrapplePos, grapplePoint, Time.deltaTime * 8f);
        lineRenderer.SetPosition(0, grappleStart.position);
        lineRenderer.SetPosition(1, currentGrapplePos);
    }

    void EndGrapple()
    {
        lineRenderer.positionCount = 0;
        Destroy(joint);
    }
}
