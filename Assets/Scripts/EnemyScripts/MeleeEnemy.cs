using UnityEngine;

public class MeleeEnemy : EnemyBase
{
    public Transform playerTransform;
    MeleeEnemy() : base() 
    { 
        
    }

    protected override void Start()
    {
        playerTransform = player.transform;
    }
    
    protected override void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, playerTransform.position) < 5) 
        {
            enemyMelee();
        }
    }

    public void enemyMelee() 
    { 
        
    }
}
