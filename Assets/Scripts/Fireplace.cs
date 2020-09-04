using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireplace : MonoBehaviour
{
    GameObject player;
    public Transform spawn;
    EnemyHealth[] enemyHealths;
    
    //Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyHealths = FindObjectsOfType<EnemyHealth>();
        //playerAnimator = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<Health>().DeathState() == true)
        {
            player.GetComponent<Animator>().enabled = false;
            player.transform.position = spawn.position;
            foreach(EnemyHealth enemy in enemyHealths)
            {
                enemy.GetComponent<EnemyHealth>().Revive();
            }
            StartCoroutine(Wait());
            player.GetComponent<Health>().Revive();
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
    }
}
