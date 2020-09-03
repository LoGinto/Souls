using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    Animator animator;
    public float hp = 100f;
    public int soulsReward;
    GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }
    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Enemy died. rewarded");
        player.GetComponent<Souls>().IncrementSouls(soulsReward);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "PlayerWeapon")
        {
            TakeDamage(collision.collider.GetComponent<DamageFromEnemy>().GetDamage());
            Debug.Log("Impact on " + gameObject.name);
            animator.SetTrigger("Hurt");
        }
    }
}
