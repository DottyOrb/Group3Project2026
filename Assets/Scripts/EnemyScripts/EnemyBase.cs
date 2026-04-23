using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected float enemySpeed = 4.0f;
    [SerializeField] public GameObject player;
    private NavMeshAgent agent;
    
    /*public enum EnemyActions
    {
        Moving,
        Attacking,
        Death
    }
    public EnemyActions enemyActions;*/
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        agent.speed = enemySpeed;

    }
    protected virtual void Update()
    {
        EnemyMove();
    }
    protected virtual void EnemyMove() 
    {
        agent.SetDestination(player.transform.position);
    }
}
