using UnityEngine;

public class RangedEnemy : EnemyBase
{
    [SerializeField] public GameObject projBase;
    //[SerializeField] private float projLife = 4;
    [SerializeField] private float enemyFireRate = 2.5f;
    public Transform projSpawn;
    
    RangedEnemy() : base() 
    { 
        
    }
    
    
    void Start()
    {
        
    }

    
    void Update()
    {
        //Random.Range(0, 10); --replace upper limit with distance to target, making the enemy more likely to shoot when the player is closer 
        
        enemyShoot();
    }

    private void enemyShoot() 
    { 
        GameObject projInst = Instantiate(projBase, projSpawn.transform.position,projSpawn.transform.rotation) as GameObject;
        Rigidbody projRb = projInst.GetComponent<Rigidbody>();
        projRb.AddForce(projRb.transform.forward * enemySpeed);
    }
}
