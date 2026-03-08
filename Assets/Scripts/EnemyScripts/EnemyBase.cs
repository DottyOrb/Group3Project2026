using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyBase : MonoBehaviour
{
    //[SerializeField] private float enemySpeed = 3.0f;
    //[SerializeField] private int enemyHealth = 3;
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
        agent.SetDestination(player.transform.position);
    }
    /*private void OnTriggerEnter(Collider otherObject)
    {
        if (otherObject.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            enemyHealth--;
            if (enemyHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }*/
}
