using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyActions 
    { 
        Moving,
        Attacking
    }
    public EnemyActions enemyActions;
    private void Update()
    {
        switch (enemyActions) 
        { 
            case EnemyActions.Moving://moves towards the player to try to get to the correct range to attack
                break;
            case EnemyActions.Attacking://attacks the player
                break;
        }
    }
}
