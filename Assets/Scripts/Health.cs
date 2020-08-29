using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Health : MonoBehaviour
{
    //only player health
    public float healthPoints = 100f;
    private float invisibilityAmount;
    // Update is called once per frame  

    public void TakeDamage(float damage)
    {
        if (invisibilityAmount <= 0)
        {
            healthPoints -= damage;
        }
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
        invisibilityAmount = invLength;
    }
}
