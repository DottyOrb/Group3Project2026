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

        Camera cam = playerCamera.GetComponent<Camera>();

        //in the event of the camera being a bit off centre figures out the true centre of the screen for easier aiming.
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));

        RaycastHit sphereCastHit;

        bool sphereHit = Physics.SphereCast(playerCamera.position, predictionSphereCastRadius, playerCamera.forward, out sphereCastHit, maxDistance, grappleable);

        RaycastHit raycastHit;
        bool rayHit = Physics.Raycast(ray, out raycastHit, maxDistance, grappleable);


        if (rayHit)
        {
            predictionHit = raycastHit;
        }
        else if (sphereHit)
        {
            predictionHit = sphereCastHit;
        }
        else
        {
            predictionHit = new RaycastHit();
        }
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
