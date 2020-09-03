using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Health : MonoBehaviour
{
    //only player health
    public float healthPoints = 100f;
    private float invisibilityAmount;
    public GameObject soulPickup;
    public Transform pickupInstanceTransform;
    // Update is called once per frame  
    Animator animator;
    Souls souls;
    bool isDead = true;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        souls = GetComponent<Souls>();
    }
    public void TakeDamage(float damage)
    {
        if (invisibilityAmount <= 0)
        {
            healthPoints -= damage;
            if (healthPoints <= 0)
            {
                GameObject soulInstance = Instantiate(soulPickup,pickupInstanceTransform.position,Quaternion.identity);
                soulInstance.GetComponent<PickupSouls>().SetSoulsLost(souls.GetSoulsCount());
                Die();
            }
        }
    }
    void Die()
    {
        Debug.Log("You died");
    }
    void Update()
    {
        if (invisibilityAmount > 0)
        {
            invisibilityAmount -= Time.deltaTime;
        }
        
    }
    public void Invinsible(float delay,float invLength)
    {
        if (delay > 0)
        {
            StartCoroutine(StartInvisible(delay,invLength));
        }
        else
        {
            invisibilityAmount = invLength;
        }
    }

    IEnumerator StartInvisible(float delay,float invLength)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Invinsible");
        invisibilityAmount = invLength;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "EnemyWeapon")
        {
            Debug.Log("Player got damage");
            animator.SetTrigger("Hurt");
            TakeDamage(collision.collider.GetComponent<DamageFromEnemy>().GetDamage());
        }
    }
}
