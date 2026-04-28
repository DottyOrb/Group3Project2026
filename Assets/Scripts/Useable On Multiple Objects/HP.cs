using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class HP : MonoBehaviour
{
    
    
    [Header("HP")]
    private float maxHealth;
    public float health;
    public bool isPlayer;
    //[SerializeField] public GameObject HPBar;
    public HealthBar healthBar;

    [Header("regen")]
    float regenDelay = 2f;
    float timeWasDamaged;

    private void Start()
    {
        maxHealth = health;
        healthBar.setMaxHealth(maxHealth);
    }


    private void Update()
    {
        if (healthBar == null)
        {
            GameObject HPObj = GameObject.FindGameObjectWithTag("PlayerHealthBar");

            if (HPObj != null)
            {
                healthBar = HPObj.GetComponent<HealthBar>();
            }
        }
    }


    public void Heal(float healAmount)
    {
        if (isPlayer || CanHeal())
        {
            health += healAmount;
            healthBar.updateBar(health);
            if (health > maxHealth)
                health = maxHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.updateBar(health);
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
        SceneManager.LoadScene("DeathScreen");
    }


    void Die()
    {
        Destroy(gameObject);
    }
}
