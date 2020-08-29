using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    Image healthBar;
    private float currentHealth;
    private float maxHealth;
    Health playerHealth;

    // Start is called before the first frame update
    void Start()
    {       
        healthBar = GetComponent<Image>();
        playerHealth = FindObjectOfType<Health>();
        maxHealth = playerHealth.healthPoints;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = playerHealth.healthPoints;
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
