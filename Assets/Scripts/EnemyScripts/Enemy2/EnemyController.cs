using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform target { get; set; }
    private UnityEngine.AI.NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (target != null)
        {
            agent.destination = target.position;
        }
    }
}
