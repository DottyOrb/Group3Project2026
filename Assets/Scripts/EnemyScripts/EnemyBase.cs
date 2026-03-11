using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected float enemySpeed = 4.0f;
    [SerializeField] private GameObject player;
    private NavMeshAgent agent;
    /*public enum EnemyActions
    {
        Moving,
        Attacking,
        Death
    }
    public EnemyActions enemyActions;*/
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        /*switch (enemyActions)
        {
            case EnemyActions.Moving://moves towards the player to try to get to the correct range to attack
                break;
            case EnemyActions.Attacking://attacks the player
                break;
            case EnemyActions.Death:
                break;
        }*/
        GetComponent<NavMeshAgent>().speed = enemySpeed;
        agent.SetDestination(player.transform.position);
    }

    public float GetEnemySpeed() 
    {
        return enemySpeed;
    }
    public void SetEnemySpeed(float speed) 
    { 
        enemySpeed = speed;
    }
}
