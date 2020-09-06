using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Health : MonoBehaviour
{
    //only player health
    public float healthPoints = 100f;
    private float invisibilityAmount;
    public GameObject soulPickup;
    float initAmount;
    public Transform pickupInstanceTransform;
    // Update is called once per frame  
    Animator animator;
    Souls souls;
    bool isDead;
    private void Awake()
    {
        initAmount = healthPoints;
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
        souls.NullifySouls();
        animator.SetTrigger("Die");
        gameObject.GetComponent<Locomotion>().enabled = false;
        gameObject.GetComponent<SwordDraw>().enabled = false;
        gameObject.GetComponent<Melee>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        Debug.Log("You died");
        isDead = true;
    }
    public void Revive()
    {
        healthPoints = initAmount;
        gameObject.GetComponent<Locomotion>().enabled = true;
        gameObject.GetComponent<SwordDraw>().enabled = true;
        gameObject.GetComponent<Melee>().enabled = true;
        gameObject.GetComponent<Collider>().enabled = true;
        animator.enabled = true;
        StartCoroutine(Wait());
        animator.SetTrigger("Sit");
        isDead = false;
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
    }
    void Update()
    {
        if (invisibilityAmount > 0)
        {
            invisibilityAmount -= Time.deltaTime;
        }
       if(this.animator.GetCurrentAnimatorStateInfo(0).IsName("Male Sitting Pose"))
       {
            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.SetTrigger("Stand");
            }
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
        if(collision.collider.tag == "WizProjectile")
        {
            Debug.Log("Wizard hit player ");
        }
    }
    public bool DeathState()
    {
        return isDead;
    }
    public void SetDeathState(bool value)
    {
        isDead = value;
    }
}
