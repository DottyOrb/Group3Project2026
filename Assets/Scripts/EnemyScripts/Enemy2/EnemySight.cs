using UnityEngine;

public class EnemySight : MonoBehaviour
{
    bool inVisionAngle, inRange, inFullView;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float detectionRange;
    [SerializeField]
    private float detectionAngle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inVisionAngle = false;
        inRange = false;
        inFullView = false;
        detectionRange = 18;
        detectionAngle = 60;
    }
    public patrolStates SetChase()
    {
        if (inRange && inFullView && inVisionAngle)
        {
            return patrolStates.Chase;
        }
        return patrolStates.Patrol;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                Debug.Log("we good now");
            }
        }

        if (Vector3.Distance(transform.position, player.transform.position) <= detectionRange)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, (player.transform.position - transform.position), out hit, Mathf.Infinity))
        {
            if (hit.transform == player.transform)
            {
                inFullView = true;
            }
            else
            {
                inFullView = false;
            }
        }
        else
        {
            inFullView = false;
        }
        Vector3 side1 = player.transform.position - transform.position;
        Vector3 side2 = transform.forward;
        float angle = Vector3.SignedAngle(side1, side2, Vector3.up);
        if (angle <= detectionAngle && angle >= -1 * detectionAngle)
        {
            inVisionAngle = true;
        }
        else
        {
            inVisionAngle = false;
        }
    }
}
