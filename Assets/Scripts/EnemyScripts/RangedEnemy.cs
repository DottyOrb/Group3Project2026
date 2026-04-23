using UnityEngine;

public class RangedEnemy : EnemyBase
{
    [SerializeField] public GameObject projBase;
    //[SerializeField] private float projLife = 4;
    [SerializeField] private float enemyFireRate = 2.5f;
    public Transform projSpawn;
    public Transform playerTransform;
    RangedEnemy() : base() 
    { 
        
    }
    
    
    protected override void Start()
    {
        base.Start();
        playerTransform = player.transform;
    }

    
    protected override void Update()
    {
        //IDEA: replace upper limit with distance to target, making the enemy more likely to shoot when the player is closer 
        EnemyMove();
        enemyShoot();
    }

    private void enemyShoot() 
    { 
        GameObject projInst = Instantiate(projBase, projSpawn.transform.position,projSpawn.transform.rotation);
        Rigidbody projRb = projInst.GetComponent<Rigidbody>();
        projRb.AddForce(projRb.transform.forward * enemySpeed);
    }
    protected override void EnemyMove() 
    {
        //distance between player and enemy to find vector, do SQRT(x^2+y^2+z^2) to find magnitude - if magnitude < certain amount (e.g. 10) then enemy should stop
        if (Vector3.Distance(gameObject.transform.position, playerTransform.position) > 2)
        {
            base.EnemyMove();
        }
        else 
        {
            enemyShoot();
        }
    }
}
