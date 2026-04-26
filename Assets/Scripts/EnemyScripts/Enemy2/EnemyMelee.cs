using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    [SerializeField] int damage;
    public void meleeAttack(float meleeRange) 
    { 
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, meleeRange)) 
        {
            HP target = hit.transform.GetComponent<HP>();
            if (target != null) 
            { 
                target.TakeDamage(damage); 
            }
            

        }
    }
}
