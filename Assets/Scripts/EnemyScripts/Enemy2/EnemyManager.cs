using System.Collections;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public enum patrolStates
{
    Patrol, Chase, Melee
}
public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject eyes;
    [SerializeField] private GameObject PatrolPoint1;
    [SerializeField] private GameObject PatrolPoint2;
    private bool IsPatrolPoint1;
    private patrolStates state;
    private EnemyController enemyScript;
    private EnemySight SightScript;
    private EnemyMelee enemyMeleeScript;
    public bool isMeleeing;
    public Animator animator;

    //I need some way to differentiate what area each enemy is in, so it can only target the points near it.
    //alternatively I could instead have each enemy patrol randomly between the points in a certain range.
    
    void Start()
    {
        state = patrolStates.Patrol;
        IsPatrolPoint1 = true;
        enemyScript = enemy.GetComponent<EnemyController>();
        SightScript = eyes.GetComponent<EnemySight>();
        enemyMeleeScript = enemy.GetComponent<EnemyMelee>();
    }

    
    void Update()
    {
        if (enemy != null)
        {
            float dist = Vector3.Distance(player.transform.position, enemy.transform.position);//calculates distance between player and enemy
            switch (state)
            {
                case patrolStates.Patrol:
                    if (dist <= 5)
                    {
                        state = patrolStates.Chase;//switches into chase mode when the player is close enough
                    }
                    else
                    {
                        float patrolPoint1Dist = Vector3.Distance(enemy.transform.position, PatrolPoint1.transform.position);//finds the distance between enemy and point 1
                        float patrolPoint2Dist = Vector3.Distance(enemy.transform.position, PatrolPoint2.transform.position);//finds the distance between enemy and point 2
                        IsPatrolPoint1 = checkCurrentPatrolNode(patrolPoint1Dist, patrolPoint2Dist);//sets the bool to true or false based on which is closer
                        if (IsPatrolPoint1)
                        {
                            enemyScript.target = PatrolPoint1.transform;
                            setLookAt(PatrolPoint1.transform.position);
                        }
                        else
                        {
                            enemyScript.target = PatrolPoint2.transform;
                            setLookAt(PatrolPoint2.transform.position);
                        }
                        state = SightScript.SetChase();
                    }
                    break;
                case patrolStates.Chase:
                    if (dist > 5)//if the player gets far enough away then stops the enemy from chasing the player
                    {
                        state = SightScript.SetChase();
                        enemyScript.target = player.transform;
                        setLookAt(player.transform.position);
                        //state = patrolStates.Patrol;
                    }
                    else if (dist < 5 && dist > 2)
                    {
                        enemyScript.target = player.transform;
                        setLookAt(player.transform.position);
                    }
                    else
                    {
                        state = patrolStates.Melee;
                    }
                    break;
                case patrolStates.Melee:
                    if (dist > 2)
                    {
                        state = patrolStates.Chase;
                    }
                    else if (dist <= 2 && isMeleeing == false)
                    {
                        animator.SetBool("isAttacking", true);
                        isMeleeing = true;
                        enemyMeleeScript.meleeAttack(2.5f);
                        StartCoroutine(resetMelee());
                    }
                    break;
                default:
                    break;
            }
        }
    }
    private IEnumerator resetMelee() 
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(1);
            animator.SetBool("isAttacking", false);
            isMeleeing = false;
        }
    }
    void setLookAt(Vector3 target)
    {
        Vector3 targetRotation = new Vector3(target.x, enemy.transform.position.y, target.z);//looks towards the player's horizontal position
        enemy.transform.LookAt(targetRotation);
    }
    bool checkCurrentPatrolNode(float dist1, float dist2) //chooses which patrol point to head towards
    {
        if (IsPatrolPoint1 && dist1 <= 2)
        {
            return false;
        }
        else
        {
            if (!IsPatrolPoint1 && dist2 <= 2)
            {
                return true;
            }
            else
            {
                return IsPatrolPoint1;
            }
        }
    }
}
