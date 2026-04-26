using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;

    public void setMaxHealth(float maxHealth) 
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }
    public void updateBar(float health) 
    { 
        healthBar.value = health;
    }
}
