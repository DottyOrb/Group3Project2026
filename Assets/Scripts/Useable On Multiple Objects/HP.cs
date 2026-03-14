using UnityEngine;

public class HP : MonoBehaviour
{
    [Header("HP")]
    private float maxHealth;
    public float health;
    public bool isPlayer;

    [Header("regen")]
    float regenDelay = 2f;
    float timeWasDamaged;

    private void Start()
    {
        maxHealth = health;
    }



     public void Heal(float healAmount)
    {
        if (isPlayer || CanHeal())
        {
            health += healAmount;

            if (health > maxHealth)
                health = maxHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        //gets the ingame time the player recieved damage
        timeWasDamaged = Time.time;

        if (health <= 0 && !isPlayer)
        {
            Die();
        }
        else if (health <= 0 && isPlayer)
        {
            PlayerDeath();
        }
    }

    public bool CanHeal()
    {
        //compares current time to time between last damage and regen delay to see if you can actually heal.
        return Time.time > timeWasDamaged + regenDelay; 
    }

    void PlayerDeath()
    {
        //Make the death UI pop up
        Debug.Log("Alex this is UI stuff so you do this");
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
